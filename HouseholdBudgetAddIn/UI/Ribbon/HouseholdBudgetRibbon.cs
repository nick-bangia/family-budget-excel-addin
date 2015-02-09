using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using System.IO;
//using HouseholdBudget.Tools;
using System.Configuration;
using HouseholdBudget.Data;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Enums;

namespace HouseholdBudget.UI
{
    public partial class HouseholdBudgetRibbon
    {
        private void HouseholdBudgetRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this.btnAddNewItems.Click += new RibbonControlEventHandler(Controller.btnAddNewItems_Click);
            this.btnAddCategory.Click += new RibbonControlEventHandler(Controller.btnAddCategory_Click);
            this.btnAddSubCategory.Click += new RibbonControlEventHandler(Controller.btnAddSubCategory_Click);
            this.btnRefresh.Click += new RibbonControlEventHandler(Controller.btnRefresh_Click);
            this.btnUpdateCategories.Click += new RibbonControlEventHandler(Controller.btnUpdateCategories_Click);
            this.btnManagePaymentMethods.Click += new RibbonControlEventHandler(Controller.btnManagePaymentMethods_Click);
            this.btnGetPendingItems.Click += new RibbonControlEventHandler(Controller.btnGetPendingItems_Click);
            this.btnGetFutureItems.Click += new RibbonControlEventHandler(Controller.btnGetFutureItems_Click);
            this.btnSearch.Click += new RibbonControlEventHandler(Controller.btnSearch_Click);
            this.btnPreProcessItems.Click += new RibbonControlEventHandler(Controller.btnPreProcessItems_Click);
            this.btnSave.Click += new RibbonControlEventHandler(Controller.btnSave_Click);
            this.btnNewGoal.Click += new RibbonControlEventHandler(Controller.btnNewGoal_Click);
        }
    }
}
