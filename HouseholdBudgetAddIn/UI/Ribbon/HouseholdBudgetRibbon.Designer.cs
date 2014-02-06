using HouseholdBudget.UI;

namespace HouseholdBudget.UI
{
    partial class HouseholdBudgetRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public HouseholdBudgetRibbon()
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
            this.tabHouseholdBudget = this.Factory.CreateRibbonTab();
            this.grpData = this.Factory.CreateRibbonGroup();
            this.btnImport = this.Factory.CreateRibbonButton();
            this.btnRefresh = this.Factory.CreateRibbonButton();
            this.grpManage = this.Factory.CreateRibbonGroup();
            this.btnAddCategory = this.Factory.CreateRibbonButton();
            this.btnAddSubCategory = this.Factory.CreateRibbonButton();
            this.btnUpdateCategories = this.Factory.CreateRibbonButton();
            this.btnManagePaymentMethods = this.Factory.CreateRibbonButton();
            this.tabHouseholdBudget.SuspendLayout();
            this.grpData.SuspendLayout();
            this.grpManage.SuspendLayout();
            // 
            // tabHouseholdBudget
            // 
            this.tabHouseholdBudget.Groups.Add(this.grpData);
            this.tabHouseholdBudget.Groups.Add(this.grpManage);
            this.tabHouseholdBudget.Label = "Household Budget";
            this.tabHouseholdBudget.Name = "tabHouseholdBudget";
            this.tabHouseholdBudget.Visible = false;
            // 
            // grpData
            // 
            this.grpData.Items.Add(this.btnImport);
            this.grpData.Items.Add(this.btnRefresh);
            this.grpData.Label = "Data";
            this.grpData.Name = "grpData";
            // 
            // btnImport
            // 
            this.btnImport.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnImport.Label = "Import ";
            this.btnImport.Name = "btnImport";
            this.btnImport.OfficeImageId = "ImportTextFile";
            this.btnImport.ShowImage = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnRefresh.Label = "Refresh";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OfficeImageId = "Refresh";
            this.btnRefresh.ShowImage = true;
            // 
            // grpManage
            // 
            this.grpManage.Items.Add(this.btnAddCategory);
            this.grpManage.Items.Add(this.btnAddSubCategory);
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
            // HouseholdBudgetRibbon
            // 
            this.Name = "HouseholdBudgetRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabHouseholdBudget);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.HouseholdBudgetRibbon_Load);
            this.tabHouseholdBudget.ResumeLayout(false);
            this.tabHouseholdBudget.PerformLayout();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.grpManage.ResumeLayout(false);
            this.grpManage.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHouseholdBudget;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImport;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddCategory;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpdateCategories;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpManage;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddSubCategory;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnManagePaymentMethods;
    }

    partial class ThisRibbonCollection
    {
        internal HouseholdBudgetRibbon HouseholdBudgetRibbon
        {
            get { return this.GetRibbon<HouseholdBudgetRibbon>(); }
        }
    }
}
