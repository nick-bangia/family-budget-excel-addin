using System;
using System.Windows.Forms;
using FamilyBudget.Common.Config;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.UI;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Utilities;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Win32;
using NativeExcel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace FamilyBudget.AddIn.Utilities
{
    internal static class WorkbookUtil
    {
        #region Properties
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_WorkbookUtil");
        #endregion

        #region Event Handlers
        internal static void btnRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            RefreshWorkbook(userRefresh: true);
        }
        #endregion

        #region Internal Methods
        internal static void RefreshWorkbook(bool userRefresh)
        {
            // Build & Populate data sheets
            LineItemsController.PopulateDataSheet(userRefresh);
            CategoriesController.PopulateSubCategoriesSheet(userRefresh);

            // pre-populate any data service lists
            PaymentMethodsController.GetPaymentMethods(userRefresh);

            // Refresh Pivot Tables
            RefreshPivotTables();

            // Show the first worksheet if refreshing without rebuilding
            if (!userRefresh)
            {
                ShowFirstWorksheet();
            }
        }

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

        internal static void SetupWorkbook()
        {
            // check the API health before continuing
            ApiHealth apiHealth = APIUtil.CheckAPIHealth();
            if (apiHealth.healthState == ApiHealthState.OK)
            {
                RefreshWorkbook(userRefresh: false);
            }
            else
            {
                logger.ErrorFormat("The API is not in a healthy state. STATE = {0}, REASON = {1}", apiHealth.healthState.ToString(), apiHealth.healthReason);
                string message =
                    "The API is not in a healthy state." + Environment.NewLine +
                    "Status: " + apiHealth.healthState.ToString() + Environment.NewLine +
                    "Reason: " + apiHealth.healthReason + Environment.NewLine + Environment.NewLine +
                    "This will prevent you from interacting with this tool properly. Please resolve the issue and click Refresh to check the state";

                MessageBox.Show(message);
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

        internal static bool IsValidWorkbook(NativeExcel.Workbook Wb)
        {
            string registeredWorkbookPath = AddInConfiguration.RegisteredWorkbookPath;

            // evaluate whether this workbook is registered with the add-in
            return registeredWorkbookPath.Equals(Wb.FullName);
        }

        internal static void RemoveData(NativeExcel.Workbook Wb)
        {
            // remove the sheets
            MasterDataController.RemoveSheet();
            SubCategoriesDataManager.RemoveSheet();
            WorksheetDataController.RemoveAllSheets();
        }

        internal static void SetRibbonState(bool isValidWorkbook)
        {
            Globals.Ribbons.FamilyBudgetRibbon.tabFamilyBudget.Visible = isValidWorkbook;
        }
        #endregion
    }
}
