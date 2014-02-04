using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using System.IO;
using HouseholdBudget.Tools;
using System.Configuration;
using HouseholdBudget.Data;
using HouseholdBudget.Data.Interfaces;

namespace HouseholdBudget.UI
{
    public partial class HouseholdBudgetRibbon
    {
        private void HouseholdBudgetRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this.btnImport.Click += new RibbonControlEventHandler(Controller.btnImport_Click);
            this.btnAddCategory.Click += new RibbonControlEventHandler(Controller.btnAddCategory_Click);
            this.btnAddSubCategory.Click += new RibbonControlEventHandler(Controller.btnAddSubCategory_Click);
            this.btnRefresh.Click += new RibbonControlEventHandler(Controller.btnRefresh_Click);
            this.btnUpdateCategories.Click += new RibbonControlEventHandler(Controller.btnUpdateCategories_Click);
        }
    }
}
