﻿using System;
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

        internal static void DisplayImportResults(List<LineItem> importReport)
        {
            // setup the variables and add a new worksheet
            importResults = importReport;
            GetImportResultsSheet();
                        
            importResultsList = vstoImportResults.Controls.AddListObject(vstoImportResults.get_Range(Properties.Resources.ImportResultsListObjectRange),
                Properties.Resources.ImportResultsListObjectName);

            // set up headers for the new list object
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.RESULT].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.RESULT);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.DATE].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.DATE);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.DESCRIPTION].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.DESCRIPTION);
            importResultsList.HeaderRowRange[1, (int)ImportResultsColumns.AMOUNT].Value2 = EnumUtil.GetFriendlyName(ImportResultsColumns.AMOUNT);

            // fill in import results
            for (int i = 0; i < importReport.Count; i++)
            {
                importResultsList.ListRows.AddEx();
                int rowNum = importResultsList.ListRows.Count;
                importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.RESULT].Value2 = importReport[i].Status.ToString();
                importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.DATE].Value2 = importReport[i].Date.ToShortDateString();
                importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.DESCRIPTION].Value2 = importReport[i].Description;
                importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.AMOUNT].Value2 = importReport[i].Amount.ToString();
                // make the amount column a currency field
                importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.AMOUNT].Style = "Currency";

                ActionButton importActionButton = AddButtonToListObject(vstoImportResults, importResultsList, ImportResultActions.IMPORT, 
                    i, rowNum, IMPORT_ACTION_COLUMN, Controller.importReport_ActionRequested);

                // add buttons to the button dictionary
                buttons.Add(rowNum, importActionButton);
            }

            // autofit the list object
            importResultsList.Range.Columns.AutoFit();
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

        internal static LineItem ConvertImportResultToLineItem(int listIndex, int resultsIndex)
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

        internal static void ProcessImportResult(LineItem item, int listIndex, int resultsIndex)
        {
            // update the status of the imported item on the list
            importResultsList.DataBodyRange.Cells[listIndex, (int)ImportResultsColumns.RESULT].Value2 = item.Status.ToString();

            // update the status of the imported item in the report
            importResults[resultsIndex].Status = item.Status;

            // update the button's enabled status based on the new status
            string buttonName = "btn" + ImportResultActions.IMPORT.ToString() + resultsIndex.ToString();
            ((ActionButton)vstoImportResults.Controls[buttonName]).Enabled = item.Status != LineItemStatus.SAVED;
        }

        //internal static void DeleteImportResult(int listIndex, int resultsIndex)
        //{
        //    int totalRows = importResultsList.ListRows.Count;

        //    // if the list index is within the list (not the last row), then iterate through each following row and move it up one
        //    if (listIndex < totalRows)
        //    {
        //        int currentRow = listIndex + 1;
        //        int toRow = listIndex;

        //        while (currentRow <= totalRows)
        //        {
        //            // move the next row down up to the row being deleted
        //            MoveRow(currentRow, toRow);
        //            currentRow += 1;
        //            toRow += 1;
        //        }
        //    }

        //    // delete the last row before resizing the list object
        //    DeleteLastRow();

        //    // resize the list object to remove the last row, if more than 1 row exists
        //    if (totalRows != 1)
        //    {
        //        importResultsList.Resize(
        //            vstoImportResults.Range[Properties.Resources.ImportResultsTopLeftRange,
        //                                    Properties.Resources.ImportResultsRightMostColumn + "$" + totalRows.ToString()]);
        //    }
        //    else
        //    {
        //        // otherwise, if only 1 row left, delete the worksheet
        //        Globals.ThisAddIn.Application.DisplayAlerts = false;
        //        vstoImportResults.Delete();
        //        Globals.ThisAddIn.Application.DisplayAlerts = true;
        //    }
        //}

        //private static void DeleteLastRow()
        //{
        //    int rowNum = importResultsList.ListRows.Count;

        //    // null out row's content
        //    importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.RESULT].Value2 = null;
        //    importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.DATE].Value2 = null;
        //    importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.DESCRIPTION].Value2 = null;
        //    importResultsList.DataBodyRange.Cells[rowNum, (int)ImportResultsColumns.AMOUNT].Value2 = null;

        //    RemoveLastRowButtons(rowNum);
        //}

        //private static void MoveRow(int currentRow, int toRow)
        //{
        //    // copy content from currentRow to toRow
        //    importResultsList.DataBodyRange.Cells[toRow, (int)ImportResultsColumns.RESULT].Value2       = importResultsList.DataBodyRange.Cells[currentRow, (int)ImportResultsColumns.RESULT].Value2;
        //    importResultsList.DataBodyRange.Cells[toRow, (int)ImportResultsColumns.DATE].Value2         = importResultsList.DataBodyRange.Cells[currentRow, (int)ImportResultsColumns.DATE].Value2;
        //    importResultsList.DataBodyRange.Cells[toRow, (int)ImportResultsColumns.DESCRIPTION].Value2  = importResultsList.DataBodyRange.Cells[currentRow, (int)ImportResultsColumns.DESCRIPTION].Value2;
        //    importResultsList.DataBodyRange.Cells[toRow, (int)ImportResultsColumns.AMOUNT].Value2       = importResultsList.DataBodyRange.Cells[currentRow, (int)ImportResultsColumns.AMOUNT].Value2;

        //    // replace buttons in toRow with buttons from currentRow
        //    ReassignButtons(currentRow, toRow);

        //    string buttonName = "btn" + ImportResultActions.IMPORT.ToString() + (toRow - 1).ToString();
        //    if (importResultsList.DataBodyRange.Cells[toRow, (int)ImportResultsColumns.RESULT].Value2 !=
        //        LineItemStatus.SAVED.ToString())
        //    {
        //        ((ActionButton)vstoImportResults.Controls[buttonName]).Enabled = true;
        //    }
        //    else
        //    {
        //        ((ActionButton)vstoImportResults.Controls[buttonName]).Enabled = false;
        //    }
        //}

        //private static void ReassignButtons(int currentRow, int toRow)
        //{
        //    // get the buttons that need to be moved
        //    List<ActionButton> buttonsToMoveUp = buttons[currentRow];
        //    List<ActionButton> buttonsToMoveDown = buttons[toRow];

        //    // swap buttons
        //    buttons[toRow] = buttonsToMoveUp;
        //    buttons[currentRow] = buttonsToMoveDown;

        //    // reassign the listIndex and resultIndex
        //    foreach (ActionButton button in buttonsToMoveUp)
        //    {
        //        ActionButton oldButton = (buttonsToMoveDown.Where(b => b.ImportResultAction == button.ImportResultAction)).First();
        //        button.ImportResultIndex = oldButton.ImportResultIndex;
        //        button.ImportResultListObjectIndex = oldButton.ImportResultListObjectIndex;
        //    }
        //}

        //private static void RemoveLastRowButtons(int rowNum)
        //{
        //    foreach (ActionButton button in buttons[rowNum])
        //    {
        //        // remove the button from the controls list
        //        string name = "btn" + button.ImportResultAction.ToString() + (rowNum - 1).ToString();
        //        vstoImportResults.Controls.Remove(name);
        //    }
        //    // delete the entry in the dictionary
        //    buttons.Remove(rowNum);
        //}

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
    }
}
