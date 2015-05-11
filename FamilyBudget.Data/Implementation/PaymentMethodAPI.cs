using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FamilyBudget.Common.Config;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Interfaces;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Utilities;
using log4net;

namespace FamilyBudget.Data.Implementation
{
    public class PaymentMethodApiMapper : IPaymentMethodAPI
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("APIPaymentMethodMapper");
        private BindingList<PaymentMethod> paymentMethods;

        #endregion

        #region IPaymentMethodMapper

        public BindingList<PaymentMethod> GetPaymentMethods(bool forceGet)
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
            
            if (paymentMethods != null && paymentMethods.Count > 0)
            {
                // make the call to the API if the paymentMethods list is not null
                APIResponseObject response = PutToAPI(paymentMethods, AddInConfiguration.APIConfiguration.Routes.AddPaymentMethods);

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> pmResults;
                status = APIUtil.EvaluateResponse(response, out pmResults, true);
                
                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in pmResults)
                {
                    dynamic dynObj = itemResponse.data;

                    // create the PaymentMethod from each item dynamically
                    PaymentMethod pm = new PaymentMethod()
                    {
                        PaymentMethodKey = dynObj.paymentMethodKey,
                        PaymentMethodName = dynObj.paymentMethodName,
                        IsActive = dynObj.isActive
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
            // Initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (paymentMethods != null && paymentMethods.Count > 0)
            {
                APIResponseObject response = PutToAPI(paymentMethods, AddInConfiguration.APIConfiguration.Routes.UpdatePaymentMethods);

                // get the operation status based on the API Response
                status = APIUtil.EvaluateResponse(response);
            }

            return status;
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

            // get the first payment method
            return paymentMethods.ElementAt(0);
        }
        #endregion

        #region Private Methods
        private BindingList<PaymentMethod> GetPaymentMethodsFromAPI()
        {
            // initialize the list
            paymentMethods = new BindingList<PaymentMethod>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetPaymentMethods);

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

        private APIResponseObject PutToAPI(List<PaymentMethod> pmsToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

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
            response = APIUtil.Put(target, postData);

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
