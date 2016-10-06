using System;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.Common.Domain;

namespace FamilyBudget.AddIn.UI
{
    public partial class frmUpdateGoals : Form
    {
        #region Properties
        private BindingList<Goal> goalDataObject;
        private BindingList<Category> categoryDataObject;
        private BindingList<Category> dataGridCategoryDataObject;
        private BindingList<Account> dataGridAccountDataObject;
        #endregion

        public frmUpdateGoals()
        {
            InitializeComponent();
        }

        private void frmUpdateGoals_Load(object sender, EventArgs e)
        {
            // get the categories for cboCategories
            categoryDataObject = CategoriesController.GetCategories(false);
            categoryBindingSource.DataSource = categoryDataObject;
            
            // get the categories for the data grid's category combo box
            dataGridCategoryDataObject = CategoriesController.GetCategories(false);
            dataGridCategoryBindingSource.DataSource = dataGridCategoryDataObject;

            // get the accounts for the data grid's account combo box
            dataGridAccountDataObject = AccountsController.GetAccounts(false);
            dataGridAccountBindingSource.DataSource = dataGridAccountDataObject;
            
            // set the selected index, and load the grid's data source
            cboCategories.SelectedIndex = 0;
            LoadGoalBindingSource();
        }        

        /// <summary>
        /// Event handler for when the selected index has changed for the cboCategories drop down box
        /// Updates the goals binding source with latest data
        /// </summary>
        private void cboCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGoalBindingSource();
        }

        /// <summary>
        /// Event handler for when the refresh button is clicked.
        /// Updates the goals binding source with latest data
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGoalBindingSource();
        }

        /// <summary>
        /// Populates the goalBindingSource with latest data
        /// </summary>
        private void LoadGoalBindingSource()
        {
            if (cboCategories.SelectedValue != null)
            {
                goalDataObject = CategoriesController.GetFilteredGoals ((string)cboCategories.SelectedValue, true);
                goalBindingSource.DataSource = goalDataObject;
            }            
        }
    }
}
