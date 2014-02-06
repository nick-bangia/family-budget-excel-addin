using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Excel.Controls;
using VstoExcel = Microsoft.Office.Tools.Excel;
using HouseholdBudget.Events;
using HouseholdBudget.Enums;

namespace HouseholdBudget.UI
{
    internal class ActionButton : Button
    {
        public int ImportResultIndex { get; set; }
        public ImportResultActions ImportResultAction { get; set; }
        public int ImportResultListObjectIndex { get; set; }
        public ImportResultsColumns column { get; set; }

        public event EventHandler<ImportActionEventArgs> OnActionRequested;
        
        protected override void OnClick(EventArgs e)
        {
            ImportActionEventArgs importActionArgs = new ImportActionEventArgs();
            importActionArgs.ImportResultsIndex = this.ImportResultIndex;
            importActionArgs.ImportAction = this.ImportResultAction;
            importActionArgs.ImportListIndex = this.ImportResultListObjectIndex;

            if (OnActionRequested != null)
            {
                // fire the event
                OnActionRequested(this, importActionArgs);
            }
        }
    }
}
