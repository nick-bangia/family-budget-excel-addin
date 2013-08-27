using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NativeExcel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using VstoExcel = Microsoft.Office.Tools.Excel;
using System.Windows.Forms;

namespace HouseholdBudget.UI
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void Application_WorkbookActivate(NativeExcel.Workbook Wb)
        {
            SetRibbonState(Wb);
        }

        private void Application_WorkbookBeforeClose(NativeExcel.Workbook Wb, ref bool Cancel)
        {
            // determine if the import results worksheet is still in the workbook
            NativeExcel.Worksheet importResultSheet = null;
            foreach (NativeExcel.Worksheet worksheet in Wb.Application.Worksheets)
            {
                if (worksheet.Name == Properties.Resources.ImportResultsWorksheetName)
                {
                    importResultSheet = worksheet;
                    break;
                }
            }

            if (importResultSheet != null)
            {
                // show a dialog prompting the user as to whether they really want to close the workbook
                DialogResult result = MessageBox.Show("Closing this workbook will result in losing your current import results." + Environment.NewLine +
                                                  "If you are not finished reconciling your statement, do not close this workbook," + Environment.NewLine +
                                                  "or you may lose your work." + Environment.NewLine + Environment.NewLine +
                                                  "Are you sure you want to close the workbook?", "Confirm Close?", MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    // cancel closing the workbook
                    Cancel = true;
                }
                else
                {
                    // if user chose Yes, then delete the sheet
                    Globals.ThisAddIn.Application.DisplayAlerts = false;
                    importResultSheet.Delete();
                    Globals.ThisAddIn.Application.DisplayAlerts = true;
                }
            }
        }

        private void Application_WorkbookOpen(NativeExcel.Workbook Wb)
        {
            SetRibbonState(Wb);
        }

        private static void SetRibbonState(NativeExcel.Workbook Wb)
        {
            bool validWorkbook = false;

            // determine if this workbook is a valid one
            foreach (NativeExcel.Worksheet worksheet in Wb.Application.Worksheets)
            {
                if (worksheet.Name == "Configuration")
                {
                    if (worksheet.Cells[1, 2].Value2 == "Household Budget")
                    {
                        Globals.Ribbons.HouseholdBudgetRibbon.tabHouseholdBudget.Visible = true;
                        validWorkbook = true;
                        break;
                    }
                }
            }

            if (!validWorkbook)
            {
                Globals.Ribbons.HouseholdBudgetRibbon.tabHouseholdBudget.Visible = false;
            }
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
            this.Application.WorkbookOpen += new NativeExcel.AppEvents_WorkbookOpenEventHandler(Application_WorkbookOpen);
            this.Application.WorkbookBeforeClose += new NativeExcel.AppEvents_WorkbookBeforeCloseEventHandler(Application_WorkbookBeforeClose);
            this.Application.WorkbookActivate += new NativeExcel.AppEvents_WorkbookActivateEventHandler(Application_WorkbookActivate);
        }        
        #endregion
    }
}
