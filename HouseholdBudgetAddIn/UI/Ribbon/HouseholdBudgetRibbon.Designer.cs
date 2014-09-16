﻿using HouseholdBudget.UI;

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
            this.NewItemsGroup = this.Factory.CreateRibbonGroup();
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
            this.btnUpdateCategories = this.Factory.CreateRibbonButton();
            this.btnManagePaymentMethods = this.Factory.CreateRibbonButton();
            this.tabHouseholdBudget.SuspendLayout();
            this.NewItemsGroup.SuspendLayout();
            this.grpData.SuspendLayout();
            this.grpManage.SuspendLayout();
            // 
            // tabHouseholdBudget
            // 
            this.tabHouseholdBudget.Groups.Add(this.NewItemsGroup);
            this.tabHouseholdBudget.Groups.Add(this.grpData);
            this.tabHouseholdBudget.Groups.Add(this.grpManage);
            this.tabHouseholdBudget.Label = "Household Budget";
            this.tabHouseholdBudget.Name = "tabHouseholdBudget";
            this.tabHouseholdBudget.Visible = false;
            // 
            // NewItemsGroup
            // 
            this.NewItemsGroup.Items.Add(this.btnAddNewItems);
            this.NewItemsGroup.Items.Add(this.btnPreProcessItems);
            this.NewItemsGroup.Items.Add(this.btnSave);
            this.NewItemsGroup.Label = "New Items";
            this.NewItemsGroup.Name = "NewItemsGroup";
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
            this.NewItemsGroup.ResumeLayout(false);
            this.NewItemsGroup.PerformLayout();
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.grpManage.ResumeLayout(false);
            this.grpManage.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHouseholdBudget;
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
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup NewItemsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnPreProcessItems;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
    }

    partial class ThisRibbonCollection
    {
        internal HouseholdBudgetRibbon HouseholdBudgetRibbon
        {
            get { return this.GetRibbon<HouseholdBudgetRibbon>(); }
        }
    }
}
