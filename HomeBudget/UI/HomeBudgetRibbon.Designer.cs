using HomeBudget;

namespace HomeBudget.UI
{
    partial class HomeBudgetRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public HomeBudgetRibbon()
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
            this.tabHomeBudget = this.Factory.CreateRibbonTab();
            this.grpTools = this.Factory.CreateRibbonGroup();
            this.btnImportStatement = this.Factory.CreateRibbonButton();
            this.btnAddCategory = this.Factory.CreateRibbonButton();
            this.tabHomeBudget.SuspendLayout();
            this.grpTools.SuspendLayout();
            // 
            // tabHomeBudget
            // 
            this.tabHomeBudget.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabHomeBudget.Groups.Add(this.grpTools);
            this.tabHomeBudget.Label = "Home Budget";
            this.tabHomeBudget.Name = "tabHomeBudget";
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
            // HomeBudgetRibbon
            // 
            this.Name = "HomeBudgetRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tabHomeBudget);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.HomeBudgetRibbon_Load);
            this.tabHomeBudget.ResumeLayout(false);
            this.tabHomeBudget.PerformLayout();
            this.grpTools.ResumeLayout(false);
            this.grpTools.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHomeBudget;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnImportStatement;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddCategory;
    }

    partial class ThisRibbonCollection
    {
        internal HomeBudgetRibbon HomeBudgetRibbon
        {
            get { return this.GetRibbon<HomeBudgetRibbon>(); }
        }
    }
}
