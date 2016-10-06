using System;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using log4net;

namespace FamilyBudget.AddIn.UI
{
    internal partial class frmNewGoal : Form
    {
        private static readonly ILog logger = LogManager.GetLogger("frmNewGoal");

        internal frmNewGoal()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void frmNewGoal_Load(object sender, EventArgs e)
        {
            // set up the category drop down
            cbParentCategory.DataSource = CategoriesController.GetCategories(false);
            cbParentCategory.DisplayMember = "CategoryName";
            cbParentCategory.ValueMember = "Key";

            // set up the accounts drop down
            cbAccounts.DataSource = AccountsController.GetAccounts();
            cbAccounts.DisplayMember = "AccountName";
            cbAccounts.ValueMember = "Key";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // attempt to parse the goal amount into the goalAmount variable
            decimal goalAmount = 0.0M;
            Decimal.TryParse(txtGoalAmount.Text, out goalAmount);

            Goal newGoal = new Goal()
            {
                CategoryKey = (string)cbParentCategory.SelectedValue,
                Name = txtGoalName.Text,
                Prefix = txtGoalPrefix.Text,
                AccountKey = (string)cbAccounts.SelectedValue,
                IsActive = chkEnabled.Checked,
                IsAllocatable = chkAllocatable.Checked,
                GoalAmount = goalAmount,
                EstimatedCompletionDate = dtEstimatedCompletionDate.Value.Date
            };

            // ask the controller to add it
            OperationStatus status = CategoriesController.AddNewGoal(newGoal);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new goal, notify user
                errorText = "An error occurred while attempting to add a new goal to the DB." + Environment.NewLine +
                            "Check that the API Is available and try again.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else
            {
                // successful, so close the modal
                this.Close();
            }
        }
    }
}
