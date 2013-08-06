using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using System.IO;
using HomeBudget.Tools;
using System.Configuration;
using HomeBudget.Data;
using HomeBudget.Data.Interfaces;

namespace HomeBudget.UI
{
    public partial class HomeBudgetRibbon
    {
        private void HomeBudgetRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            this.btnImportStatement.Click += new RibbonControlEventHandler(Controller.btnImportStatement_Click);
            this.btnAddCategory.Click += new RibbonControlEventHandler(Controller.btnAddCategory_Click);
        }
    }
}
