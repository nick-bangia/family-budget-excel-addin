using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Events;
using log4net;

namespace HouseholdBudget.UI
{
    public partial class frmPaymentMethods : Form
    {
        #region Properties

        private LiveDataObject paymentMethodDataObject { get; set; }
        private static readonly ILog logger = LogManager.GetLogger("frmPaymentModal");

        #endregion

        public frmPaymentMethods()
        {
            InitializeComponent();
        }

        private void PaymentMethodModal_Load(object sender, EventArgs e)
        {
            paymentMethodDataObject = Controller.GetPaymentMethods();
            PaymentMethodBindingSource.DataSource = paymentMethodDataObject.dataSource;
        }

        private void PaymentMethodDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            paymentMethodDataObject.objectContext.SaveChanges();
        }

        private void btnAddNewPaymentMethod_Click(object sender, EventArgs e)
        {
            string paymentMethod = txtPaymentMethod.Text;

            OperationStatus status = Controller.AddNewPaymentMethod(txtPaymentMethod.Text, chkEnabled.Checked);

            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                string errorText = "An error occurred while attempting to add a new Payment Method to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new Payment Method is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else
            {
                // if successful, refresh the grid to show the new method
                paymentMethodDataObject = Controller.GetPaymentMethods();
                PaymentMethodBindingSource.DataSource = paymentMethodDataObject.dataSource;
            }
        }
    }
}
