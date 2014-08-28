using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VstoExcel = Microsoft.Office.Tools.Excel;
using NativeExcel = Microsoft.Office.Interop.Excel;
using HouseholdBudget.Data.Enums;
using log4net;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Utilities;
using HouseholdBudget.Enums;
using System.Windows.Forms;
using HouseholdBudget.UI;

namespace HouseholdBudget
{
    internal static class DataWorksheetManager
    {
        #region Composite Datasheet Object
        private class CompositeDataSheetObject
        {
            public DataWorksheetType worksheetType;
            public VstoExcel.Worksheet vstoWorksheet;
            public VstoExcel.ListObject listObject;
            public string worksheetName;
        }
        #endregion

        #region Properties
        private static List<CompositeDataSheetObject> dataWorksheets = new List<CompositeDataSheetObject>();
        private static readonly ILog logger = LogManager.GetLogger("HouseholdBudgetAddIn_DataWorksheetManager");
        internal static VstoExcel.ListObject lineItemsListObject;
        internal static VstoExcel.Worksheet dataSheet;
        internal static List<DenormalizedLineItem> lineItems;
        internal static Dictionary<DataWorksheetType, Dictionary<int, ActionButton>> buttons = 
            new Dictionary<DataWorksheetType, Dictionary<int, ActionButton>>();
        internal const int NUM_HEADER_ROWS = 1;
        internal const int EDIT_ACTION_COLUMN = 1;
        #endregion
        
        public static void PopulateNewWorksheet(DataWorksheetType worksheetType, List<DenormalizedLineItem> lineItemsFromDB)
        {
            // log status
            logger.Info(String.Format("Beginning population of {0} data sheet...", EnumUtil.GetFriendlyName(worksheetType)));

            // setup the variables
            lineItems = lineItemsFromDB;

            // check if a worksheet already exists for this type. If so, provide user with a message
            bool worksheetExists = false;
            DialogResult result = DialogResult.None;
            string worksheetName = null;
            foreach (CompositeDataSheetObject obj in dataWorksheets)
            {
                // if the worksheet type desired is already in existence and has not been deleted, ask user if they want to remove it
                if (obj.worksheetType == worksheetType)
                {
                    if (WorksheetPhysicallyExists(obj.worksheetName, out worksheetName))
                    {
                        result = MessageBox.Show("A data sheet of this type already exists. Remove it?", "Worksheet Exists!", MessageBoxButtons.YesNo);
                        worksheetExists = true;
                        break;
                    }                        
                }
            }

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
                Controller.ToggleUpdatingAndAlerts(false);

                // create the list object
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

                // fill in data as an array
                logger.Info("Creating data matrix.");

                int rows = lineItems.Count;
                int columns = lineItemsListObject.HeaderRowRange.Columns.Count;
                
                var data = new object[rows, columns];
                for (int row = 1; row <= rows; row++)
                {
                    for (int col = 1; col <= columns; col++)
                    {
                        data[row - 1, col - 1] = GetDataValue(col, lineItems[row - 1]);
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
                
                // applying formatting to currency and date columns
                lineItemsListObject.DataBodyRange.Columns[(int)DataWorksheetColumns.AMOUNT].NumberFormat = "$#,##0.00";
                lineItemsListObject.DataBodyRange.Columns[(int)DataWorksheetColumns.DATE].NumberFormat = "MM/DD/YYYY";

                // delete the last row, if only 1 row was entered
                if (oneRow)
                {
                    dataSheet.Rows[rows + 1].Delete();
                }

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
                Dictionary<int, ActionButton> thisWorksheetButtons;
                thisWorksheetButtons = new Dictionary<int, ActionButton>();
                buttons.Add(worksheetType, thisWorksheetButtons);
                
                // loop through each line item, and apply the button, and save it in the dictionary
                for (int i = 0; i < lineItems.Count; i++)
                {
                    ActionButton editActionButton = Controller.AddButtonToListObject(dataSheet, lineItemsListObject, LineItemActions.EDIT, 
                        worksheetType, i, i + 1, EDIT_ACTION_COLUMN, NUM_HEADER_ROWS, Controller.dataWorksheet_ActionRequested);

                    // add buttons to the button dictionary
                    thisWorksheetButtons.Add(i + 1, editActionButton);
                }

                // autofit the list object
                lineItemsListObject.Range.Columns.AutoFit();

                // save the composite object
                dataWorksheets.Add(new CompositeDataSheetObject()
                {
                    vstoWorksheet = dataSheet,
                    listObject = lineItemsListObject,
                    worksheetType = worksheetType,
                    worksheetName = dataSheet.Name
                });
                
                // enable screen updating, events, & alerts
                Controller.ToggleUpdatingAndAlerts(true);

                // log completion
                logger.Info("Completed population of data worksheet.");
            }            
        }

        public static void RemoveAllSheets()
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

        public static Guid GetItemKey(int listObjectIndex, int lineItemIndex)
        {
            // get the unique key from the list object, so that it can be retreived
            // from the DB
            return Guid.Parse(lineItemsListObject.DataBodyRange.Cells[listObjectIndex, DataWorksheetColumns.UNIQUE_ID].Value2);
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
                    value = lineItem.UniqueKey == Guid.Empty ? "" : lineItem.UniqueKey.ToString();
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

        public static void UpdateLineItem(int listObjectIndex, int lineItemIndex, DataWorksheetType worksheetType, DenormalizedLineItem updatedLineItem)
        {
            CompositeDataSheetObject dataSheetObject = dataWorksheets.Find(dso => dso.worksheetType == worksheetType);

            if (dataSheetObject != null)
            {
                // update the list object
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DATE].Value2 =
                    new DateTime(updatedLineItem.Year, updatedLineItem.MonthInt, updatedLineItem.Day);
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.DESCRIPTION].Value2 = updatedLineItem.Description;
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.AMOUNT].Value2 = updatedLineItem.Amount;
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.CATEGORY].Value2 = updatedLineItem.Category;
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.SUBCATEGORY].Value2 = updatedLineItem.SubCategory;
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.TYPE].Value2 = EnumUtil.GetFriendlyName(updatedLineItem.Type);
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.PAYMENT_METHOD].Value2 = updatedLineItem.PaymentMethod;
                dataSheetObject.listObject.DataBodyRange.Cells[listObjectIndex, (int)DataWorksheetColumns.STATUS].Value2 = EnumUtil.GetFriendlyName(updatedLineItem.Status);
            }

            // select the sheet for editing
            dataSheetObject.vstoWorksheet.Select();
        }
    }
}
