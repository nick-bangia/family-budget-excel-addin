using System;
using System.Collections.Generic;
using System.ComponentModel;
using FamilyBudget.Common.Config;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Interfaces;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Utilities;
using log4net;

namespace FamilyBudget.Data.Implementation
{
    public class AccountAPI : IAccountAPI
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("AccountAPI");
        private BindingList<Account> accounts;

        #endregion

        #region IAccountAPI

        public BindingList<Account> GetAccounts(bool forceGet)
        {
            if (accounts != null && !forceGet)
            {
                return accounts;
            }
            else
            {
                return GetAccountsFromAPI();
            }
        }

        public OperationStatus AddNewAccounts(List<Account> accounts)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (accounts != null && accounts.Count > 0)
            {
                // make the call to the API if the paymentMethods list is not null
                APIResponseObject response = PutToAPI(accounts, AddInConfiguration.APIConfiguration.Routes.AddAccounts);

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> accResults;
                status = APIUtil.EvaluateResponse(response, out accResults, true);

                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in accResults)
                {
                    dynamic dynObj = itemResponse.data;

                    // create the PaymentMethod from each item dynamically
                    Account acc = new Account()
                    {
                        AccountKey = dynObj.accountKey,
                        AccountName = dynObj.accountName,
                        IsActive = dynObj.isActive
                    };

                    // add to the persisted list
                    this.accounts.Add(acc);
                }
            }

            // get the operation status based on the API Response
            return status;
        }

        public OperationStatus UpdateAccounts(List<Account> accounts)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (accounts != null && accounts.Count > 0)
            {
                APIResponseObject response = PutToAPI(accounts, AddInConfiguration.APIConfiguration.Routes.UpdateAccounts);

                // get the operation status based on the API Response
                status = APIUtil.EvaluateResponse(response);
            }

            return status;
        }
        
        #endregion

        #region Private Methods
        private BindingList<Account> GetAccountsFromAPI()
        {
            // Initialize the list
            accounts = new BindingList<Account>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetAccounts);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through the response's data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    Account acc = new Account();
                    acc.AccountKey = d.accountKey;
                    acc.AccountName = d.accountName;
                    acc.IsActive = d.isActive;
                    accounts.Add(acc);
                }

                // attach to the listUpdated event for this BindingList
                accounts.ListChanged += Accounts_ListChanged;
            }           
            
            return accounts;
        }

        private APIResponseObject PutToAPI(List<Account> accountsToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            foreach (Account acc in accountsToPost)
            {
                postData.data.Add(new
                {
                    accountKey = acc.AccountKey,
                    accountName = acc.AccountName,
                    isActive = acc.IsActive
                });
            }

            // make the POST request & return response
            response = APIUtil.Put(target, postData);

            return response;
        }        
        #endregion

        #region Event Handlers
        internal void Accounts_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (accounts != null)
                {
                    List<Account> accountsToUpdate = new List<Account>();

                    // get the affected payment method
                    Account accChanged = accounts[e.NewIndex];
                    accountsToUpdate.Add(accChanged);

                    // send the list off to the update method.
                    UpdateAccounts(accountsToUpdate);
                }
            }
        }
        #endregion        
    }
}
