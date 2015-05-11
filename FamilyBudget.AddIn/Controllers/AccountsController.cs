using FamilyBudget.AddIn.UI;
using FamilyBudget.Data.Interfaces;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using System.ComponentModel;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using FamilyBudget.Data;

namespace FamilyBudget.AddIn.Controllers
{
    internal static class AccountsController
    {
        #region Properties
        // editing form
        private static frmAccounts accountsForm;

        // paymentMethod mapper interface
        private static IAccountAPI _accountAPI;
        private static IAccountAPI accountAPI
        {
            get
            {
                if (_accountAPI == null)
                {
                    // get the configured name of the interface to manage accounts
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(IAccountAPI));
                    if (mapperType != null)
                    {
                        _accountAPI = (IAccountAPI)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _accountAPI = null;
                    }
                }

                return _accountAPI;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_AccountController");
        #endregion

        internal static void btnManageAccounts_Click(object sender, RibbonControlEventArgs e)
        {
            accountsForm = new frmAccounts();
            accountsForm.Show();
        }

        internal static BindingList<Account> GetAccounts(bool force = false)
        {
            return accountAPI.GetAccounts(force);
        }

        internal static OperationStatus AddNewAccount(Account newAccount)
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(newAccount);

            return accountAPI.AddNewAccounts(accounts);
        }
    }
}
