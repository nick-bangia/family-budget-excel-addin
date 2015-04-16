using System;
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
    public partial class frmPaymentMethods : Form
    {
        #region Properties

        private BindingList<PaymentMethod> paymentMethods { get; set; }
        private static readonly ILog logger = LogManager.GetLogger("frmPaymentModal");

        #endregion

        public frmPaymentMethods()
        {
            InitializeComponent();
        }

        private void PaymentMethodModal_Load(object sender, EventArgs e)
        {
            paymentMethods = PaymentMethodsController.GetPaymentMethods();
            PaymentMethodBindingSource.DataSource = paymentMethods;
        }
       
        private void btnAddNewPaymentMethod_Click(object sender, EventArgs e)
        {
            OperationStatus status = PaymentMethodsController.AddNewPaymentMethod(txtPaymentMethod.Text, chkEnabled.Checked);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                 errorText = "An error occurred while attempting to add a new Payment Method to the DB:" + Environment.NewLine +
                                    "Check that the API is available and try again.";

                 logger.Error(errorText);
                 MessageBox.Show(errorText);
            }            
        }
    }
}
