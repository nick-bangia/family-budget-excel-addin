using System;
using System.Threading;
using HouseholdBudget.Async;
using HouseholdBudget.Controllers;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Utilities;
using HouseholdBudget.DataControllers;
using HouseholdBudget.Utilities;
using log4net;
using log4net.Config;
using NativeExcel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;

namespace HouseholdBudget.UI
{
    public partial class ThisAddIn
    {
        private static ILog logger;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void Application_WorkbookOpen(NativeExcel.Workbook Wb)
        {
            // check whether the workbook is configured properly
            bool validWorkbook = IsValidWorkbook(Wb, requiresConfiguration: true);

            // based on validity, set ribbon state
            SetRibbonState(Wb, validWorkbook);

            if (validWorkbook)
            {
                // check the API health before continuing
                ApiHealth apiHealth = APIUtil.CheckAPIHealth();
                if (apiHealth.healthState == ApiHealthState.OK)
                {
                    // only update the data sheet is the workbook is configured properly
                    LineItemsController.PopulateDataSheet();
                    CategoriesController.PopulateSubCategoriesSheet();
                    WorkbookUtil.RefreshPivotTables();

                    // pre-populate any data service lists
                    PaymentMethodsController.GetPaymentMethods();

                    // Switch to the first worksheet
                    WorkbookUtil.ShowFirstWorksheet();
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
        }

        private void Application_WorkbookBeforeClose(NativeExcel.Workbook Wb, ref bool Cancel)
        {
            bool validWorkbook = IsValidWorkbook(Wb, requiresConfiguration: false);

            if (validWorkbook)
            {
                logger.Info("Saving state and shutting down the workbook.");
                
                // remove any data based worksheets
                RemoveData(Wb);
            }            
        }

        private void RemoveData(NativeExcel.Workbook Wb)
        {
            // remove any data sheets without notification
            Globals.ThisAddIn.Application.DisplayAlerts = false;

            // remove the sheets
            MasterDataController.RemoveSheet();
            SubCategoriesDataManager.RemoveSheet();
            WorksheetDataController.RemoveAllSheets();

            // save the workbook, and renable alerts
            Wb.Save();
            Globals.ThisAddIn.Application.DisplayAlerts = true;
        }

        private static void SetRibbonState(NativeExcel.Workbook Wb, bool ribbonState)
        {
            Globals.Ribbons.HouseholdBudgetRibbon.tabHouseholdBudget.Visible = ribbonState;
        }

        private static bool IsValidWorkbook(NativeExcel.Workbook Wb, bool requiresConfiguration)
        {
            bool validWorkbook = false;

            // determine if this workbook is a valid one
            foreach (NativeExcel.Worksheet worksheet in Wb.Application.Worksheets)
            {
                if (worksheet.Name == Properties.Resources.ConfigurationWorksheetName)
                {
                    if (worksheet.Cells[1, 2].Value2 == Properties.Resources.WorkbookNameValue)
                    {
                        validWorkbook = true;

                        if (requiresConfiguration)
                        {
                            // Configure logging if the the workbook is valid and requires configuration
                            ConfigureLogging();

                            // configure the workbook if the workbook is valid
                            WorkbookUtil.ConfigureWorkbook(worksheet, Wb.Path);
                        }
                        
                        // break from the foreach loop since the workbook is valid
                        break;
                    }
                }
            }

            return validWorkbook;
        }

        private static void ConfigureLogging()
        {
            // do most of the configuration via config file
            XmlConfigurator.Configure();

            // create the add in logger
            logger = LogManager.GetLogger("HouseholdBudgetAddIn_Main");
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return Globals.Factory.GetRibbonFactory().CreateRibbonManager(
                new Microsoft.Office.Tools.Ribbon.IRibbonExtension[] { new HouseholdBudgetRibbon() });
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            this.Application.WorkbookBeforeClose += new NativeExcel.AppEvents_WorkbookBeforeCloseEventHandler(Application_WorkbookBeforeClose);
            this.Application.WorkbookOpen += new NativeExcel.AppEvents_WorkbookOpenEventHandler(Application_WorkbookOpen);
        }        
        #endregion
    }
}
