using Microsoft.Office.Tools.Ribbon;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.Utilities;

namespace FamilyBudget.AddIn.UI
{
    public partial class FamilyBudgetRibbon
    {
        private void FamilyBudgetRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Line Items
            this.btnAddNewItems.Click += new RibbonControlEventHandler(LineItemsController.btnAddNewItems_Click);
            this.btnGetPendingItems.Click += new RibbonControlEventHandler(LineItemsController.btnGetPendingItems_Click);
            this.btnSearch.Click += new RibbonControlEventHandler(LineItemsController.btnSearch_Click);
            this.btnPreProcessItems.Click += new RibbonControlEventHandler(LineItemsController.btnPreProcessItems_Click);
            this.btnSave.Click += new RibbonControlEventHandler(LineItemsController.btnSave_Click);
            this.btnAddJournalEntries.Click += new RibbonControlEventHandler(LineItemsController.btnAddJournalEntries_Click);

            // Categories
            this.btnAddCategory.Click += new RibbonControlEventHandler(CategoriesController.btnAddCategory_Click);
            this.btnAddSubCategory.Click += new RibbonControlEventHandler(CategoriesController.btnAddSubCategory_Click);
            this.btnUpdateCategories.Click += new RibbonControlEventHandler(CategoriesController.btnUpdateCategories_Click);
            this.btnNewGoal.Click += new RibbonControlEventHandler(CategoriesController.btnNewGoal_Click);
            this.btnUpdateGoals.Click += new RibbonControlEventHandler(CategoriesController.btnUpdateGoals_Click);

            // Payment Methods
            this.btnManagePaymentMethods.Click += new RibbonControlEventHandler(PaymentMethodsController.btnManagePaymentMethods_Click);

            // Accounts
            this.btnManageAccounts.Click += new RibbonControlEventHandler(AccountsController.btnManageAccounts_Click);

            // Workbook
            this.btnRefresh.Click += new RibbonControlEventHandler(WorkbookUtil.btnRefresh_Click);
            this.btnRefreshToken.Click += new RibbonControlEventHandler(WorkbookUtil.btnRefreshToken_Click);
        }
    }
}
