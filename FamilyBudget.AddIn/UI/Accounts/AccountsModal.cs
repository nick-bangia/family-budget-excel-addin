﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Enums;
using FamilyBudget.AddIn.Events;
using log4net;
using FamilyBudget.Data.Domain;
using FamilyBudget.AddIn.Controllers;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmAccounts : Form
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("frmAccounts");

        #endregion

        public frmAccounts()
        {
            InitializeComponent();
        }
                     
        private void AccountModal_Load(object sender, EventArgs e)
        {
            AccountBindingSource.DataSource = AccountsController.GetAccounts();
        }

        private void btnAddNewAccount_Click(object sender, EventArgs e)
        {
            // create the new account object
            Account newAccount = new Account()
            {
                AccountName = txtAccount.Text,
                IsActive = chkEnabled.Checked
            };
            
            // ask the controller to add it
            OperationStatus status = AccountsController.AddNewAccount(newAccount);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                errorText = "An error occurred while attempting to add a new Account to the DB." + Environment.NewLine +
                            "Check that the API is available and try again.";

                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else
            {
                // reset the text in the account textbox
                txtAccount.Text = null;
            }
        }
    }
}