using System;
using System.Windows.Forms;
using HouseholdBudget.Events;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Enums;

namespace HouseholdBudget.UI
{
    internal partial class frmNewCategory : Form
    {
        internal event EventHandler<CategoryEventArgs> CategorySaved;
        internal event EventHandler<CategoryControlEventArgs> UserCancelled;

        internal frmNewCategory()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber to the saved event, fire the event
            // else, report error
            if (CategorySaved != null)
            {
                CategoryEventArgs args = new CategoryEventArgs()
                {
                    category = new Category()
                    {
                        CategoryName = txtCategoryName.Text
                    }
                };

                // fire saved event
                CategorySaved(this, args);
            }
            else
            {
                // report error and close the form
                MessageBox.Show("There is no event handler tied to the save event!");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber, fire the event, otherwise, report error
            if (UserCancelled != null)
            {
                UserCancelled(this, new CategoryControlEventArgs() { formType =  CategoryFormType.ParentCategory });
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
