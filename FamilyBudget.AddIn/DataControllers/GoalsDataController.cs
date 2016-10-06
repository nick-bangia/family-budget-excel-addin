using System.ComponentModel;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.UI;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Utilities;
using log4net;
using NativeExcel = Microsoft.Office.Interop.Excel;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace FamilyBudget.AddIn.DataControllers
{
    internal static class GoalsDataController
    {
        #region Properties
        private static VstoExcel.Worksheet vstoDataSheet = null;
        private static VstoExcel.ListObject goalsListObject = null;
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_GoalsManager");
        #endregion

        public static void PopulateGoalsDataTable(BindingList<GoalSummary> goals)
        {
            // log the status
            logger.Info("Beginning population of data sheet...");

            // Get the data sheet into the vstoDataSheet
            GetDataSheet();

            // disable screen updating, events, & alerts
            WorkbookUtil.ToggleUpdatingAndAlerts(false);

            if (goalsListObject == null)
            {
                goalsListObject = vstoDataSheet.Controls.AddListObject(vstoDataSheet.get_Range(Properties.Resources.GoalsListObjectRange),
                    Properties.Resources.GoalsListObjectName);

                // set up headers for list object
                logger.Info("Setting up headers.");

                goalsListObject.HeaderRowRange[1, (int)GoalDataColumns.NAME].Value2 = EnumUtil.GetFriendlyName(GoalDataColumns.NAME);
                goalsListObject.HeaderRowRange[1, (int)GoalDataColumns.TOTAL_SAVED].Value2 = EnumUtil.GetFriendlyName(GoalDataColumns.TOTAL_SAVED);
                goalsListObject.HeaderRowRange[1, (int)GoalDataColumns.GOAL_AMOUNT].Value2 = EnumUtil.GetFriendlyName(GoalDataColumns.GOAL_AMOUNT);
                goalsListObject.HeaderRowRange[1, (int)GoalDataColumns.TARGET_COMPLETION].Value2 = EnumUtil.GetFriendlyName(GoalDataColumns.TARGET_COMPLETION);
            }
            else
            {
                logger.Info("Clearing current data.");

                if (goalsListObject.DataBodyRange != null)
                {
                    goalsListObject.DataBodyRange.Clear();
                }
                goalsListObject.Resize(
                    vstoDataSheet.Range[Properties.Resources.GoalsListTopLeftRange,
                                        Properties.Resources.GoalsListBottomRightRange]);
            }

            // fill in data as an array
            logger.Info("Creating data matrix.");
            
            int rows = goals.Count;
            int columns = goalsListObject.HeaderRowRange.Columns.Count;
                        
            var data = new object[rows, columns];
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    data[row - 1, col - 1] = GetDataValue(row - 1, col, goals);                    
                }
            }

            // if there is only 1 row, increase it by 1, to avoid a null data body range
            bool oneRow = false;
            if (rows == 1)
            {
                rows += 1;
                oneRow = true;
            }

            // size the list object appropriately
            goalsListObject.Resize(
                vstoDataSheet.Range[Properties.Resources.GoalsListTopLeftRange,
                                    Properties.Resources.GoalsListRightMostColumn + "$" + (rows + 1).ToString()]);
            
            // apply data if it exists
            if (rows > 0)
            {
                // update the data range of list object
                logger.Info("Applying data to worksheet.");
                goalsListObject.DataBodyRange.Value2 = data;

                // autofit the list object
                goalsListObject.Range.Columns.AutoFit();
            }

            // delete the last row, if only 1 row was entered
            if (oneRow)
            {
                vstoDataSheet.Rows[rows + 1].Delete();
            }

            // enable screen updating, events, & alerts
            WorkbookUtil.ToggleUpdatingAndAlerts(true);

            // log completion
            logger.Info("Completed population of data sheet.");
        }

        public static void RemoveSheet()
        {
            if (vstoDataSheet != null)
            {
                vstoDataSheet.Delete();
                vstoDataSheet = null;
                goalsListObject = null;
            }
        }

        private static object GetDataValue(int index, int colNum, BindingList<GoalSummary> goals)
        {
            object value;

            // switch through the colNum, and provide the correct data point based on the row index
            switch (colNum)
            {
                case (int)GoalDataColumns.NAME:
                    value = goals[index].Name;
                    break;
                case (int)GoalDataColumns.TOTAL_SAVED:
                    value = goals[index].TotalSaved;
                    break;
                case (int)GoalDataColumns.GOAL_AMOUNT:
                    value = goals[index].GoalAmount;
                    break;
                case (int)GoalDataColumns.TARGET_COMPLETION:
                    value = goals[index].TargetCompletionDate.ToShortDateString();
                    break;
                default:
                    value = "N/A";
                    break;
            }

            return value;
        }

        private static void GetDataSheet()
        {
            if (vstoDataSheet == null)
            {
                NativeExcel.Worksheet dataSheet = null;

                // loop through current worksheets, and look for data sheet
                NativeExcel.Sheets worksheets = Globals.ThisAddIn.Application.Worksheets;
                foreach (NativeExcel.Worksheet wrksheet in worksheets)
                {
                    if (wrksheet.Name == Properties.Resources.GoalsDataWorksheetName)
                    {
                        // if found, assign it to the dataSheet
                        dataSheet = wrksheet;
                        break;
                    }
                }

                // if it doesn't exist, create it
                if (dataSheet == null)
                {
                    NativeExcel.Worksheet lastWorksheet = worksheets[worksheets.Count];
                    dataSheet = (NativeExcel.Worksheet)Globals.ThisAddIn.Application.Worksheets.Add(After: lastWorksheet);
                    dataSheet.Name = Properties.Resources.GoalsDataWorksheetName;
                }

                // assign the VSTO object for it to the vstoDataSheet property
                vstoDataSheet = Globals.Factory.GetVstoObject(dataSheet);
            }           
        }
    }
}
