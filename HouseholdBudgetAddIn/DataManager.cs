using System.Collections.Generic;
using HouseholdBudget.Data.Domain;
using VstoExcel = Microsoft.Office.Tools.Excel;
using NativeExcel = Microsoft.Office.Interop.Excel;
using HouseholdBudget.UI;
using HouseholdBudget.Enums;
using HouseholdBudget.Utilities;
using System;

namespace HouseholdBudget
{
    internal static class DataManager
    {
        #region Properties
        private static VstoExcel.Worksheet vstoDataSheet = null;
        private static VstoExcel.ListObject lineItemsListObject = null;
        #endregion

        public static void PopulateDataSheet(List<DenormalizedLineItem> lineItems)
        {
            // Get the data sheet into the vstoDataSheet
            GetDataSheet();

            // disable screen updating, events, & alerts
            Globals.ThisAddIn.Application.EnableEvents = false;
            Globals.ThisAddIn.Application.DisplayAlerts = false;
            Globals.ThisAddIn.Application.ScreenUpdating = false;
            
            if (lineItemsListObject == null)
            {
                lineItemsListObject = vstoDataSheet.Controls.AddListObject(vstoDataSheet.get_Range(Properties.Resources.DataListObjectRange),
                    Properties.Resources.DataListObjectName);

                // set up headers for list object
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.YEAR].Value2 = EnumUtil.GetFriendlyName(DataColumns.YEAR);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.MONTH].Value2 = EnumUtil.GetFriendlyName(DataColumns.MONTH);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DAY].Value2 = EnumUtil.GetFriendlyName(DataColumns.DAY);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DAY_OF_WEEK].Value2 = EnumUtil.GetFriendlyName(DataColumns.DAY_OF_WEEK);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.DESCRIPTION].Value2 = EnumUtil.GetFriendlyName(DataColumns.DESCRIPTION);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.CATEGORY].Value2 = EnumUtil.GetFriendlyName(DataColumns.CATEGORY);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.AMOUNT].Value2 = EnumUtil.GetFriendlyName(DataColumns.AMOUNT);
                lineItemsListObject.HeaderRowRange[1, (int)DataColumns.TYPE].Value2 = EnumUtil.GetFriendlyName(DataColumns.TYPE);
            }
            else
            {
                lineItemsListObject.DataBodyRange.Clear();
                lineItemsListObject.Resize(
                    vstoDataSheet.Range[Properties.Resources.DataListTopLeftRange,
                                        Properties.Resources.DataListBottomRightRange]);
            }

            // fill in data as an array
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
            lineItemsListObject.DataBodyRange.Value2 = data;
                                
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.YEAR].Value2 = lineItems[i].Year.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.MONTH].Value2 = lineItems[i].Month.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.DAY].Value2 = lineItems[i].Day.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.DAY_OF_WEEK].Value2 = lineItems[i].DayOfWeek.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.DESCRIPTION].Value2 = lineItems[i].Description.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.CATEGORY].Value2 = lineItems[i].Category.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.AMOUNT].Value2 = lineItems[i].Amount.ToString();
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.AMOUNT].Style = "Currency";
            //lineItemsListObject.DataBodyRange.Cells[rowNum, (int)DataColumns.TYPE].Value2 = lineItems[i].Type.ToString();
            //}

            // autofit the list object
            lineItemsListObject.Range.Columns.AutoFit();

            // enable screen updating, events, & alerts
            Globals.ThisAddIn.Application.EnableEvents = true;
            Globals.ThisAddIn.Application.DisplayAlerts = true;
            Globals.ThisAddIn.Application.ScreenUpdating = true;
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
                case (int)DataColumns.DAY:
                    value = lineItems[index].Day;
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
                case (int)DataColumns.AMOUNT:
                    value = lineItems[index].Amount;
                    break;
                case (int)DataColumns.TYPE:
                    value = lineItems[index].Type.ToString();
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
                    if (wrksheet.Name == Properties.Resources.DataWorksheetName)
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
                    dataSheet.Name = Properties.Resources.DataWorksheetName;
                }

                // assign the VSTO object for it to the vstoDataSheet property
                vstoDataSheet = Globals.Factory.GetVstoObject(dataSheet);
            }           
        }
    }
}
