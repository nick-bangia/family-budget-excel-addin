using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FamilyBudget.AddIn.Async;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Events;
using FamilyBudget.AddIn.UI;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Common;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Interfaces;
using FamilyBudget.Common.Utilities;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace FamilyBudget.AddIn.Controllers
{
    internal static class LineItemsController
    {
        #region Properties
        // modals
        private static ProgressModal progressModal;
        private static frmSearchItems searchItemsForm;
        private static frmEnterJournalEntries journalEntriesForm;

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
                    Type mapperType = APIResolver.ResolveTypeForInterface(typeof(ILineItemAPI));
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

        // category API interface
        private static ICategoryAPI _categoryAPI;
        private static ICategoryAPI categoryAPI
        {
            get
            {
                if (_categoryAPI == null)
                {
                    // get the configured name of the interface to use to work with goals
                    Type mapperType = APIResolver.ResolveTypeForInterface(typeof(ICategoryAPI));
                    if (mapperType != null)
                    {
                        _categoryAPI = (ICategoryAPI)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _categoryAPI = null;
                    }
                }

                return _categoryAPI;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_LineItemsController");
        #endregion

        #region Event Handlers
        internal static void btnAddNewItems_Click(object sender, RibbonControlEventArgs e)
        {
            WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.NEW_ENTRIES);
            Globals.Ribbons.FamilyBudgetRibbon.btnPreProcessItems.Enabled = true;
            Globals.Ribbons.FamilyBudgetRibbon.btnSave.Enabled = false;
        }

        internal static void btnPreProcessItems_Click(object sender, RibbonControlEventArgs e)
        {
            WorksheetDataController.PreProcessNewItems();
        }

        internal static void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            List<DenormalizedLineItem> worksheetItems = WorksheetDataController.GetLineItemsToSave(DataWorksheetType.NEW_ENTRIES);
            SaveLineItems(updateWorksheet: true, lineItemsToSave: worksheetItems);
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
                    MasterDataController.PopulateMasterDataTable(lineItemAPI.GetAllLineItems(true));
                    GoalsDataController.PopulateGoalsDataTable(categoryAPI.GetGoalSummaries(true));
                    WorkbookUtil.RefreshPivotTables();

                    // update the new items worksheet object with the latest line items
                    WorksheetDataController.BackgroundWorkCompleted(DataWorksheetType.NEW_ENTRIES, lineItems);
                    Globals.Ribbons.FamilyBudgetRibbon.btnSave.Enabled = false;
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
                    Globals.Ribbons.FamilyBudgetRibbon.btnPreProcessItems.Enabled = false;
                    Globals.Ribbons.FamilyBudgetRibbon.btnSave.Enabled = true;
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

        internal static void btnAddJournalEntries_Click(object sender, RibbonControlEventArgs e)
        {
            journalEntriesForm = new frmEnterJournalEntries();
            journalEntriesForm.Show();
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
                    string itemKey = WorksheetDataController.GetItemKey(e.ListIndex, e.worksheetType);

                    DenormalizedLineItem item;
                    if (!String.IsNullOrWhiteSpace(itemKey) && !itemKey.Contains("failed"))
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

                        if (!String.IsNullOrWhiteSpace(itemKey))
                        {
                            error = error + " Unique ID: " + itemKey;
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
        internal static void SaveJournalEntries(BindingList<JournalEntry> journalEntries)
        {
            // initialize the line items list
            List<DenormalizedLineItem> lineItems = new List<DenormalizedLineItem>();
            
            foreach (JournalEntry je in journalEntries)
            {
                DenormalizedLineItem lineItem1 = new DenormalizedLineItem()
                {
                    Year = je.OnDate.Year,
                    MonthInt = (short)je.OnDate.Month,
                    Day = (short)je.OnDate.Day,
                    DayOfWeekId = (short)(((short)je.OnDate.DayOfWeek) + 1),
                    Quarter = DateUtil.GetQuarterForMonth(je.OnDate.Month),
                    Description = String.Format("Journal: From {0} to {1}; Reason: {2}",
                        je.FromSubcategory.Name, je.ToSubcategory.Name, je.Reason),
                    CategoryKey = je.FromSubcategory.CategoryKey,
                    SubCategoryKey = je.FromSubcategory.Key,
                    PaymentMethodKey = PaymentMethodsController.GetDefaultPaymentMethod().Key,
                    Amount = je.Amount * -1,
                    IsTaxDeductible = false,
                    Type = LineItemType.JOURNAL,
                    Status = LineItemStatus.RECONCILED,
                    SubType = LineItemSubType.DEBIT,
                    IsDeleted = false,
                    IsDuplicate = false,
                    ItemSurrogateKey = -1,
                    APIState = String.Empty
                };

                // add the from line item to the list
                lineItems.Add(lineItem1);

                DenormalizedLineItem lineItem2 = new DenormalizedLineItem()
                {
                    Year = je.OnDate.Year,
                    MonthInt = (short)je.OnDate.Month,
                    Day = (short)je.OnDate.Day,
                    DayOfWeekId = (short)(((short)je.OnDate.DayOfWeek) + 1),
                    Quarter = DateUtil.GetQuarterForMonth(je.OnDate.Month),
                    Description = String.Format("Journal: From {0} to {1}; Reason: {2}",
                        je.FromSubcategory.Name, je.ToSubcategory.Name, je.Reason),
                    CategoryKey = je.ToSubcategory.CategoryKey,
                    SubCategoryKey = je.ToSubcategory.Key,
                    PaymentMethodKey = PaymentMethodsController.GetDefaultPaymentMethod().Key,
                    Amount = je.Amount,
                    IsTaxDeductible = false,
                    Type = LineItemType.JOURNAL,
                    Status = LineItemStatus.RECONCILED,
                    SubType = LineItemSubType.CREDIT,
                    IsDeleted = false,
                    IsDuplicate = false,
                    ItemSurrogateKey = -1,
                    APIState = String.Empty
                };

                // add the to line item to the list
                lineItems.Add(lineItem2);
            }

            // save the line items to via the API
            SaveLineItems(false, lineItems);
        }
        internal static void PopulateDataSheet(bool rebuild)
        {
            if (rebuild)
            {
                logger.Info("Removing the master data sheet.");
                Globals.ThisAddIn.Application.DisplayAlerts = false;
                MasterDataController.RemoveSheet();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }

            logger.Info("Populating the master data sheet.");
            MasterDataController.PopulateMasterDataTable(GetAllLineItems(true));
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

        internal static void DeleteLineItem(string itemKey)
        {
            OperationStatus status = lineItemAPI.DeleteLineItem(itemKey);

            if (status == OperationStatus.FAILURE)
            {
                MessageBox.Show("Unable to delete item with key: " + itemKey.ToString() + Environment.NewLine +
                    "Check that the item exists, or check the log for more details.");
            }
        }

        internal static DenormalizedLineItem UpdateLineItem(DenormalizedLineItem lineItem)
        {
            List<DenormalizedLineItem> lineItemsToUpdate = new List<DenormalizedLineItem>();
            lineItemsToUpdate.Add(lineItem);

            List<DenormalizedLineItem> updatedItems = lineItemAPI.UpdateLineItems(lineItemsToUpdate);

            if (updatedItems[0].APIState.Contains("failed"))
            {
                MessageBox.Show("Unable to update item with key: " + lineItem.Key.ToString() + Environment.NewLine +
                    "Details: " + updatedItems[0].APIState);
            }

            return updatedItems[0];
        }

        internal static void SearchSubmitted(SearchCriteria sc)
        {
            List<DenormalizedLineItem> items = lineItemAPI.GetLineItemsByCriteria(sc);
            bool displayResults = true;

            if (items != null)
            {
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
            }
            else
            {
                // if the results are null, an error occurred with the request
                searchItemsForm.NotifyUser("An error occurred attempting to search line items. Check the log for more details.");
                displayResults = false;
            }

            if (displayResults)
            {
                // otherwise, populate the worksheet
                WorksheetDataController.PopulateNewWorksheet(DataWorksheetType.SEARCH_RESULTS, items);
                searchItemsForm.Close();
            }
        }
                
        internal static DenormalizedLineItem AddNewLineItem(DenormalizedLineItem lineItemToInsert)
        {
            List<DenormalizedLineItem> lineItemsToInsert = new List<DenormalizedLineItem>();
            lineItemsToInsert.Add(lineItemToInsert);

            DenormalizedLineItem insertedItem = lineItemAPI.AddNewLineItems(lineItemsToInsert)[0];

            if (insertedItem.APIState.Contains("failed"))
            {
                MessageBox.Show("Unable to save goal. Check the log for more details." + Environment.NewLine +
                    "Details: " + insertedItem.APIState);
            }

            return insertedItem;
        }

        internal static List<DenormalizedLineItem> GetAllLineItems(bool forceGet)
        {
            return lineItemAPI.GetAllLineItems(forceGet);
        }
        #endregion

        #region Private Methods
        private static void SaveLineItems(bool updateWorksheet, List<DenormalizedLineItem> lineItemsToSave)
        {
            try
            {
                // get an instance of the line item mapper being used, and create the importer
                importer = new LineItemImporter(lineItemAPI, lineItemsToSave, updateWorksheet);
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
