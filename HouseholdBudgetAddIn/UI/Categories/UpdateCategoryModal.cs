using System;
using System.Windows.Forms;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Controllers;

namespace HouseholdBudget.UI
{
    public partial class frmUpdateCategories : Form
    {
        #region Properties
        private LiveDataObject subCategoryDataObject;
        private LiveDataObject categoryDataObject;
        private LiveDataObject dataGridCategoryDataObject;
        #endregion

        public frmUpdateCategories()
        {
            InitializeComponent();
        }

        private void frmUpdateCategories_Load(object sender, EventArgs e)
        {
            // get the categories for cboCategories
            categoryDataObject = CategoriesController.GetCategories();
            categoryBindingSource.DataSource = categoryDataObject.dataSource;
            
            // get the categories for the data grid's category combo box
            dataGridCategoryDataObject = CategoriesController.GetCategories();
            dataGridCategoryBindingSource.DataSource = dataGridCategoryDataObject.dataSource;
            
            // set the selected index, and load the grid's data source
            cboCategories.SelectedIndex = 0;
            LoadSubCategoryBindingSource();
        }        

        /// <summary>
        /// Event handler for when a cell in the data grid ends editing.
        /// Saves changes on the object context so that changes are persisted
        /// </summary>
        private void categoryDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            subCategoryDataObject.objectContext.SaveChanges();
        }

        /// <summary>
        /// Event handler for when the selected index has changed for the cboCategories drop down box
        /// Updates the subcategories binding source with latest data
        /// </summary>
        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubCategoryBindingSource();
        }

        /// <summary>
        /// Event handler for when the refresh button is clicked.
        /// Updates the subcategories binding source with latest data
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSubCategoryBindingSource();
        }

        /// <summary>
        /// Populates the subCategoryBindingSource with latest data
        /// </summary>
        private void LoadSubCategoryBindingSource()
        {
            if (cboCategories.SelectedValue != null)
            {
                subCategoryDataObject = CategoriesController.GetFilteredSubCategories((Guid)cboCategories.SelectedValue);
                subCategoryBindingSource.DataSource = subCategoryDataObject.dataSource;
            }            
        }
    }
}
