using FamilyBudget.AddIn.UI;

namespace FamilyBudget.AddIn.UI
{
    partial class FamilyBudgetRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public FamilyBudgetRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabFamilyBudget = this.Factory.CreateRibbonTab();
            this.grpNewItems = this.Factory.CreateRibbonGroup();
            this.grpData = this.Factory.CreateRibbonGroup();
            this.grpManage = this.Factory.CreateRibbonGroup();
            this.grpAPIAccess = this.Factory.CreateRibbonGroup();
            this.btnAddNewItems = this.Factory.CreateRibbonButton();
            this.btnPreProcessItems = this.Factory.CreateRibbonButton();
            this.btnSave = this.Factory.CreateRibbonButton();
            this.btnAddJournalEntries = this.Factory.CreateRibbonButton();
            this.btnRefresh = this.Factory.CreateRibbonButton();
            this.btnGetPendingItems = this.Factory.CreateRibbonButton();
            this.btnSearch = this.Factory.CreateRibbonButton();
            this.btnAddSubCategory = this.Factory.CreateRibbonButton();
            this.btnNewGoal = this.Factory.CreateRibbonButton();
            this.btnUpdateCategories = this.Factory.CreateRibbonButton();
            this.btnUpdateGoals = this.Factory.CreateRibbonButton();
            this.btnAddCategory = this.Factory.CreateRibbonButton();
            this.btnManageAccounts = this.Factory.CreateRibbonButton();
            this.btnManagePaymentMethods = this.Factory.CreateRibbonButton();
            this.btnRefreshToken = this.Factory.CreateRibbonButton();
            this.tabFamilyBudget.SuspendLayout();
            this.grpNewItems.SuspendLayout();
            this.grpData.SuspendLayout();
            this.grpManage.SuspendLayout();
            this.grpAPIAccess.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFamilyBudget
            // 
            this.tabFamilyBudget.Groups.Add(this.grpNewItems);
            this.tabFamilyBudget.Groups.Add(this.grpData);
            this.tabFamilyBudget.Groups.Add(this.grpManage);
            this.tabFamilyBudget.Groups.Add(this.grpAPIAccess);
            this.tabFamilyBudget.Label = "Family Budget";
            this.tabFamilyBudget.Name = "tabFamilyBudget";
            this.tabFamilyBudget.Visible = false;
            // 
            // grpNewItems
            // 
            this.grpNewItems.Items.Add(this.btnAddNewItems);
            this.grpNewItems.Items.Add(this.btnPreProcessItems);
            this.grpNewItems.Items.Add(this.btnSave);
            this.grpNewItems.Items.Add(this.btnAddJournalEntries);
            this.grpNewItems.Label = "New Items";
            this.grpNewItems.Name = "grpNewItems";
            // 
            // grpData
            // 
            this.grpData.Items.Add(this.btnRefresh);
            this.grpData.Items.Add(this.btnGetPendingItems);
            this.grpData.Items.Add(this.btnSearch);
            this.grpData.Label = "Data";
            this.grpData.Name = "grpData";
            // 
            // grpManage
            // 
            this.grpManage.Items.Add(this.btnAddSubCategory);
            this.grpManage.Items.Add(this.btnNewGoal);
            this.grpManage.Items.Add(this.btnUpdateCategories);
            this.grpManage.Items.Add(this.btnUpdateGoals);
            this.grpManage.Items.Add(this.btnAddCategory);
            this.grpManage.Items.Add(this.btnManageAccounts);
            this.grpManage.Items.Add(this.btnManagePaymentMethods);
            this.grpManage.Label = "Manage";
            this.grpManage.Name = "grpManage";
            // 
            // grpAPIAccess
            // 
            this.grpAPIAccess.Items.Add(this.btnRefreshToken);
            this.grpAPIAccess.Label = "Access";
            this.grpAPIAccess.Name = "grpAPIAccess";
            // 
            // btnAddNewItems
            // 
            this.btnAddNewItems.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddNewItems.Label = "Add New Items";
            this.btnAddNewItems.Name = "btnAddNewItems";
            this.btnAddNewItems.OfficeImageId = "AddAccount";
            this.btnAddNewItems.ShowImage = true;
            // 
            // btnPreProcessItems
            // 
            this.btnPreProcessItems.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnPreProcessItems.Enabled = false;
            this.btnPreProcessItems.Label = "Pre-Process";
            this.btnPreProcessItems.Name = "btnPreProcessItems";
            this.btnPreProcessItems.OfficeImageId = "QueryRunQuery";
            this.btnPreProcessItems.ShowImage = true;
            // 
            // btnSave
            // 
            this.btnSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSave.Enabled = false;
            this.btnSave.Label = "Save";
            this.btnSave.Name = "btnSave";
            this.btnSave.OfficeImageId = "SaveItem";
            this.btnSave.ShowImage = true;
            // 
            // btnAddJournalEntries
            // 
            this.btnAddJournalEntries.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddJournalEntries.Label = "Add Journal Entries";
            this.btnAddJournalEntries.Name = "btnAddJournalEntries";
            this.btnAddJournalEntries.OfficeImageId = "ListSetNumberingValue";
            this.btnAddJournalEntries.ShowImage = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnRefresh.Label = "Refresh";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OfficeImageId = "Refresh";
            this.btnRefresh.ShowImage = true;
            // 
            // btnGetPendingItems
            // 
            this.btnGetPendingItems.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnGetPendingItems.Label = "Pending Items";
            this.btnGetPendingItems.Name = "btnGetPendingItems";
            this.btnGetPendingItems.OfficeImageId = "RuleLinesMenu";
            this.btnGetPendingItems.ShowImage = true;
            // 
            // btnSearch
            // 
            this.btnSearch.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSearch.Label = "Search";
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.OfficeImageId = "PrintPreviewZoomMenu";
            this.btnSearch.ShowImage = true;
            // 
            // btnAddSubCategory
            // 
            this.btnAddSubCategory.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddSubCategory.Label = "New SubCategory";
            this.btnAddSubCategory.Name = "btnAddSubCategory";
            this.btnAddSubCategory.OfficeImageId = "AddAccount";
            this.btnAddSubCategory.ShowImage = true;
            // 
            // btnNewGoal
            // 
            this.btnNewGoal.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnNewGoal.Label = "New Goal";
            this.btnNewGoal.Name = "btnNewGoal";
            this.btnNewGoal.OfficeImageId = "FlagCustom";
            this.btnNewGoal.ShowImage = true;
            // 
            // btnUpdateCategories
            // 
            this.btnUpdateCategories.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdateCategories.Label = "Update Subcategories";
            this.btnUpdateCategories.Name = "btnUpdateCategories";
            this.btnUpdateCategories.OfficeImageId = "EditItem";
            this.btnUpdateCategories.ShowImage = true;
            // 
            // btnUpdateGoals
            // 
            this.btnUpdateGoals.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpdateGoals.Label = "Update Goals";
            this.btnUpdateGoals.Name = "btnUpdateGoals";
            this.btnUpdateGoals.OfficeImageId = "GroupSummaryActions";
            this.btnUpdateGoals.ShowImage = true;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddCategory.Label = "Categories";
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.OfficeImageId = "CategorizeMenu";
            this.btnAddCategory.ShowImage = true;
            // 
            // btnManageAccounts
            // 
            this.btnManageAccounts.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnManageAccounts.Label = "Accounts";
            this.btnManageAccounts.Name = "btnManageAccounts";
            this.btnManageAccounts.OfficeImageId = "DatabaseSwitchboardManager";
            this.btnManageAccounts.ShowImage = true;
            // 
            // btnManagePaymentMethods
            // 
            this.btnManagePaymentMethods.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnManagePaymentMethods.Label = "Payment Methods";
            this.btnManagePaymentMethods.Name = "btnManagePaymentMethods";
            this.btnManagePaymentMethods.OfficeImageId = "AccountingFormat";
            this.btnManagePaymentMethods.ShowImage = true;
            // 
            // btnRefreshToken
            // 
            this.btnRefreshToken.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnRefreshToken.Label = "Refresh Token";
            this.btnRefreshToken.Name = "btnRefreshToken";
            this.btnRefreshToken.OfficeImageId = "Recurrence";
            this.btnRefreshToken.ShowImage = true;
            // 
            // FamilyBudgetRibbon
            // 
            this.Name = "FamilyBudgetRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabFamilyBudget);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.FamilyBudgetRibbon_Load);
            this.tabFamilyBudget.ResumeLayout(false);
            this.tabFamilyBudget.PerformLayout();
            this.grpNewItems.ResumeLayout(false);
            this.grpNewItems.PerformLayout();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.grpManage.ResumeLayout(false);
            this.grpManage.PerformLayout();
            this.grpAPIAccess.ResumeLayout(false);
            this.grpAPIAccess.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabFamilyBudget;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddNewItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddCategory;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdateCategories;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpManage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddSubCategory;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnManagePaymentMethods;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetPendingItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpNewItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPreProcessItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNewGoal;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnManageAccounts;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpAPIAccess;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRefreshToken;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdateGoals;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddJournalEntries;
    }

    partial class ThisRibbonCollection
    {
        internal FamilyBudgetRibbon FamilyBudgetRibbon
        {
            get { return this.GetRibbon<FamilyBudgetRibbon>(); }
        }
    }
}
