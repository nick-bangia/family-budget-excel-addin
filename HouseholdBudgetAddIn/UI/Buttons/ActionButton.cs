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
        public int index { get; set; }
        public LineItemActions action { get; set; }
        public int listObjectIndex { get; set; }
        public DataWorksheetType worksheetType { get; set; }

        public event EventHandler<ActionEventArgs> OnActionRequested;
        
        protected override void OnClick(EventArgs e)
        {
            ActionEventArgs actionArgs = new ActionEventArgs();
            actionArgs.Index = this.index;
            actionArgs.Action = this.action;
            actionArgs.ListIndex = this.listObjectIndex;
            actionArgs.worksheetType = this.worksheetType;

            if (OnActionRequested != null)
            {
                // fire the event
                OnActionRequested(this, actionArgs);
            }
        }
    }
}
