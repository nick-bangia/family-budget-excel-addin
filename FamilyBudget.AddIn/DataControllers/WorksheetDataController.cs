using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Events;
using FamilyBudget.AddIn.UI;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Utilities;
using log4net;
using NativeExcel = Microsoft.Office.Interop.Excel;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace FamilyBudget.AddIn.DataControllers
{
    internal static class WorksheetDataController
    {
        #region Composite Datasheet Object
        private class CompositeDataSheetObject
        {
            public DataWorksheetType worksheetType;
            public VstoExcel.Worksheet vstoWorksheet;
            public VstoExcel.ListObject listObject;
            public List<DenormalizedLineItem> lineItems;
            public string worksheetName;
        }
        #endregion

        #region Properties
        private static List<CompositeDataSheetObject> dataWorksheets = new List<CompositeDataSheetObject>();
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_DataWorksheetManager");
        private static VstoExcel.ListObject lineItemsListObject;
        private static VstoExcel.Worksheet dataSheet;
        private static List<DenormalizedLineItem> lineItems;
        private static Dictionary<DataWorksheetType, Dictionary<int, List<ActionButton>>> buttons = 
            new Dictionary<DataWorksheetType, Dictionary<int, List<ActionButton>>>();
        private const int NUM_HEADER_ROWS = 1;
        private const int EDIT_ACTION_COLUMN = 1;
        private const string amountFormat = "$#,##0.00";
        private const string dateFormat = "MM/DD/YYYY";
        #endregion

        #region Internal Methods
        internal static void PopulateNewWorksheet(DataWorksheetType worksheetType, List<DenormalizedLineItem> lineItemsFromDB = null)
        {
            // log status
            logger.Info(String.Format("Beginning population of {0} data sheet...", EnumUtil.GetFriendlyName(worksheetType)));

            // setup the variables
            lineItems = lineItemsFromDB;
            CompositeDataSheetObject dso = new CompositeDataSheetObject();

            // check if a worksheet already exists for this type. If so, provide user with a message
            DialogResult result;
            string worksheetName;
            bool worksheetExists = DataWorksheetExists(worksheetType, true, out result, out worksheetName);

            // if worksheet doesn't physically exist, then remove it from the composite data object
            if (!String.IsNullOrEmpty(worksheetName))
            {
                dataWorksheets.RemoveAll(cdo => cdo.worksheetName == worksheetName);
            }
            // otherwise, check that the user confirms they want to remove it, and remove it if so
            else if (worksheetExists && result == DialogResult.Yes)
            {
                RemoveSheet(worksheetType);
                worksheetExists = false;
            }
            
            // as long as the worksheet does not exist, work on creating it
            if (!worksheetExists)
            {
                // provision a new worksheet to use
                GetNewDataSheet(worksheetType);
                               
                // disable screen updating, event, and alerts
                WorkbookUtil.ToggleUpdatingAndAlerts(false);

                lineItemsListObject = dataSheet.Controls.AddListObject(dataSheet.get_Range(Properties.Resources.DataWorksheetObjectRange),
                    Properties.Resources.DataWorksheetName + EnumUtil.GetFriendlyName(worksheetType));
                               
                // set up headers
                logger.Info("Setting up headers...");

                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.UNIQUE_ID].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.UNIQUE_ID);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.DATE].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.DATE);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.DESCRIPTION].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.DESCRIPTION);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.AMOUNT].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.AMOUNT);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.CATEGORY].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.CATEGORY);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.SUBCATEGORY].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.SUBCATEGORY);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.TYPE].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.TYPE);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.PAYMENT_METHOD].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.PAYMENT_METHOD);
                lineItemsListObject.HeaderRowRange[1, (int)DataWorksheetColumns.STATUS].Value2 = EnumUtil.GetFriendlyName(DataWorksheetColumns.STATUS);

                // set up the new composite data sheet object worksheet
                dso.vstoWorksheet = dataSheet;
                dso.listObject = lineItemsListObject;
                dso.worksheetType = worksheetType;
                dso.worksheetName = dataSheet.Name;
                dso.lineItems = lineItems;

                // add it back into the collection
                dataWorksheets.Add(dso);

                // if not a new worksheet, fill in data as an array
                if (worksheetType != DataWorksheetType.NEW_ENTRIES)
                {
                    logger.Info("Creating data matrix.");

                    int rows = lineItemsFromDB.Count;
                    int columns = lineItemsListObject.HeaderRowRange.Columns.Count;

                    var data = new object[rows, columns];
                    for (int row = 1; row <= rows; row++)
                    {
                        for (int col = 1; col <= columns; col++)
                        {
                            data[row - 1, col - 1] = GetDataValue(col, lineItemsFromDB[row - 1]);
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
                    lineItemsListObject.Resize(
                        dataSheet.Range[Properties.Resources.DataWorksheetTopLeftRange,
                                        Properties.Resources.DataWorksheetRightMostColumn + "$" + (rows + 1).ToString()]);

                    // update the data range of the list object
                    logger.Info("Applying data to worksheet.");
                    lineItemsListObject.DataBodyRange.Value2 = data;

                    // delete the last row, if only 1 row was entered
                    if (oneRow)
                    {
                        dataSheet.Rows[rows + 1].Delete();
                    }

                    // add the edit buttons to the left of each line item
                    AddEditButtons(worksheetType, dso);

                    // applying formatting to currency and date columns
                    lineItemsListObject.DataBodyRange.Columns[(int)DataWorksheetColumns.AMOUNT].NumberFormat = amountFormat;
                    lineItemsListObject.DataBodyRange.Columns[(int)DataWorksheetColumns.DATE].NumberFormat = dateFormat;
                }
                else
                {
                    dataSheet.Columns[(int)DataWorksheetColumns.AMOUNT + 1].NumberFormat = amountFormat;
                    dataSheet.Columns[(int)DataWorksheetColumns.DATE + 1].NumberFormat = dateFormat;
                }

                // autofit the list object
                lineItemsListObject.Range.Columns.AutoFit();
                
                // enable screen updating, events, & alerts
                WorkbookUtil.ToggleUpdatingAndAlerts(true);

                // log completion
                logger.Info("Completed population of data worksheet.");
            }            
        }
                        
        internal static void RemoveAllSheets()
        {
            // loop through and delete all the data sheets
            foreach (CompositeDataSheetObject obj in dataWorksheets)
            {
                if (WorksheetPhysicallyExists(obj.worksheetName))
                {
                    obj.vstoWorksheet.Delete();
                }                
            }

            // clear out the persisted list
            dataWorksheets.Clear();
        }

        internal static string GetItemKey(int listObjectIndex, DataWorksheetType worksheetType)
        {
            // get the unique key from the list object, so that it can be retreived
            // from the DB
            string itemKey = null;
            CompositeDataSheetObject dataSheetObject = dataWorksheets.Find(dso => dso.worksheetType == worksheetType);

            // get the item key using the list object index from the appropriate data sheet's list object
            if (dataSheetObject != null)
            {
                string uniqueIdValue = dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, DataWorksheetColumns.UNIQUE_ID].Value2;
                if (!String.IsNullOrEmpty(uniqueIdValue))
                {
                    itemKey = uniqueIdValue;
                }
                
            }

            // return the itemKey, if it exists
            return itemKey;
        }

        internal static DenormalizedLineItem GetItem(int itemIndex, DataWorksheetType worksheetType)
        {
            // check if the worksheet type exists
            CompositeDataSheetObject dataSheetObject = dataWorksheets.Find(dso => dso.worksheetType == worksheetType);

            // if it does, get the item that has been clicked on
            if (dataSheetObject != null && dataSheetObject.lineItems != null)
            {
                return dataSheetObject.lineItems[itemIndex];
            }
            else
            {
                return null;
            }
        }

        internal static void RemoveLineItem(int listObjectIndex, int lineItemIndex, DataWorksheetType worksheetType)
        {
            CompositeDataSheetObject dso = dataWorksheets.Find(cdso => cdso.worksheetType == worksheetType);

            if (dso != null)
            {
                Dictionary<int, List<ActionButton>> thisWorksheetButtons = buttons[worksheetType];

                // remove action button from row
                ActionButton editButtonForLineItem = thisWorksheetButtons[lineItemIndex + 1].Find(ab => ab.index == lineItemIndex);
                dso.vstoWorksheet.Controls.Remove(editButtonForLineItem);
                
                // set the IsDeleted flag to true
                DenormalizedLineItem lineItem = dso.lineItems[lineItemIndex] as DenormalizedLineItem;
                lineItem.IsDeleted = true;

                // clear out the row
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.UNIQUE_ID].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DATE].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DESCRIPTION].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.AMOUNT].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.CATEGORY].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.SUBCATEGORY].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.TYPE].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.PAYMENT_METHOD].Value2 = null;
                dso.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.STATUS].Value2 = null;

            }
        }

        internal static void UpdateLineItem(int listObjectIndex, int lineItemIndex, DataWorksheetType worksheetType, DenormalizedLineItem updatedLineItem)
        {
            CompositeDataSheetObject dataSheetObject = dataWorksheets.Find(dso => dso.worksheetType == worksheetType);

            if (dataSheetObject != null)
            {
                if (updatedLineItem != null)
                {
                    // update the list object and the lineItems list
                    DateTime date = new DateTime(updatedLineItem.Year, updatedLineItem.MonthInt, updatedLineItem.Day);
                    string type = EnumUtil.GetFriendlyName(updatedLineItem.Type);
                    string status = EnumUtil.GetFriendlyName(updatedLineItem.Status);
                    string uniqueKey = (updatedLineItem.IsDuplicate ? "DUPLICATE" : (!String.IsNullOrWhiteSpace(updatedLineItem.UniqueKey) ? updatedLineItem.UniqueKey.ToString() : String.Empty));

                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.UNIQUE_ID].Value2 = uniqueKey;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DATE].Value2 = date;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DESCRIPTION].Value2 = updatedLineItem.Description;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.AMOUNT].Value2 = updatedLineItem.Amount;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.CATEGORY].Value2 = updatedLineItem.Category;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.SUBCATEGORY].Value2 = updatedLineItem.SubCategory;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.TYPE].Value2 = type;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.PAYMENT_METHOD].Value2 = updatedLineItem.PaymentMethod;
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.STATUS].Value2 = status;

                    // update the line item in the list
                    if (dataSheetObject.lineItems != null)
                    {
                        dataSheetObject.lineItems[lineItemIndex] = updatedLineItem;
                    }                    
                }
                else
                {
                    dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.UNIQUE_ID].Value2 = "ERROR";
                }
            }

            // select the sheet for editing
            dataSheetObject.vstoWorksheet.Select();
        }

        internal static void PreProcessNewItems()
        {
            // check if there is a new worksheet that exists
            DialogResult userNotifyResult = DialogResult.None;
            string worksheetName = null;
            bool worksheetExists = DataWorksheetExists(DataWorksheetType.NEW_ENTRIES, false, out userNotifyResult, out worksheetName);

            if (worksheetExists)
            {
                // new worksheet exists, so pre-process the items
                CompositeDataSheetObject cdo = dataWorksheets.Find(ds => ds.worksheetType == DataWorksheetType.NEW_ENTRIES);
                VstoExcel.ListObject listObject = cdo.listObject;

                // spin off the background worker to do the preprocessing
                LineItemsController.PreprocessLineItems(listObject, cdo.lineItems);

                // add edit buttons to the sheet
                AddEditButtons(DataWorksheetType.NEW_ENTRIES, cdo);
            }
        }

        internal static List<DenormalizedLineItem> GetLineItemsToSave(DataWorksheetType worksheetType)
        {
            CompositeDataSheetObject dso = dataWorksheets.Find(ds => ds.worksheetType == worksheetType);

            if (dso != null && dso.lineItems != null)
            {
                return dso.lineItems;
            }
            else
            {
                return null;
            }
        }

        internal static ActionButton AddButtonToListObject(VstoExcel.Worksheet worksheet,
                                                          VstoExcel.ListObject listObject,
                                                          LineItemActions action,
                                                          DataWorksheetType worksheetType,
                                                          int resultIndex,
                                                          int listObjectIndex,
                                                          int column,
                                                          int NUM_HEADER_ROWS,
                                                          EventHandler<ActionEventArgs> handler)
        {
            // create the button & set its properties
            ActionButton actionButton = new ActionButton();
            actionButton.index = resultIndex;
            actionButton.action = action;
            actionButton.listObjectIndex = listObjectIndex;
            actionButton.worksheetType = worksheetType;
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

        internal static void BackgroundWorkCompleted(DataWorksheetType dataWorksheetType, List<DenormalizedLineItem> lineItems)
        {
            CompositeDataSheetObject dso = dataWorksheets.Find(ds => ds.worksheetType == dataWorksheetType);

            // set the updated line items on the data sheet object
            if (dso != null)
            {
                dso.lineItems = lineItems;
            }
        }
        #endregion

        #region Private Methods
        private static bool DataWorksheetExists(DataWorksheetType worksheetType, bool notifyUser, out DialogResult result, out string worksheetName)
        {
            bool worksheetExists = false;
            result = DialogResult.None;
            worksheetName = null;
            foreach (CompositeDataSheetObject obj in dataWorksheets)
            {
                // if the worksheet type desired is already in existence and has not been deleted, ask user if they want to remove it
                if (obj.worksheetType == worksheetType)
                {
                    if (WorksheetPhysicallyExists(obj.worksheetName, out worksheetName))
                    {
                        result = notifyUser ?
                            MessageBox.Show("A data sheet of this type already exists. Remove it?", "Worksheet Exists!", MessageBoxButtons.YesNo) :
                            DialogResult.No;
                        worksheetExists = true;
                        break;
                    }
                }
            }

            return worksheetExists;
        }

        private static void AddEditButtons(DataWorksheetType worksheetType, CompositeDataSheetObject dso)
        {
            // fill in edit buttons
            logger.Info("Creating edit buttons for all line items.");

            // get the appropriate dictionary for the worksheet type
            if (buttons.ContainsKey(worksheetType))
            {
                // if the dictionary contains an entry for this worksheet type
                // delete it, as you can only one open at a time - which means
                // the worksheet has already been deleted manually.
                buttons.Remove(worksheetType);
            }

            // create the new button dictionary for this worksheet type
            Dictionary<int, List<ActionButton>> thisWorksheetButtons;
            thisWorksheetButtons = new Dictionary<int, List<ActionButton>>();
            buttons.Add(worksheetType, thisWorksheetButtons);

            // based on the type of worksheet that buttons are being added to, we need to get the total count from different sources
            int totalRows = 0;
            if (dso.listObject != null)
            {
                totalRows = dso.listObject.ListRows.Count;
            }
            else if (dso.lineItems != null)
            {
                totalRows = dso.lineItems.Count;
            }

            // loop through each line item, and apply the button, and save it in the dictionary
            for (int i = 1; i <= totalRows; i++)
            {
                ActionButton editActionButton = AddButtonToListObject(dataSheet, dso.listObject, LineItemActions.EDIT,
                    dso.worksheetType, i - 1, i, EDIT_ACTION_COLUMN, NUM_HEADER_ROWS, LineItemsController.dataWorksheet_ActionRequested);

                List<ActionButton> actionButtonList;
                if (!thisWorksheetButtons.TryGetValue(i, out actionButtonList))
                {
                    actionButtonList = new List<ActionButton>();
                }

                // add the edit button to the list
                actionButtonList.Add(editActionButton);

                // add buttons to the button dictionary
                thisWorksheetButtons.Add(i, actionButtonList);
            }
        }

        private static bool WorksheetPhysicallyExists(string dataWorksheetName)
        {
            string dummyOutString = null;
            return WorksheetPhysicallyExists(dataWorksheetName, out dummyOutString);
        }

        private static bool WorksheetPhysicallyExists(string dataWorksheetName, out string worksheetNameToRemove)
        {
            bool worksheetExists = false;
            worksheetNameToRemove = null;
            foreach (NativeExcel.Worksheet worksheet in Globals.ThisAddIn.Application.Worksheets)
            {
                if (worksheet.Name == dataWorksheetName)
                {
                    worksheetExists = true;
                    break;
                }
            }

            // if the worksheet in question does not actually exist, then flag it for removal
            worksheetNameToRemove = worksheetExists ? null : dataWorksheetName;
            return worksheetExists;
        }

        private static object GetDataValue(int colNum, DenormalizedLineItem lineItem)
        {
            object value;

            // switch through the colNum, and provide the correct data point based on the row index
            switch (colNum)
            {
                case (int)DataWorksheetColumns.UNIQUE_ID:
                    value = String.IsNullOrWhiteSpace(lineItem.UniqueKey) ? "" : lineItem.UniqueKey.ToString();
                    break;
                case (int)DataWorksheetColumns.DATE:
                    value = new DateTime(lineItem.Year, lineItem.MonthInt, lineItem.Day);
                    break;
                case (int)DataWorksheetColumns.DESCRIPTION:
                    value = lineItem.Description;
                    break;
                case (int)DataWorksheetColumns.AMOUNT:
                    value = lineItem.Amount;
                    break;
                case (int)DataWorksheetColumns.CATEGORY:
                    value = lineItem.Category;
                    break;
                case (int)DataWorksheetColumns.SUBCATEGORY:
                    value = lineItem.SubCategory;
                    break;
                case (int)DataWorksheetColumns.TYPE:
                    value = EnumUtil.GetFriendlyName(lineItem.Type);
                    break;
                case (int)DataWorksheetColumns.PAYMENT_METHOD:
                    value = lineItem.PaymentMethod;
                    break;
                case (int)DataWorksheetColumns.STATUS:
                    value = EnumUtil.GetFriendlyName(lineItem.Status);
                    break;
                default:
                    value = "N/A";
                    break;
            }

            return value;
        }

        private static void RemoveSheet(DataWorksheetType worksheetType)
        {
            foreach (CompositeDataSheetObject obj in dataWorksheets)
            {
                if (obj.worksheetType == worksheetType && obj.vstoWorksheet != null)
                {
                    obj.vstoWorksheet.Delete();
                    dataWorksheets.Remove(obj);
                    buttons.Remove(worksheetType);
                    break;
                }
            }
        }

        private static void GetNewDataSheet(DataWorksheetType worksheetType)
        {
            NativeExcel.Worksheet dataWorksheet = null;

            NativeExcel.Sheets worksheets = Globals.ThisAddIn.Application.Worksheets;
            NativeExcel.Worksheet lastWorksheet = worksheets[worksheets.Count];
            dataWorksheet = (NativeExcel.Worksheet)Globals.ThisAddIn.Application.Worksheets.Add(After: lastWorksheet);
            dataWorksheet.Name = Properties.Resources.DataWorksheetName + EnumUtil.GetFriendlyName(worksheetType);

            // return the VSTO object to the caller
            dataSheet = Globals.Factory.GetVstoObject(dataWorksheet);
        }
        #endregion
    }
}
