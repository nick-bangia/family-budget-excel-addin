﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Protocol;
using log4net;
using Newtonsoft.Json;

namespace FamilyBudget.Data.Utilities
{
    public static class APIUtil
    {
        #region Properties
        private static readonly ILog logger = LogManager.GetLogger("APIUtil");
        private static string rootUri = ConfigurationManager.AppSettings["familyBudgetAPIRootURL"];
        #endregion

        public static APIResponseObject Get(string uri, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.GET, queryParams, null);
        }
        
        public static APIResponseObject Post(string uri, APIDataObject body, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.POST, queryParams, body);
        }

        public static APIResponseObject Put(string uri, APIDataObject body, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.PUT, queryParams, body);
        }

        public static APIResponseObject Delete(string uri, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.DELETE, queryParams, null);
        }

        public static OperationStatus EvaluateResponse(APIResponseObject response, out List<Object> responseData, bool successfulOnly)
        {
            // start with assuming success
            OperationStatus opStatus = OperationStatus.FAILURE;
            int successCount = 0;
            responseData = null;

            // check the overall response wrapper's success before continuing
            if (response != null && IsSuccessful(response))
            {
                // initialize the responseData and opStatus if the overall wrapper is successful
                responseData = new List<Object>();
                opStatus = OperationStatus.SUCCESS;

                // loop through each item in the response's data and check for success
                foreach (dynamic itemResponse in response.data)
                {
                    if (IsSuccessful(itemResponse))
                    {
                        // keep count of each success
                        successCount++;

                        // add this item to the responseData output parameter
                        responseData.Add(itemResponse);
                    }
                    else
                    {
                        // if not successful, check if it is requested in the output parameter
                        if (!successfulOnly)
                        {
                            responseData.Add(itemResponse);
                        }
                    }
                }

                // assign partial success if the number of items posted successfully is not equal to
                // the total count, but there is at least 1 successful item
                // assign failure if the number of items posted successfully is 0
                // otherwise, keep the status the same
                opStatus = (successCount > 0 && successCount != response.data.Count) ? OperationStatus.PARTIAL_SUCCESS :
                           ((successCount == 0) ? OperationStatus.FAILURE : opStatus);
                
            }

            return opStatus;
        }

        public static OperationStatus EvaluateResponse(APIResponseObject response)
        {
            // initialize a dummy output variable to call on EvaluateResponse with
            List<Object> dummyList = null;

            return EvaluateResponse(response, out dummyList, true);
        }

        public static bool IsSuccessful(dynamic response)
        {
            return (response.status == "ok" && response.reason == "success");
        }

        public static ApiHealth CheckAPIHealth()
        {
            // initialize the return string
            ApiHealth health = null;

            // attempt to ping the API and check whether it is available, and whether this utility is authorized to use it
            string uri = "/ping";
            APIResponseObject response = Get(uri);
            
            // Evaluate the response and determine the healthState of the API
            if (response.status.Contains("ok"))
            {
                health = new ApiHealth(ApiHealthState.OK, "API is Healthy!");
            }
            else if (response.status.Contains("failure"))
            {
                // evaluate any failure responses and build the ApiHealth object
                switch (response.reason)
                {
                    case "401 - Unauthorized":
                        health = new ApiHealth(ApiHealthState.UNAUTHORIZED, "This utility is unauthorized to use the API. Please check your configured username & password");
                        break;
                    default:
                        health = new ApiHealth(ApiHealthState.OTHER, response.reason);
                        break;
                }
            }

            // return the health state
            return health;
        }
        
        private static APIResponseObject MakeAPICall(string uri, ApiMethod method, Dictionary<string, string> queryParams, APIDataObject body)
        {
            // log the upcoming operation
            logger.InfoFormat("About to {0} from {1}", method.ToString(), uri);
            
            // resolve the uri if queryParams are present
            if (queryParams != null) 
            {
                StringBuilder qs = new StringBuilder();
                for (int i = 0; i < queryParams.Count; i++)
                {
                    // add the delimiter if it is not the first query parameter
                    if (i != 0) { qs.Append("&"); }

                    // add the query parameter
                    string queryString = queryParams.ElementAt(i) + "=" + queryParams.ElementAt(i).Value;
                    logger.InfoFormat("Query string {0} is {1}", i, queryString);
                    qs.Append(queryString);
                }

                // append the query strings to the uri
                uri += "?" + qs.ToString();
                logger.InfoFormat("Full URI to call on: {0}", uri);
            }
            
            // set up the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            string username = ConfigurationManager.AppSettings["familyBudgetAPIUsername"];
            string password = ConfigurationManager.AppSettings["familyBudgetAPIPassword"];
            SetBasicAuthHeader(request, username, password);
            request.Method = method.ToString();
            
            // get the body into the request if it is not null & the method is applicable
            if ((method == ApiMethod.POST || method == ApiMethod.PUT || method == ApiMethod.DELETE) && body != null)
            {
                // convert the body into JSON and then into a byte array to stream into the request
                string jsonBody = JsonConvert.SerializeObject(body);
                byte[] bodyBytes = Encoding.ASCII.GetBytes(jsonBody.ToCharArray());
                
                // build the body of the request
                request.ContentLength = bodyBytes.Length;
                request.ContentType = "application/json";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(bodyBytes, 0, bodyBytes.Length);
                dataStream.Close();
            }

            // initialize the responseObject
            APIResponseObject responseObject = null;
            
            // attempt to get a response from the API
            try
            {
                String jsonResponse = null;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // if successful, then check that a response body exists
                    if (request.HaveResponse && response != null)
                    {
                        // if so, read that response into the jsonResponse string
                        logger.InfoFormat("Successful response received! Response Headers:\n{0}", response.Headers);
                        
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            jsonResponse = reader.ReadToEnd();

                            // log & convert to APIResponseObject
                            logger.InfoFormat("JSON response: {0}", jsonResponse);
                            responseObject = JsonConvert.DeserializeObject<APIResponseObject>(jsonResponse);
                        }
                    }
                }
            }
            // catch any web exceptions that occur (non 200 responses)
            catch (WebException ex)
            {
                string response = null;
                responseObject = new APIResponseObject();
                responseObject.data = new List<Object>();

                // if the response in the exception is available, create an APIResponseObject with it
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        logger.ErrorFormat("Error response received! Response Headers:\n{0}", errorResponse.Headers);

                        // construct the ApiResponseObject and read the response (if any) into the jsonResponse variable
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            HttpStatusCode status = ((HttpWebResponse)ex.Response).StatusCode;
                            responseObject.status = "failure";
                            responseObject.reason = ((int)status).ToString() + " - " + status.ToString();

                            response = reader.ReadToEnd();
                            responseObject.data.Add(response);                           
                        }
                    }
                }
                else
                {
                    // create the response object for this exception, if there is no HttpWebResponse object present
                    responseObject.status = "failure - " + ex.Status.ToString();
                    responseObject.reason = ex.Message;
                }
            }            

            return responseObject;
        }

        private static void SetBasicAuthHeader(WebRequest request, String username, String password)
        {
            string authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }

        private static string GetQualifiedUri(string uri)
        {
            // verify that the passed in path begins with a "/"
            if (!uri.StartsWith("/"))
            {
                uri += "/";
            }

            // return the qualified uri
            return rootUri + uri;
        }
    }
}