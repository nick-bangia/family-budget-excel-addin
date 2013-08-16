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
            this.grpTools = this.Factory.CreateRibbonGroup();
            this.btnImportStatement = this.Factory.CreateRibbonButton();
            this.btnAddCategory = this.Factory.CreateRibbonButton();
            this.tabHouseholdBudget.SuspendLayout();
            this.grpTools.SuspendLayout();
            // 
            // tabHouseholdBudget
            // 
            this.tabHouseholdBudget.Groups.Add(this.grpTools);
            this.tabHouseholdBudget.Label = "Household Budget";
            this.tabHouseholdBudget.Name = "tabHouseholdBudget";
            this.tabHouseholdBudget.Visible = false;
            // 
            // grpTools
            // 
            this.grpTools.Items.Add(this.btnImportStatement);
            this.grpTools.Items.Add(this.btnAddCategory);
            this.grpTools.Label = "Tools";
            this.grpTools.Name = "grpTools";
            // 
            // btnImportStatement
            // 
            this.btnImportStatement.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnImportStatement.Label = "Import Statement";
            this.btnImportStatement.Name = "btnImportStatement";
            this.btnImportStatement.OfficeImageId = "ImportTextFile";
            this.btnImportStatement.ShowImage = true;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnAddCategory.Label = "New Category";
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.OfficeImageId = "AddAccount";
            this.btnAddCategory.ShowImage = true;
            // 
            // HouseholdBudgetRibbon
            // 
            this.Name = "HouseholdBudgetRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabHouseholdBudget);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.HouseholdBudgetRibbon_Load);
            this.tabHouseholdBudget.ResumeLayout(false);
            this.tabHouseholdBudget.PerformLayout();
            this.grpTools.ResumeLayout(false);
            this.grpTools.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHouseholdBudget;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImportStatement;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddCategory;
    }

    partial class ThisRibbonCollection
    {
        internal HouseholdBudgetRibbon HouseholdBudgetRibbon
        {
            get { return this.GetRibbon<HouseholdBudgetRibbon>(); }
        }
    }
}
