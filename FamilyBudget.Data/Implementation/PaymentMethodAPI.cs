using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using FamilyBudget.Data.Interfaces;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Domain;
using FamilyBudget.DataModel;
using System.ComponentModel;
using FamilyBudget.Data.Utilities;
using Newtonsoft.Json;

namespace FamilyBudget.Data.Implementation
{
    public class PaymentMethodApiMapper : IPaymentMethodAPI
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("APIPaymentMethodMapper");
        private BindingList<PaymentMethod> paymentMethods;

        #endregion

        #region IPaymentMethodMapper

        public BindingList<PaymentMethod> GetPaymentMethods(bool forceGet = false)
        {
            if (paymentMethods != null && !forceGet)
            {
                return paymentMethods;
            }
            else
            {
                return GetPaymentMethodsFromAPI();
            }            
        }

        public OperationStatus AddNewPaymentMethods(List<PaymentMethod> paymentMethods)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;
            
            if (paymentMethods != null)
            {
                // make the call to the API if the paymentMethods list is not null
                APIResponseObject response = PostToAPI(paymentMethods, "/add");

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> pmResults;
                status = APIUtil.EvaluateResponse(response, out pmResults, true);
                
                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in pmResults)
                {
                    dynamic dynPm = itemResponse.data;

                    // create the PaymentMethod from each item dynamically
                    PaymentMethod pm = new PaymentMethod()
                    {
                        PaymentMethodKey = dynPm.paymentMethodKey,
                        PaymentMethodName = dynPm.paymentMethodName,
                        IsActive = dynPm.isActive
                    };

                    // add to the persisted list
                    this.paymentMethods.Add(pm);
                }
            }

            // get the operation status based on the API Response
            return status;
        }

        public OperationStatus UpdatePaymentMethods(List<PaymentMethod> paymentMethods)
        {
            APIResponseObject response = PostToAPI(paymentMethods, "/update");

            // get the operation status based on the API Response
            return APIUtil.EvaluateResponse(response);
        }
                             
        public PaymentMethod GetPaymentMethodByName(string paymentMethodName)
        {
            // get the payment method list if it isn't already populated
            if (paymentMethods == null)
            {
                GetPaymentMethodsFromAPI();
            }

            // get the first payment method whose name is the same as the passed in name
            return paymentMethods.First(pm => pm.PaymentMethodName.Equals(paymentMethodName));
        }

        public PaymentMethod GetDefaultPaymentMethod()
        {
            // get the payment method list if it isn't already populated
            if (paymentMethods == null)
            {
                GetPaymentMethodsFromAPI();
            }

            // get the first payment method whose name is the same as the passed in name
            return paymentMethods.ElementAt(0);
        }
        #endregion

        #region Private Methods
        private BindingList<PaymentMethod> GetPaymentMethodsFromAPI()
        {
            // make the API call to get the data
            paymentMethods = new BindingList<PaymentMethod>();

            string uri = "/paymentMethods/all";

            // make the call to the API
            APIResponseObject response = APIUtil.Get(uri);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through the response's data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    PaymentMethod pm = new PaymentMethod();
                    pm.PaymentMethodKey = d.paymentMethodKey;
                    pm.PaymentMethodName = d.paymentMethodName;
                    pm.IsActive = d.isActive;
                    paymentMethods.Add(pm);
                }

                // attach to the listUpdated event for this BindingList
                paymentMethods.ListChanged += PaymentMethods_ListChanged;
            }           
            
            return paymentMethods;
        }

        private APIResponseObject PostToAPI(List<PaymentMethod> pmsToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

            string uri = "/paymentMethods" + target;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            foreach (PaymentMethod pm in pmsToPost)
            {
                postData.data.Add(new
                {
                    paymentMethodKey = pm.PaymentMethodKey,
                    paymentMethodName = pm.PaymentMethodName,
                    isActive = pm.IsActive
                });
            }

            // make the POST request & return response
            response = APIUtil.Post(uri, postData);

            return response;
        }        
        #endregion

        #region Event Handlers
        internal void PaymentMethods_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (paymentMethods != null)
                {
                    List<PaymentMethod> paymentMethodsToUpdate = new List<PaymentMethod>();

                    // get the affected payment method
                    PaymentMethod pmChanged = paymentMethods[e.NewIndex];
                    paymentMethodsToUpdate.Add(pmChanged);

                    // send the list off to the update method.
                    UpdatePaymentMethods(paymentMethodsToUpdate);

                }
            }
        }
        #endregion
    }
}
