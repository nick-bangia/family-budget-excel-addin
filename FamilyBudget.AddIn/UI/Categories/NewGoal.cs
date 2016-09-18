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
        private BindingList<Account> accounts;
        private static readonly ILog logger = LogManager.GetLogger("frmNewGoal");

        internal frmNewGoal()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void frmNewGoal_Load(object sender, EventArgs e)
        {
            accounts = AccountsController.GetAccounts();
            cbAccounts.DataSource = accounts;
            cbAccounts.DisplayMember = "AccountName";
            cbAccounts.ValueMember = "AccountKey";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // attempt to parse the goal amount into the goalAmount variable
            decimal goalAmount = 0.0M;
            Decimal.TryParse(txtGoalAmount.Text, out goalAmount);

            // get the Goals category, if it exists. If it doesn't, don't save this goal and notify user
            string categoryKey = CategoriesController.GetCategoryID("Goals");

            if (goalAmount > 0.0M && !String.IsNullOrWhiteSpace(categoryKey))
            {
                /*Goal newGoal = new Goal()
                {
                    CategoryKey = categoryKey,
                    SubcategoryName = txtGoalName.Text,
                    SubcategoryPrefix = txtGoalPrefix.Text,
                    AccountKey = (string)cbAccounts.SelectedValue,
                    IsActive = chkEnabled.Checked,
                    GoalAmount = goalAmount
                };

                // ask the controller to add it
                OperationStatus status = CategoriesController.AddNewGoal(newGoal);

                string errorText = null;
                if (status == OperationStatus.FAILURE)
                {
                    // wasn't able to add the new goal, notify user
                    errorText = "An error occurred while attempting to add a new Goal to the DB." + Environment.NewLine +
                                "Check that the API Is available and try again.";
                    logger.Error(errorText);
                    MessageBox.Show(errorText);
                }
                else
                {
                    // successful, so close the modal
                    this.Close();
                }*/
            }
            else
            {
                // validation failed. notify user
                MessageBox.Show("Invalid Goal Amount, or there is no category named Goals in the system. Please fix errors & try again.");
            }
        }
    }
}
