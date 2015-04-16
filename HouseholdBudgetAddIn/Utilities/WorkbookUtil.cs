using System.Windows.Forms;
using HouseholdBudget.Controllers;
using HouseholdBudget.UI;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using NativeExcel = Microsoft.Office.Interop.Excel;

namespace HouseholdBudget.Utilities
{
    internal static class WorkbookUtil
    {
        #region Properties
        private static readonly ILog logger = LogManager.GetLogger("HouseholdBudgetAddIn_WorkbookUtil");
        #endregion

        #region Event Handlers
        internal static void btnRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            LineItemsController.RebuildDataSheet();
            CategoriesController.RebuildSubCategoryDataSheet();
            RefreshPivotTables();
            ShowFirstWorksheet();
        }
        #endregion

        #region Internal Methods
        internal static void RefreshPivotTables()
        {
            logger.Info("Refreshing pivot tables!");
            foreach (NativeExcel.Worksheet sheet in Globals.ThisAddIn.Application.Worksheets)
            {
                // go through each sheet, and refresh any pivot table(s) it might have
                foreach (NativeExcel.PivotTable table in sheet.PivotTables())
                {
                    if (!table.RefreshTable())
                    {
                        MessageBox.Show("Unable to refresh pivot table: " + table.Name);
                    }
                }
            }
        }

        internal static void ShowFirstWorksheet()
        {
            NativeExcel._Worksheet firstWorksheet = null;

            foreach (NativeExcel.Worksheet worksheet in Globals.ThisAddIn.Application.Worksheets)
            {
                firstWorksheet = (NativeExcel._Worksheet)worksheet;
                break;
            }

            if (firstWorksheet != null)
            {
                firstWorksheet.Activate();
            }
        }

        internal static void ShowWorksheetByName(string worksheetName)
        {
            foreach (NativeExcel.Worksheet worksheet in Globals.ThisAddIn.Application.Worksheets)
            {
                if (worksheet.Name == worksheetName)
                {
                    NativeExcel._Worksheet ws = (NativeExcel._Worksheet)worksheet;
                    ws.Activate();
                    break;
                }
            }
        }

        internal static void ToggleUpdatingAndAlerts(bool enabled)
        {
            Globals.ThisAddIn.Application.EnableEvents = enabled;
            Globals.ThisAddIn.Application.DisplayAlerts = enabled;
            Globals.ThisAddIn.Application.ScreenUpdating = enabled;
        }

        internal static void ConfigureWorkbook(NativeExcel.Worksheet configurationWorksheet, string workbookPath)
        {
            // log that configuration is being completed
            logger.Info("Configuring the workbook.");
        }
        #endregion
    }
}
