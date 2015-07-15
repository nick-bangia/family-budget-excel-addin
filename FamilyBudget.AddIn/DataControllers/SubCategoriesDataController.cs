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
    internal static class SubCategoriesDataManager
    {
        #region Properties
        private static VstoExcel.Worksheet vstoDataSheet = null;
        private static VstoExcel.ListObject subCategoriesListObject = null;
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_SubCategoriesManager");
        #endregion

        public static void PopulateSubcategoriesDataTable(BindingList<Subcategory> subCategories)
        {
            // log the status
            logger.Info("Beginning population of data sheet...");

            // Get the data sheet into the vstoDataSheet
            GetDataSheet();

            // disable screen updating, events, & alerts
            WorkbookUtil.ToggleUpdatingAndAlerts(false);
                        
            if (subCategoriesListObject == null)
            {
                subCategoriesListObject = vstoDataSheet.Controls.AddListObject(vstoDataSheet.get_Range(Properties.Resources.SubCategoriesListObjectRange),
                    Properties.Resources.SubCategoriesListObjectName);

                // set up headers for list object
                logger.Info("Setting up headers.");

                subCategoriesListObject.HeaderRowRange[1, (int)SubCategoryDataColumns.PREFIX].Value2 = EnumUtil.GetFriendlyName(SubCategoryDataColumns.PREFIX);
                subCategoriesListObject.HeaderRowRange[1, (int)SubCategoryDataColumns.SUBCATEGORY].Value2 = EnumUtil.GetFriendlyName(SubCategoryDataColumns.SUBCATEGORY);
                subCategoriesListObject.HeaderRowRange[1, (int)SubCategoryDataColumns.ACCOUNT_NAME].Value2 = EnumUtil.GetFriendlyName(SubCategoryDataColumns.ACCOUNT_NAME);
                subCategoriesListObject.HeaderRowRange[1, (int)SubCategoryDataColumns.IS_ACTIVE].Value2 = EnumUtil.GetFriendlyName(SubCategoryDataColumns.IS_ACTIVE);
              
            }
            else
            {
                logger.Info("Clearing current data.");

                subCategoriesListObject.DataBodyRange.Clear();
                subCategoriesListObject.Resize(
                    vstoDataSheet.Range[Properties.Resources.SubCategoriesListTopLeftRange,
                                        Properties.Resources.SubCategoriesListBottomRightRange]);
            }

            // fill in data as an array
            logger.Info("Creating data matrix.");
            
            int rows = subCategories.Count;
            int columns = subCategoriesListObject.HeaderRowRange.Columns.Count;
                        
            var data = new object[rows, columns];
            for (int row = 1; row <= rows; row++)
            {
                for (int col = 1; col <= columns; col++)
                {
                    data[row - 1, col - 1] = GetDataValue(row - 1, col, subCategories);                    
                }
            }

            // size the list object appropriately
            subCategoriesListObject.Resize(
                vstoDataSheet.Range[Properties.Resources.SubCategoriesListTopLeftRange,
                                    Properties.Resources.SubCategoriesListRightMostColumn + "$" + (rows + 1).ToString()]);
            
            // update the data range of list object
            logger.Info("Applying data to worksheet.");
            subCategoriesListObject.DataBodyRange.Value2 = data;
            
            // autofit the list object
            subCategoriesListObject.Range.Columns.AutoFit();

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
                subCategoriesListObject = null;
            }
        }

        private static object GetDataValue(int index, int colNum, BindingList<Subcategory> lineItems)
        {
            object value;

            // switch through the colNum, and provide the correct data point based on the row index
            switch (colNum)
            {
                case (int)SubCategoryDataColumns.PREFIX:
                    value = lineItems[index].SubcategoryPrefix;
                    break;
                case (int)SubCategoryDataColumns.SUBCATEGORY:
                    value = lineItems[index].SubcategoryName;
                    break;
                case (int)SubCategoryDataColumns.ACCOUNT_NAME:
                    value = lineItems[index].AccountName;
                    break;
                case (int)SubCategoryDataColumns.IS_ACTIVE:
                    value = lineItems[index].IsActive ? "Yes" : "No";
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
                    if (wrksheet.Name == Properties.Resources.SubCategoriesDataWorksheetName)
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
                    dataSheet.Name = Properties.Resources.SubCategoriesDataWorksheetName;
                }

                // assign the VSTO object for it to the vstoDataSheet property
                vstoDataSheet = Globals.Factory.GetVstoObject(dataSheet);
            }           
        }
    }
}
