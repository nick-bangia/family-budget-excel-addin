using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Domain;
using VstoExcel = Microsoft.Office.Tools.Excel;
using NativeExcel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using HouseholdBudget.UI;
using HouseholdBudget.Events;
using HouseholdBudget.Enums;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Utilities;

namespace HouseholdBudget
{
    internal static class ImportResultsManager
    {
        #region Properties
        internal static VstoExcel.Worksheet vstoImportResults;
        internal static VstoExcel.ListObject importResultsList;
        internal static List<LineItem> importResults;
        internal static Dictionary<int, ActionButton> buttons = new Dictionary<int, ActionButton>();
        internal const int NUM_HEADER_ROWS = 1;
        internal const int IMPORT_ACTION_COLUMN = 1;
        #endregion

        public static void DisplayImportResults(List<LineItem> importReport)
        {
            // setup the variables and add a new worksheet
            importResults = importReport;
            GetImportResultsSheet();

            // disable screen updating, events, & alerts
            Controller.ToggleUpdatingAndAlerts(false);
                        
            importResultsList = vstoImportResults.Controls.AddListObject(vstoImportResults.get_Range(Properties.Resources.ImportResultsListObjectRange),
                Properties.Resources.ImportResultsListObjectName);

            // set up headers for the new list object
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.RESULT].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.RESULT);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.DATE].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.DATE);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.DESCRIPTION].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.DESCRIPTION);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.AMOUNT].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.AMOUNT);

            // fill in data as an array
            int rows = importReport.Count;
            int columns = importResultsList.HeaderRowRange.Columns.Count;

            var data = new object[rows, columns];
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    data[row - 1, col - 1] = GetDataValue(row - 1, col, importReport);
                }
            }

            // size the list object appropriately
            importResultsList.Resize(
                vstoImportResults.Range[Properties.Resources.ImportResultsTopLeftRange,
                                        Properties.Resources.ImportResultsRightMostColumn + "$" + (rows + 1).ToString()]);
                                    
            // update data range of list object
            importResultsList.DataBodyRange.Value2 = data;

            // fill in import buttons
            for (int i = 0; i < importReport.Count; i++)
            {
                ActionButton importActionButton = AddButtonToListObject(vstoImportResults, importResultsList, ImportResultActions.IMPORT, 
                    i, i + 1, IMPORT_ACTION_COLUMN, Controller.importReport_ActionRequested);

                // add buttons to the button dictionary
                buttons.Add(i + 1, importActionButton);
            }

            // autofit the list object
            importResultsList.Range.Columns.AutoFit();

            // enable screen updating, events, & alerts
            Controller.ToggleUpdatingAndAlerts(true);
        }

        private static object GetDataValue(int index, int colNum, List<LineItem> importReport)
        {
            object value;

            // switch through colNum, and provide the correct data point based on the row index
            switch (colNum)
            {
                case (int)ImportResultsColumns.RESULT:
                    value = importReport[index].Status.ToString();
                    break;
                case (int)ImportResultsColumns.DATE:
                    value = importReport[index].Date.ToShortDateString();
                    break;
                case (int)ImportResultsColumns.DESCRIPTION:
                    value = importReport[index].Description;
                    break;
                case (int)ImportResultsColumns.AMOUNT:
                    value = importReport[index].Amount;
                    break;
                default:
                    value = "N/A";
                    break;
            }

            return value;
        }

        public static LineItem ConvertImportResultToLineItem(int listIndex, int resultsIndex)
        {
            // convert the corresponding row in the list object to a LineItem
            LineItem item = new LineItem();
            item.Status = (LineItemStatus)Enum.Parse(typeof(LineItemStatus), 
                importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.RESULT].Value2);
            item.setDate(DateTime.FromOADate(importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.DATE].Value2));
            item.Description = importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.DESCRIPTION].Value2;
            item.Amount = Convert.ToDecimal(importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.AMOUNT].Value2);

            // update the import results report with the updated item, before it is attempted to be imported again
            importResults[resultsIndex] = item;

            // return the converted line item
            return item;
        }

        public static void ProcessImportResult(LineItem item, int listIndex, int resultsIndex)
        {
            // update the status of the imported item on the list
            importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.RESULT].Value2 = item.Status.ToString();

            // update the status of the imported item in the report
            importResults[resultsIndex].Status = item.Status;

            // update the button's enabled status based on the new status
            string buttonName = "btn" + ImportResultActions.IMPORT.ToString() + resultsIndex.ToString();
            ((ActionButton)vstoImportResults.Controls[buttonName]).Enabled = item.Status != LineItemStatus.SAVED;
        }

        public static void RemoveSheet()
        {
            if (vstoImportResults != null)
            {
                vstoImportResults.Delete();
                vstoImportResults = null;
                importResultsList = null;
                importResults = null;
                buttons.Clear();
            }
        }

        private static ActionButton AddButtonToListObject(VstoExcel.Worksheet worksheet, 
                                                          VstoExcel.ListObject listObject, 
                                                          ImportResultActions action, 
                                                          int resultIndex, 
                                                          int listObjectIndex, 
                                                          int column, 
                                                          EventHandler<ImportActionEventArgs> handler)
        {
            // create the button & set its properties
            ActionButton actionButton = new ActionButton();
            actionButton.ImportResultIndex = resultIndex;
            actionButton.ImportResultAction = action;
            actionButton.ImportResultListObjectIndex = listObjectIndex;
            //actionButton.column = column;
            actionButton.Text = EnumUtil.GetFriendlyName(action);
            string buttonName = "btn" + action.ToString() + (resultIndex).ToString();
            actionButton.Name = buttonName;
                        
            // add it to the list
            int rowIndex = listObjectIndex + NUM_HEADER_ROWS;
            worksheet.Controls.AddControl(actionButton, worksheet.Cells[rowIndex, column], buttonName);
            
            // tie it to the handler
            actionButton.OnActionRequested += handler;

            return actionButton;
        }

        private static void GetImportResultsSheet()
        {
            NativeExcel.Worksheet importResultsSheet;

            NativeExcel.Sheets worksheets = Globals.ThisAddIn.Application.Worksheets;
            // delete the import results worksheet if it already exists
            foreach (NativeExcel.Worksheet worksheet in worksheets)
            {
                if (worksheet.Name == Properties.Resources.ImportResultsWorksheetName)
                {
                    worksheet.Delete();
                    break;
                }
            }

            NativeExcel.Worksheet lastWorksheet = worksheets[worksheets.Count];
            importResultsSheet = (NativeExcel.Worksheet)Globals.ThisAddIn.Application.Worksheets.Add(After: lastWorksheet);
            importResultsSheet.Name = Properties.Resources.ImportResultsWorksheetName;
            vstoImportResults = Globals.Factory.GetVstoObject(importResultsSheet);
        }
    }
}
