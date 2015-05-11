using System.Configuration;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Utilities;
using log4net;
using log4net.Config;
using NativeExcel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using FamilyBudget.Common.Config;
using System;
using System.Windows.Forms;

namespace FamilyBudget.AddIn.UI
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
            // Configure Logging
            ConfigureLogging();
            
            // if the add-in has been registered to a workbook, check if this workbook is a valid workbook
            // check whether the workbook is configured properly
            bool isValidWorkbook = WorkbookUtil.IsValidWorkbook(Wb);

            // based on validity, set ribbon state
            WorkbookUtil.SetRibbonState(isValidWorkbook);

            if (isValidWorkbook)
            {
                WorkbookUtil.SetupWorkbook();
            }
        }

        private void Application_WorkbookActivate(NativeExcel.Workbook wb)
        {
            // set the ribbon state based on if the workbook is valid or not
            WorkbookUtil.SetRibbonState(WorkbookUtil.IsValidWorkbook(wb));
        }

        private void Application_WorkbookBeforeClose(NativeExcel.Workbook Wb, ref bool Cancel)
        {
            if (WorkbookUtil.IsValidWorkbook(Wb))
            {
                logger.Info("Saving state and shutting down the workbook.");
                
                // disable notification so that sheets can be removed without any disruption
                Globals.ThisAddIn.Application.DisplayAlerts = false;

                // remove any data sheets without notification
                WorkbookUtil.RemoveData(Wb);

                // save the workbook, and renable alerts
                Wb.Save();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }
        }       

        private static void ConfigureLogging()
        {
            // do most of the configuration via config file
            XmlConfigurator.Configure();

            // create the add in logger
            logger = LogManager.GetLogger("FamilyBudgetAddIn_Main");
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return Globals.Factory.GetRibbonFactory().CreateRibbonManager(
                new Microsoft.Office.Tools.Ribbon.IRibbonExtension[] { new FamilyBudgetRibbon() });
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
            this.Application.WorkbookActivate += new NativeExcel.AppEvents_WorkbookActivateEventHandler(Application_WorkbookActivate);
        }        
        #endregion
    }
}
