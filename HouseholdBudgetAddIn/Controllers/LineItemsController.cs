using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using HouseholdBudget.Data;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.DataControllers;
using HouseholdBudget.Enums;
using HouseholdBudget.Events;
using HouseholdBudget.Async;
using HouseholdBudget.UI;
using HouseholdBudget.Utilities;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace HouseholdBudget.Controllers
{
    internal static class LineItemsController
    {
        #region Properties
        // modals
        private static ProgressModal progressModal;
        private static frmSearchItems searchItemsForm;

        // importing items
        private static LineItemImporter importer;
        private static LineItemPreprocessor preprocessor;
        private static List<DenormalizedLineItem> lineItems;

        // line item mapper interface
        private static ILineItemAPI _lineItemAPI;
        private static ILineItemAPI lineItemAPI
        {
            get
            {
                if (_lineItemAPI == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(ILineItemAPI));
                    if (mapperType != null)
                    {
                        _lineItemAPI = (ILineItemAPI)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _lineItemAPI = null;
                    }
                }

                return _lineItemAPI;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("HouseholdBudgetAddIn_LineItemController");
        #endregion

        #region Event Handlers
        internal static void btnAddNewItems_Click(object sender, RibbonControlEventArgs e)
        {
            WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.NEW_ENTRIES);
            Globals.Ribbons.HouseholdBudgetRibbon.btnPreProcessItems.Enabled = true;
            Globals.Ribbons.HouseholdBudgetRibbon.btnSave.Enabled = false;
        }

        internal static void btnPreProcessItems_Click(object sender, RibbonControlEventArgs e)
        {
            WorksheetDataController.PreProcessNewItems();
        }

        internal static void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                // get an instance of the line item mapper being used, and create the importer
                importer = new LineItemImporter(lineItemAPI, WorksheetDataController.GetLineItemsToSave(DataWorksheetType.NEW_ENTRIES));
                importer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(importer_RunWorkerCompleted);
                importer.ProgressChanged += new ProgressChangedEventHandler(backgroundThread_ProgressChanged);

                // Show a status modal on the main thread.
                // attempt to close it first in case it is still open
                CloseProgressModal();

                progressModal = new ProgressModal();
                progressModal.OnCancelBtnClicked += new EventHandler(progressModal_importerCancelled);
                progressModal.Show();

                // disable screen updating
                WorkbookUtil.ToggleUpdatingAndAlerts(false);

                importer.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                // close the progress modal in case it was opened
                CloseProgressModal();
                // notify that an exception occurred
                MessageBox.Show("An error occurred while attempting to import the statement: " + Environment.NewLine + Environment.NewLine +
                                "Error Details: " + Environment.NewLine +
                                ex.Message);
            }
        }

        internal static void backgroundThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // When the background worker updates progress, send the number/message to the
            // progressModal
            if (progressModal != null)
            {
                progressModal.UpdateProgress(e.ProgressPercentage, (String)e.UserState);
            }
        }

        internal static void importer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // null out the importer instance so that another import can be run
            importer = null;

            // re-enable screen updating
            WorkbookUtil.ToggleUpdatingAndAlerts(true);

            if (e.Error != null)
            {
                // if errored out, close the modal and output a message to the user
                CloseProgressModal();

                // log the error before reporting it to the UI
                logger.Error("Error occurred while importing transactions.", e.Error);

                // report it to the UI
                MessageBox.Show("An error occurred while importing transactions." + Environment.NewLine +
                    "Please review the error and retry. If you continue to recieve this exception," + Environment.NewLine +
                    "contact the application developer for assistance." + Environment.NewLine + Environment.NewLine +
                    "Error Details:" + Environment.NewLine + e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // if cancelled, just close the modal and exit out of the handler
                CloseProgressModal();
            }
            else
            {
                try
                {
                    // if successful completion, update the workbook accordingly
                    logger.Info("Successfully completed import. Finishing up.");

                    // close the modal after import is complete
                    CloseProgressModal();


                    logger.Info("Refreshing workbook data");
                    lineItems = e.Result as List<DenormalizedLineItem>;

                    // refresh the workbook
                    MasterDataController.PopulateMasterDataTable(lineItemAPI.GetAllLineItems());
                    WorkbookUtil.RefreshPivotTables();

                    // update the new items worksheet object with the latest line items
                    WorksheetDataController.BackgroundWorkCompleted(DataWorksheetType.NEW_ENTRIES, lineItems);
                    Globals.Ribbons.HouseholdBudgetRibbon.btnSave.Enabled = false;
                    WorkbookUtil.ShowWorksheetByName(Properties.Resources.DataWorksheetName + EnumUtil.GetFriendlyName(DataWorksheetType.NEW_ENTRIES));
                }
                catch (Exception ex)
                {
                    // close the modal 
                    CloseProgressModal();

                    // log the exception
                    logger.Error("An error occurred when finishing up the import.", ex);

                    // show the exception to the UI
                    MessageBox.Show("An error occurred while finishing up the import." + Environment.NewLine + Environment.NewLine +
                                    "Error Details:" + Environment.NewLine +
                                    ex.Message);
                }
            }
        }

        internal static void preprocessor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // null out the importer instance so that another import can be run
            preprocessor = null;

            // re-enable screen updating
            WorkbookUtil.ToggleUpdatingAndAlerts(true);

            if (e.Error != null)
            {
                // if errored out, close the modal and output a message to the user
                CloseProgressModal();

                // log the error before reporting it to the UI
                logger.Error("Error occurred while preprocessing transactions.", e.Error);

                // report it to the UI
                MessageBox.Show("An error occurred while preprocessing transactions." + Environment.NewLine +
                    "Please review the error and retry. If you continue to recieve this exception," + Environment.NewLine +
                    "contact the application developer for assistance." + Environment.NewLine + Environment.NewLine +
                    "Error Details:" + Environment.NewLine + e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // if cancelled, just close the modal and exit out of the handler
                CloseProgressModal();
            }
            else
            {
                try
                {
                    // if successful completion, update the workbook accordingly
                    logger.Info("Successfully completed preprocessing. Finishing up.");

                    // close the modal after import is complete
                    CloseProgressModal();

                    // refresh the workbook
                    logger.Info("Refreshing workbook data");
                    lineItems = e.Result as List<DenormalizedLineItem>;
                    WorksheetDataController.BackgroundWorkCompleted(DataWorksheetType.NEW_ENTRIES, lineItems);
                    Globals.Ribbons.HouseholdBudgetRibbon.btnPreProcessItems.Enabled = false;
                    Globals.Ribbons.HouseholdBudgetRibbon.btnSave.Enabled = true;
                }
                catch (Exception ex)
                {
                    // close the modal 
                    CloseProgressModal();

                    // log the exception
                    logger.Error("An error occurred when finishing up the preprocessing.", ex);

                    // show the exception to the UI
                    MessageBox.Show("An error occurred while finishing up the preprocessing." + Environment.NewLine + Environment.NewLine +
                                    "Error Details:" + Environment.NewLine +
                                    ex.Message);
                }
            }

            // re-enable screen updating
            WorkbookUtil.ToggleUpdatingAndAlerts(true);
        }

        internal static void btnGetPendingItems_Click(object sender, RibbonControlEventArgs e)
        {
            SearchCriteria sc = new SearchCriteria()
            {
                Status = (short)LineItemStatus.PENDING
            };

            List<DenormalizedLineItem> items = lineItemAPI.GetLineItemsByCriteria(sc);

            if (items.Count == 0)
            {
                // if no line items are returned, then notify user
                MessageBox.Show("No pending items were found.");
            }
            else
            {
                // otherwise, populate the worksheet
                WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.PENDING, items);
            }
        }

        internal static void btnGetFutureItems_Click(object sender, RibbonControlEventArgs e)
        {
            SearchCriteria sc = new SearchCriteria()
            {
                Status = (short)LineItemStatus.FUTURE
            };

            List<DenormalizedLineItem> items = lineItemAPI.GetLineItemsByCriteria(sc);

            if (items.Count == 0)
            {
                // if no line items are returned, then notify user
                MessageBox.Show("No future items were found.");
            }
            else
            {
                // otherwise, populate the worksheet
                WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.FUTURE, items);
            }
        }

        internal static void btnSearch_Click(object sender, RibbonControlEventArgs e)
        {
            searchItemsForm = new frmSearchItems();
            searchItemsForm.Show();
        }

        internal static void progressModal_importerCancelled(object sender, EventArgs e)
        {
            // if the CancelButton is clicked, set the request cancellation of the worker thread
            if (importer != null)
            {
                importer.CancelAsync();
            }
        }

        internal static void progressModal_preProcessorCancelled(object sender, EventArgs e)
        {
            // if the CancelButton is clicked, set the request cancellation of the worker thread
            if (preprocessor != null)
            {
                preprocessor.CancelAsync();
            }
        }

        internal static void dataWorksheet_ActionRequested(object sender, ActionEventArgs e)
        {
            try
            {
                // if EDIT, then bring up a dialog to edit or delete the line item
                if (e.Action == LineItemActions.EDIT)
                {
                    logger.Info("Attempting to edit a single line item...");
                    Guid itemKey = WorksheetDataController.GetItemKey(e.ListIndex, e.worksheetType);

                    DenormalizedLineItem item;
                    if (itemKey != Guid.Empty)
                    {
                        SearchCriteria sc = new SearchCriteria() { UniqueId = itemKey };
                        item = lineItemAPI.GetFirstLineItemByCriteria(sc);
                    }
                    else
                    {
                        item = WorksheetDataController.GetItem(e.Index, e.worksheetType);
                    }


                    // bring up dialog to edit or delete the line item
                    if (item != null)
                    {
                        frmItem itemForm = new frmItem(e.ListIndex, e.Index, e.worksheetType);
                        itemForm.Show();
                        itemForm.HydrateForm(item);
                        itemForm.Focus();
                    }
                    else
                    {
                        // define the error message and log it, and show it to the user
                        string error = "Unable to access item!";

                        if (itemKey != Guid.Empty)
                        {
                            error = error + " Unique ID: " + item.UniqueKey.ToString();
                        }

                        error = error + Environment.NewLine + Environment.NewLine + "Check that this item actually exists!";

                        logger.Error(error);

                        MessageBox.Show(error);
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception
                logger.Error("An error occurred while attempting to edit a line item.", ex);

                MessageBox.Show("An error occurred while attempting to edit a line item." + Environment.NewLine + Environment.NewLine +
                                "Error Details:" + Environment.NewLine +
                                ex.Message);
            }
        }
        #endregion

        #region Internal Methods
        internal static void RebuildDataSheet()
        {
            Globals.ThisAddIn.Application.DisplayAlerts = false;
            MasterDataController.RemoveSheet();
            Globals.ThisAddIn.Application.DisplayAlerts = true;
            PopulateDataSheet();
        }

        internal static void PopulateDataSheet()
        {
            logger.Info("Populating the data sheet.");
            MasterDataController.PopulateMasterDataTable(GetAllLineItems());
        }

        internal static void PreprocessLineItems(VstoExcel.ListObject listObject, List<DenormalizedLineItem> lineItems)
        {
            try
            {
                // get an instance of the line item mapper being used, and create the importer
                preprocessor = new LineItemPreprocessor(listObject, lineItems);
                preprocessor.RunWorkerCompleted += new RunWorkerCompletedEventHandler(preprocessor_RunWorkerCompleted);
                preprocessor.ProgressChanged += new ProgressChangedEventHandler(backgroundThread_ProgressChanged);

                // Show a status modal on the main thread.
                // attempt to close it first in case it is still open
                CloseProgressModal();

                progressModal = new ProgressModal();
                progressModal.OnCancelBtnClicked += new EventHandler(progressModal_preProcessorCancelled);
                progressModal.Show();

                // disable screen updating
                WorkbookUtil.ToggleUpdatingAndAlerts(false);

                preprocessor.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                // close the progress modal in case it was opened
                CloseProgressModal();
                // notify that an exception occurred
                MessageBox.Show("An error occurred while attempting to import the statement: " + Environment.NewLine + Environment.NewLine +
                                "Error Details: " + Environment.NewLine +
                                ex.Message);
            }
        }

        internal static void DeleteLineItem(Guid itemKey)
        {
            OperationStatus status = lineItemAPI.DeleteLineItem(itemKey);

            if (status == OperationStatus.FAILURE)
            {
                MessageBox.Show("Unable to delete item with key: " + itemKey.ToString() + Environment.NewLine +
                    "Check that the item exists, or check the log for more details.");
            }
        }

        internal static void UpdateLineItem(DenormalizedLineItem lineItem)
        {
            OperationStatus status = lineItemAPI.UpdateLineItem(lineItem);

            if (status == OperationStatus.FAILURE)
            {
                MessageBox.Show("Unable to update item with key: " + lineItem.UniqueKey.ToString() + Environment.NewLine +
                    "Check that the item exists, or check the log for more details.");
            }
        }

        internal static void SearchSubmitted(SearchCriteria sc)
        {
            List<DenormalizedLineItem> items = lineItemAPI.GetLineItemsByCriteria(sc);
            bool displayResults = true;

            if (items.Count == 0)
            {
                // if no line items are returned, then notify user
                searchItemsForm.NotifyUser("No items found! Please change your search criteria.");
                displayResults = false;
            }
            else if (items.Count > Convert.ToInt32(Properties.Resources.MaxSearchResults))
            {
                // number of items exceeds the max search results, so truncate the results and notify user
                int startIndex = Convert.ToInt32(Properties.Resources.MaxSearchResults);
                items.RemoveRange(startIndex, items.Count - startIndex);
                searchItemsForm.NotifyUser(
                    "Search results exceeded max results size of " +
                    startIndex.ToString() + " items. Results have been truncated." + Environment.NewLine +
                    Environment.NewLine + "Update your search criteria to limit number of results.");
            }

            if (displayResults)
            {
                // otherwise, populate the worksheet
                WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.SEARCH_RESULTS, items);
                searchItemsForm.Close();
            }
        }

        internal static DenormalizedLineItem SaveNewLineItem(DenormalizedLineItem lineItem)
        {
            throw new NotImplementedException();
        }

        internal static void AddNewLineItem(DenormalizedLineItem goalLineItem)
        {
            lineItemAPI.AddNewLineItem(goalLineItem);
        }

        internal static List<DenormalizedLineItem> GetAllLineItems()
        {
            return lineItemAPI.GetAllLineItems();
        }
        #endregion

        #region Private Methods
        private static void CloseProgressModal()
        {
            if (progressModal != null)
            {
                if (!progressModal.IsDisposed)
                {
                    progressModal.Close();
                }

                progressModal = null;
            }
        }
        #endregion
    }
}
