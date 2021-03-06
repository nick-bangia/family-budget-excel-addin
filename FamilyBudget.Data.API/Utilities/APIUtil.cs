﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using FamilyBudget.Common.Config;
using FamilyBudget.Common.Enums;
using FamilyBudget.Data.API.Domain;
using FamilyBudget.Data.API.Enums;
using FamilyBudget.Data.API.Protocol;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FamilyBudget.Data.API.Utilities
{
    public static class APIUtil
    {
        #region Properties
        private static readonly ILog logger = LogManager.GetLogger("APIUtil");
        private static readonly string LOGIN_URI = "/login";
        private static readonly string RENEW_URI = "/renew";
        #endregion

        public static APIResponseObject Get(string uri, bool useAccessToken = true, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.GET, queryParams, null, useAccessToken);
        }
        
        public static APIResponseObject Post(string uri, APIDataObject body, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.POST, queryParams, body, true);
        }

        public static APIResponseObject Put(string uri, APIDataObject body, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.PUT, queryParams, body, true);
        }

        public static APIResponseObject Delete(string uri, Dictionary<string, string> queryParams = null)
        {
            return MakeAPICall(GetQualifiedUri(uri), ApiMethod.DELETE, queryParams, null, true);
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
                           ((successCount == 0 && response.data.Count != 0) ? OperationStatus.FAILURE : opStatus);
                
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

        public static ApiToken Login(bool renew = false)
        {
            // initialize the return string
            ApiToken token = null;

            // attempt to ping the API and check whether it is available, and whether this utility is authorized to use it
            logger.Info("Logging into the API...");
            string uri = LOGIN_URI;
            if (renew)
            {
                uri = RENEW_URI;
            }
            
            APIResponseObject response = Get(uri, renew);
            
            // Evaluate the response
            if (response.status.Contains("ok"))
            {
                logger.Info("...and we're in!");
                if (response.data.Count > 0)
                {
                    dynamic tokenInfo = response.data[0];
                    DateTime accessExpiryDate = new DateTime(tokenInfo.access_expires_on.Value.Ticks);
                    DateTime refreshExpiryDate = new DateTime(tokenInfo.refresh_expires_on.Value.Ticks);
                    token = new ApiToken((string)tokenInfo.access_token.Value, accessExpiryDate, (string)tokenInfo.refresh_token.Value, refreshExpiryDate);
                }
                else
                {
                    // if for any reason no token is provided in a successful response, return an failure message
                    token = new ApiToken("No token provided from the API!");
                }
                
            }
            else if (response.status.Contains("failure"))
            {
                logger.Info("...login failure. Response from API on login: " + response.status + ", Reason: " + response.reason);

                // evaluate any failure responses and build the ApiHealth object
                switch (response.reason)
                {
                    case "401 - Unauthorized":
                        token = new ApiToken("This utility is unauthorized to use the API. Please check your configured username & password");
                        break;
                    default:
                        token = new ApiToken(response.reason);
                        break;
                }
            }

            // return the health state
            return token;
        }

        private static void CheckAccess(string uri)
        {
            // don't check access if the uri currently being requested is to request access
            if (!uri.Contains(LOGIN_URI) && !uri.Contains(RENEW_URI))
            {
                // initialize variables
                DateTime currentTime = DateTime.UtcNow;
                DateTime accessExpires = DateTime.Parse(AddInConfiguration.APIConfiguration.AccessExpires);
                DateTime refreshExpires = DateTime.Parse(AddInConfiguration.APIConfiguration.RefreshExpires);

                // check if the current date/time is between the access expiration and refresh expiration dates. If so, renew access
                if (currentTime < accessExpires)
                {
                    // access has not expired yet, so return
                    return;
                }
                else if (currentTime >= accessExpires && currentTime < refreshExpires)
                {
                    // renew access
                    ApiToken renewedToken = Login(true);
                    SaveTokenToConfiguration(renewedToken);
                }
                else if (currentTime >= refreshExpires)
                {
                    // login again
                    ApiToken newToken = Login();
                    SaveTokenToConfiguration(newToken);
                }
            }
        }

        private static APIResponseObject MakeAPICall(string uri, ApiMethod method, Dictionary<string, string> queryParams, APIDataObject body, bool useAccessToken)
        {
            // Check if access to the API is still valid, and if not, either login or renew the access token
            CheckAccess(uri);
            
            // log the upcoming operation
            logger.InfoFormat("About to {0} from {1}", method.ToString(), uri);
            
            // resolve the uri if queryParams are present
            if (queryParams != null) 
            {
                StringBuilder qs = new StringBuilder();
                for (int i = 0; i < queryParams.Count; i++)
                {
                    // add the delimiter if it is not the first query parameter
                    if (i != 0) { qs.Append("/"); }

                    // add the query parameter
                    string queryString = queryParams.ElementAt(i).Value;
                    logger.InfoFormat("Query string {0} is {1}", i, queryString);
                    qs.Append(queryString);
                }

                // append the query strings to the uri
                uri += "/" + qs.ToString();
                logger.InfoFormat("Full URI to call on: {0}", uri);
            }
            
            // set up the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method.ToString();

            if (!useAccessToken)
            {
                string username = AddInConfiguration.APIConfiguration.Username;
                string password = AddInConfiguration.APIConfiguration.Password;
                SetBasicAuthHeader(request, username, password);   
            }
            else
            {
                string accessToken = AddInConfiguration.APIConfiguration.AccessToken;
                string refreshToken = AddInConfiguration.APIConfiguration.RefreshToken;
                SetTokenHeader(request, accessToken, refreshToken);
            }
            
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
                            IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = AddInConfiguration.APIConfiguration.DateFormat };
                            responseObject = JsonConvert.DeserializeObject<APIResponseObject>(jsonResponse, dateTimeConverter);
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

        private static void SaveTokenToConfiguration(ApiToken apiToken)
        {
            AddInConfiguration.APIConfiguration.AccessToken = apiToken.accessToken;
            AddInConfiguration.APIConfiguration.AccessExpires = apiToken.accessExpiresOn.ToString("o");
            AddInConfiguration.APIConfiguration.RefreshToken = apiToken.refreshToken;
            AddInConfiguration.APIConfiguration.RefreshExpires = apiToken.refreshExpiresOn.ToString("o");
        }

        private static void SetBasicAuthHeader(WebRequest request, String username, String password)
        {
            string authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }

        private static void SetTokenHeader(WebRequest request, String accessToken, String refreshToken = null)
        {
            request.Headers["x_access_token"] = accessToken;

            if (!String.IsNullOrEmpty(refreshToken))
            {
                request.Headers["x_refresh_token"] = refreshToken;
            }
        }

        private static string GetQualifiedUri(string uri)
        {
            // verify that the passed in path begins with a "/"
            if (!uri.StartsWith("/"))
            {
                uri += "/";
            }

            // return the qualified uri
            return AddInConfiguration.APIConfiguration.RootUri + uri;
        }
    }
}
