using System;
using System.Windows.Forms;
using FamilyBudget.AddIn.Events;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Domain;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Controllers;

namespace FamilyBudget.AddIn.UI
{
    internal partial class frmNewSubCategory : Form
    {
        internal event EventHandler<SubCategoryEventArgs> SubCategorySaved;
        internal event EventHandler<CategoryControlEventArgs> UserCancelled;
        private LiveDataObject categoryDataObject;

        internal frmNewSubCategory()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void frmNewCategory_Load(object sender, EventArgs e)
        {
            categoryDataObject = CategoriesController.GetCategories();
            cbParentCategory.DataSource = categoryDataObject.dataSource;
            cbParentCategory.DisplayMember = "CategoryName";
            cbParentCategory.ValueMember = "CategoryKey";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // if there is at least one subscriber to the saved event, fire the event
            // else, report error
            if (SubCategorySaved != null)
            {
                SubCategoryEventArgs args = new SubCategoryEventArgs()
                {
                    subCategory = new SubCategory()
                    {
                        CategoryKey = (Guid)cbParentCategory.SelectedValue,
                        SubCategoryName = txtSubCategoryName.Text,
                        SubCategoryPrefix = txtSubCategoryPrefix.Text,
                        AccountName = txtAccountName.Text,
                        IsActive = chkEnabled.Checked,
                        IsGoal = false
                    }
                    
                };

                // fire saved event
                SubCategorySaved(this, args);
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
                UserCancelled(this, new CategoryControlEventArgs() { formType = CategoryFormType.SubCategory });
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
