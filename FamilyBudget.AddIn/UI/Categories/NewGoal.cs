using System;
using System.Windows.Forms;
using FamilyBudget.AddIn.Events;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Domain;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Controllers;

namespace FamilyBudget.AddIn.UI
{
    internal partial class frmNewGoal : Form
    {
        internal event EventHandler<GoalEventArgs> GoalSaved;
        internal event EventHandler<CategoryControlEventArgs> UserCancelled;
        private LiveDataObject categoryDataObject;

        internal frmNewGoal()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void frmNewGoal_Load(object sender, EventArgs e)
        {
            categoryDataObject = CategoriesController.GetCategories();
            cbParentCategory.DataSource = categoryDataObject.dataSource;
            cbParentCategory.DisplayMember = "CategoryName";
            cbParentCategory.ValueMember = "CategoryKey";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // attempt to parse the goal amount into the goalAmount variable
            decimal goalAmount = 0.0M;
            Decimal.TryParse(txtGoalAmount.Text, out goalAmount);
            
            if (goalAmount > 0.0M) {
                // if there is at least one subscriber to the saved event, fire the event
                // else, report error
                if (GoalSaved != null)
                {
                    GoalEventArgs args = new GoalEventArgs()
                    {
                        subCategory = new SubCategory()
                        {
                            CategoryKey = (Guid)cbParentCategory.SelectedValue,
                            SubCategoryName = txtGoalName.Text,
                            SubCategoryPrefix = txtGoalPrefix.Text,
                            AccountName = txtAccountName.Text,
                            IsActive = chkEnabled.Checked,
                            IsGoal = true
                        },
                        GoalAmount = goalAmount
                    
                    };

                    // fire saved event
                    GoalSaved(this, args);
                }
                else
                {
                    // report error and close the form
                    MessageBox.Show("There is no event handler tied to the save event!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid Goal Amount. Please enter a valid decimal number!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber, fire the event, otherwise, report error
            if (UserCancelled != null)
            {
                UserCancelled(this, new CategoryControlEventArgs() { formType = CategoryFormType.Goal });
            }
            else
            {
                // report error and close the form
                MessageBox.Show("There is no event handler tied to the cancel event!");
                this.Close();
            }
        }
    }
}
