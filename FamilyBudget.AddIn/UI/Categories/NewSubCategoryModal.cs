using System;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using log4net;

namespace FamilyBudget.AddIn.UI
{
    internal partial class frmNewSubcategory : Form
    {
        private static readonly ILog logger = LogManager.GetLogger("frmNewSubcategory");

        internal frmNewSubcategory()
        {
            InitializeComponent();
            this.btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void frmNewSubcategory_Load(object sender, EventArgs e)
        {
            // set up the category drop down
            cbParentCategory.DataSource = CategoriesController.GetCategories(false);
            cbParentCategory.DisplayMember = "CategoryName";
            cbParentCategory.ValueMember = "CategoryKey";

            // set up the accounts drop down
            cbAccount.DataSource = AccountsController.GetAccounts();
            cbAccount.DisplayMember = "AccountName";
            cbAccount.ValueMember = "AccountKey";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // build out the new subcategory 
            Subcategory newSubcategory = new Subcategory()
            {
                CategoryKey = (string)cbParentCategory.SelectedValue,
                SubcategoryName = txtSubCategoryName.Text,
                SubcategoryPrefix = txtSubCategoryPrefix.Text,
                AccountKey = (string)cbAccount.SelectedValue,
                IsActive = chkEnabled.Checked
            };

            // ask the controller to add it
            OperationStatus status = CategoriesController.AddNewSubcategory(newSubcategory);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                errorText = "An error occurred while attempting to add a new Account to the DB:" + Environment.NewLine +
                            "Check that the API is available and try again.";

                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form once completed
            this.Close();
        }
    }
}
