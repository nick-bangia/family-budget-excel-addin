using System;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using log4net;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmCategories : Form
    {
        #region Properties

        private BindingList<Category> categories { get; set; }
        private static readonly ILog logger = LogManager.GetLogger("frmCategories");

        #endregion

        public frmCategories()
        {
            InitializeComponent();
        }
                     
        private void CategoryModal_Load(object sender, EventArgs e)
        {
            categories = CategoriesController.GetCategories(false);
            CategoryBindingSource.DataSource = categories;
        }

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            OperationStatus status = CategoriesController.AddNewCategory(txtCategory.Text, chkEnabled.Checked);

            string errorText = null;
            if (status == OperationStatus.FAILURE)
            {
                // wasn't able to add the new payment method - notify user
                errorText = "An error occurred while attempting to add a new Category to the DB:" + Environment.NewLine +
                            "Check that the API is available and try again.";

                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else
            {
                // reset the text in the category text box
                txtCategory.Text = null;
            }
        }
    }
}
