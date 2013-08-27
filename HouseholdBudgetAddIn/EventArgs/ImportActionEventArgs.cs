using System;
using System.Collections.Generic;
using HouseholdBudget.Enums;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace HouseholdBudget.Events
{
    internal class ImportActionEventArgs : EventArgs
    {
        public int ImportResultsIndex { get; set; }
        public ImportResultActions ImportAction { get; set; }
        public int ImportListIndex { get; set; }
    }
}
