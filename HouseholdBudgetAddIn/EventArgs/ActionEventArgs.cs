using System;
using System.Collections.Generic;
using HouseholdBudget.Enums;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace HouseholdBudget.Events
{
    internal class ActionEventArgs : EventArgs
    {
        public int Index { get; set; }
        public LineItemActions Action { get; set; }
        public int ListIndex { get; set; }
        public DataWorksheetType worksheetType { get; set; }
    }
}
