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
            this.btnAddNewItems = this.Factory.CreateRibbonButton();
            this.btnPreProcessItems = this.Factory.CreateRibbonButton();
            this.btnSave = this.Factory.CreateRibbonButton();
            this.grpData = this.Factory.CreateRibbonGroup();
            this.btnRefresh = this.Factory.CreateRibbonButton();
            this.btnGetPendingItems = this.Factory.CreateRibbonButton();
            this.btnGetFutureItems = this.Factory.CreateRibbonButton();
            this.btnSearch = this.Factory.CreateRibbonButton();
            this.grpManage = this.Factory.CreateRibbonGroup();
            this.btnAddCategory = this.Factory.CreateRibbonButton();
            this.btnAddSubCategory = this.Factory.CreateRibbonButton();
            this.btnNewGoal = this.Factory.CreateRibbonButton();
            this.btnUpdateCategories = this.Factory.CreateRibbonButton();
            this.btnManagePaymentMethods = this.Factory.CreateRibbonButton();
            this.tabFamilyBudget.SuspendLayout();
            this.grpNewItems.SuspendLayout();
            this.grpData.SuspendLayout();
            this.grpManage.SuspendLayout();
            // 
            // tabFamilyBudget
            // 
            this.tabFamilyBudget.Groups.Add(this.grpNewItems);
            this.tabFamilyBudget.Groups.Add(this.grpData);
            this.tabFamilyBudget.Groups.Add(this.grpManage);
            this.tabFamilyBudget.Label = "Family Budget";
            this.tabFamilyBudget.Name = "tabFamilyBudget";
            this.tabFamilyBudget.Visible = false;
            // 
            // grpNewItems
            // 
            this.grpNewItems.Items.Add(this.btnAddNewItems);
            this.grpNewItems.Items.Add(this.btnPreProcessItems);
            this.grpNewItems.Items.Add(this.btnSave);
            this.grpNewItems.Label = "New Items";
            this.grpNewItems.Name = "grpNewItems";
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
            // grpData
            // 
            this.grpData.Items.Add(this.btnRefresh);
            this.grpData.Items.Add(this.btnGetPendingItems);
            this.grpData.Items.Add(this.btnGetFutureItems);
            this.grpData.Items.Add(this.btnSearch);
            this.grpData.Label = "Data";
            this.grpData.Name = "grpData";
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
            this.btnGetPendingItems.Label = "Pending Items";
            this.btnGetPendingItems.Name = "btnGetPendingItems";
            // 
            // btnGetFutureItems
            // 
            this.btnGetFutureItems.Label = "Future Items";
            this.btnGetFutureItems.Name = "btnGetFutureItems";
            // 
            // btnSearch
            // 
            this.btnSearch.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSearch.Label = "Search";
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.OfficeImageId = "PrintPreviewZoomMenu";
            this.btnSearch.ShowImage = true;
            // 
            // grpManage
            // 
            this.grpManage.Items.Add(this.btnAddCategory);
            this.grpManage.Items.Add(this.btnAddSubCategory);
            this.grpManage.Items.Add(this.btnNewGoal);
            this.grpManage.Items.Add(this.btnUpdateCategories);
            this.grpManage.Items.Add(this.btnManagePaymentMethods);
            this.grpManage.Label = "Manage";
            this.grpManage.Name = "grpManage";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddCategory.Label = "New Category";
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.OfficeImageId = "AddAccount";
            this.btnAddCategory.ShowImage = true;
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
            this.btnUpdateCategories.Label = "Update Categories";
            this.btnUpdateCategories.Name = "btnUpdateCategories";
            this.btnUpdateCategories.OfficeImageId = "EditItem";
            this.btnUpdateCategories.ShowImage = true;
            // 
            // btnManagePaymentMethods
            // 
            this.btnManagePaymentMethods.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnManagePaymentMethods.Label = "Payment Methods";
            this.btnManagePaymentMethods.Name = "btnManagePaymentMethods";
            this.btnManagePaymentMethods.OfficeImageId = "AccountingFormat";
            this.btnManagePaymentMethods.ShowImage = true;
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnGetFutureItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpNewItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPreProcessItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnNewGoal;
    }

    partial class ThisRibbonCollection
    {
        internal FamilyBudgetRibbon FamilyBudgetRibbon
        {
            get { return this.GetRibbon<FamilyBudgetRibbon>(); }
        }
    }
}
