﻿using System;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using log4net;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmPaymentMethods : Form
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("frmPaymentModal");

        #endregion

        public frmPaymentMethods()
        {
            InitializeComponent();
        }

        private void PaymentMethodModal_Load(object sender, EventArgs e)
        {
            PaymentMethodBindingSource.DataSource = PaymentMethodsController.GetPaymentMethods();
        }
       
        private void btnAddNewPaymentMethod_Click(object sender, EventArgs e)
        {
            // create the new payment method
            PaymentMethod newPaymentMethod = new PaymentMethod()
            {
                PaymentMethodName = txtPaymentMethod.Text,
                IsActive = chkEnabled.Checked
            };

            // ask the controller to add it
            OperationStatus status = PaymentMethodsController.AddNewPaymentMethod(newPaymentMethod);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                errorText = "An error occurred while attempting to add a new Payment Method to the DB:" + Environment.NewLine +
                                   "Check that the API is available and try again.";

                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else
            {
                // reset the text in the paymentMethod textbox
                txtPaymentMethod.Text = null;
            }
        }
    }
}
