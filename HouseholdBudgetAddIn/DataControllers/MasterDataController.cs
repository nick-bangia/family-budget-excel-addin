using System.Collections.Generic;
using HouseholdBudget.Data.Domain;
using VstoExcel = Microsoft.Office.Tools.Excel;
using NativeExcel = Microsoft.Office.Interop.Excel;
using HouseholdBudget.UI;
using HouseholdBudget.Enums;
using HouseholdBudget.Utilities;
using log4net;
using HouseholdBudget.Controllers;

namespace HouseholdBudget.DataControllers
{
    internal static class MasterDataController
    {
        #region Properties
        private static VstoExcel.Worksheet vstoDataSheet = null;
        private static VstoExcel.ListObject lineItemsListObject = null;
        private static readonly ILog logger = LogManager.GetLogger("HouseholdBudgetAddIn_DataManager");
        #endregion

        public static void PopulateMasterDataTable(List<DenormalizedLineItem> lineItems)
        {
            // log the status
            logger.Info("Beginning population of data sheet...");

            // Get the data sheet into the vstoDataSheet
            GetDataSheet();

            // disable screen updating, events, & alerts
            WorkbookUtil.ToggleUpdatingAndAlerts(false);
                        
            if (lineItemsListObject == null)
            {
                lineItemsListObject = vstoDataSheet.Controls.AddListObject(vstoDataSheet.get_Range(Properties.Resources.DataListObjectRange),
                    Properties.Resources.DataListObjectName);

                // set up headers for list object
                logger.Info("Setting up headers.");

                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.YEAR].Value2 = EnumUtil.GetFriendlyName(DataColumns.YEAR);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.MONTH].Value2 = EnumUtil.GetFriendlyName(DataColumns.MONTH);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.MONTH_YEAR].Value2 = EnumUtil.GetFriendlyName(DataColumns.MONTH_YEAR);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DAY_MONTH_YEAR].Value2 = EnumUtil.GetFriendlyName(DataColumns.DAY_MONTH_YEAR);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.QUARTER_YEAR].Value2 = EnumUtil.GetFriendlyName(DataColumns.QUARTER_YEAR);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DAY_OF_WEEK].Value2 = EnumUtil.GetFriendlyName(DataColumns.DAY_OF_WEEK);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DESCRIPTION].Value2 = EnumUtil.GetFriendlyName(DataColumns.DESCRIPTION);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.CATEGORY].Value2 = EnumUtil.GetFriendlyName(DataColumns.CATEGORY);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.SUBCATEGORY].Value2 = EnumUtil.GetFriendlyName(DataColumns.SUBCATEGORY);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.SUBCATEGORY_WITH_PREFIX].Value2 = EnumUtil.GetFriendlyName(DataColumns.SUBCATEGORY_WITH_PREFIX);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.AMOUNT].Value2 = EnumUtil.GetFriendlyName(DataColumns.AMOUNT);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.TYPE].Value2 = EnumUtil.GetFriendlyName(DataColumns.TYPE);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.SUBTYPE].Value2 = EnumUtil.GetFriendlyName(DataColumns.SUBTYPE);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.PAYMENT_METHOD].Value2 = EnumUtil.GetFriendlyName(DataColumns.PAYMENT_METHOD);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.ACCOUNT].Value2 = EnumUtil.GetFriendlyName(DataColumns.ACCOUNT);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.STATUS].Value2 = EnumUtil.GetFriendlyName(DataColumns.STATUS);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.IS_GOAL].Value2 = EnumUtil.GetFriendlyName(DataColumns.IS_GOAL);
            }
            else
            {
                logger.Info("Clearing current data.");

                lineItemsListObject.DataBodyRange.Clear();
                lineItemsListObject.Resize(
                    vstoDataSheet.Range[Properties.Resources.DataListTopLeftRange,
                                        Properties.Resources.DataListBottomRightRange]);
            }

            // fill in data as an array
            logger.Info("Creating data matrix.");
            
            int rows = lineItems.Count;
            int columns = lineItemsListObject.HeaderRowRange.Columns.Count;
                        
            var data = new object[rows, columns];
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    data[row - 1, col - 1] = GetDataValue(row - 1, col, lineItems);                    
                }
            }

            // size the list object appropriately
            lineItemsListObject.Resize(
                vstoDataSheet.Range[Properties.Resources.DataListTopLeftRange,
                                    Properties.Resources.DataListRightMostColumn + "$" + (rows + 1).ToString()]);
            
            // update the data range of list object
            logger.Info("Applying data to worksheet.");
            lineItemsListObject.DataBodyRange.Value2 = data;
            
            // autofit the list object
            lineItemsListObject.Range.Columns.AutoFit();

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
                lineItemsListObject = null;
            }
        }

        private static object GetDataValue(int index, int colNum, List<DenormalizedLineItem> lineItems)
        {
            object value;

            // switch through the colNum, and provide the correct data point based on the row index
            switch (colNum)
            {
                case (int)DataColumns.YEAR:
                    value = lineItems[index].Year;
                    break;
                case (int)DataColumns.MONTH:
                    value = lineItems[index].Month;
                    break;
                case (int)DataColumns.MONTH_YEAR:
                    value = lineItems[index].Month + "-" + lineItems[index].Year.ToString();
                    break;
                case (int)DataColumns.DAY_MONTH_YEAR:
                    value = lineItems[index].Day.ToString() + "-" + lineItems[index].Month + "-" + lineItems[index].Year.ToString();
                    break;
                case (int)DataColumns.QUARTER_YEAR:
                    value = EnumUtil.GetFriendlyName(lineItems[index].Quarter) + "-" + lineItems[index].Year.ToString().Substring(2);
                    break;
                case (int)DataColumns.DAY_OF_WEEK:
                    value = lineItems[index].DayOfWeek;
                    break;
                case (int)DataColumns.DESCRIPTION:
                    value = lineItems[index].Description;
                    break;
                case (int)DataColumns.CATEGORY:
                    value = lineItems[index].Category;
                    break;
                case (int)DataColumns.SUBCATEGORY:
                    value = lineItems[index].SubCategory;
                    break;
                case (int)DataColumns.SUBCATEGORY_WITH_PREFIX:
                    value = lineItems[index].SubCategoryPrefix + " - " + lineItems[index].SubCategory;
                    break;
                case (int)DataColumns.AMOUNT:
                    value = lineItems[index].Amount;
                    break;
                case (int)DataColumns.TYPE:
                    value = EnumUtil.GetFriendlyName(lineItems[index].Type);
                    break;
                case (int)DataColumns.SUBTYPE:
                    value = EnumUtil.GetFriendlyName(lineItems[index].SubType);
                    break;
                case (int)DataColumns.PAYMENT_METHOD:
                    value = lineItems[index].PaymentMethod;
                    break;
                case (int)DataColumns.ACCOUNT:
                    value = lineItems[index].AccountName;
                    break;
                case (int)DataColumns.STATUS:
                    value = EnumUtil.GetFriendlyName(lineItems[index].Status);
                    break;
                case (int)DataColumns.IS_GOAL:
                    value = lineItems[index].IsGoal;
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
                    if (wrksheet.Name == Properties.Resources.MasterDataWorksheetName)
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
                    dataSheet.Name = Properties.Resources.MasterDataWorksheetName;
                }

                // assign the VSTO object for it to the vstoDataSheet property
                vstoDataSheet = Globals.Factory.GetVstoObject(dataSheet);
            }           
        }
    }
}
