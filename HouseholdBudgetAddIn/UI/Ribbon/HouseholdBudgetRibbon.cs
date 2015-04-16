using Microsoft.Office.Tools.Ribbon;
using HouseholdBudget.Controllers;
using HouseholdBudget.Utilities;

namespace HouseholdBudget.UI
{
    public partial class HouseholdBudgetRibbon
    {
        private void HouseholdBudgetRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Line Items
            this.btnAddNewItems.Click += new RibbonControlEventHandler(LineItemsController.btnAddNewItems_Click);
            this.btnGetPendingItems.Click += new RibbonControlEventHandler(LineItemsController.btnGetPendingItems_Click);
            this.btnGetFutureItems.Click += new RibbonControlEventHandler(LineItemsController.btnGetFutureItems_Click);
            this.btnSearch.Click += new RibbonControlEventHandler(LineItemsController.btnSearch_Click);
            this.btnPreProcessItems.Click += new RibbonControlEventHandler(LineItemsController.btnPreProcessItems_Click);
            this.btnSave.Click += new RibbonControlEventHandler(LineItemsController.btnSave_Click);

            // Categories
            this.btnAddCategory.Click += new RibbonControlEventHandler(CategoriesController.btnAddCategory_Click);
            this.btnAddSubCategory.Click += new RibbonControlEventHandler(CategoriesController.btnAddSubCategory_Click);
            this.btnUpdateCategories.Click += new RibbonControlEventHandler(CategoriesController.btnUpdateCategories_Click);
            this.btnNewGoal.Click += new RibbonControlEventHandler(CategoriesController.btnNewGoal_Click);

            // Payment Methods
            this.btnManagePaymentMethods.Click += new RibbonControlEventHandler(PaymentMethodsController.btnManagePaymentMethods_Click);

            // Workbook
            this.btnRefresh.Click += new RibbonControlEventHandler(WorkbookUtil.btnRefresh_Click);
        }
    }
}
