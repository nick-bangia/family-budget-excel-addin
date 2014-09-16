using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using log4net;
using HouseholdBudget.Data;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Enums;
using HouseholdBudget.Events;
using HouseholdBudget.Tools;
using HouseholdBudget.UI;
using Microsoft.Office.Tools.Ribbon;
using NativeExcel = Microsoft.Office.Interop.Excel;
using VstoExcel = Microsoft.Office.Tools.Excel;
using HouseholdBudget.Utilities;

namespace HouseholdBudget
{
    internal static class Controller
    {
        #region Properties
        // modals
        private static ProgressModal progressModal;
        private static frmNewCategory newCategoryForm;
        private static frmNewSubCategory newSubCategoryForm;
        private static frmUpdateCategories updateCategoriesForm;
        private static frmPaymentMethods paymentMethodsForm;
        private static frmSearchItems searchItemsForm;

        // importing items
        private static LineItemImporter importer;
        private static LineItemPreprocessor preprocessor;
        private static List<DenormalizedLineItem> lineItems;
        
        // line item mapper interface
        private static ILineItemMapper _lineItemMapper;
        private static ILineItemMapper lineItemMapper
        {
            get
            {
                if (_lineItemMapper == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(ILineItemMapper));
                    if (mapperType != null)
                    {
                        _lineItemMapper = (ILineItemMapper)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _lineItemMapper = null;
                    }
                }

                return _lineItemMapper;
            }
        }
        
        // category mapper interface
        private static ICategoryMapper _categoryMapper;
        private static ICategoryMapper categoryMapper
        {
            get
            {
                if (_categoryMapper == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(ICategoryMapper));
                    if (mapperType != null)
                    {
                        _categoryMapper = (ICategoryMapper)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _categoryMapper = null;
                    }
                }

                return _categoryMapper;
            }
        }

        // paymentMethod mapper interface
        private static IPaymentMethodMapper _paymentMethodMapper;
        private static IPaymentMethodMapper paymentMethodMapper
        {
            get
            {
                if (_paymentMethodMapper == null)
                {
                    // get the configured name of the interface to manage payment methods
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(IPaymentMethodMapper));
                    if (mapperType != null)
                    {
                        _paymentMethodMapper = (IPaymentMethodMapper)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _paymentMethodMapper = null;
                    }
                }

                return _paymentMethodMapper;
            }
        }
        
        // excel helpers
        private enum DefaultWorksheets
        {
            Views = 1,
            Charts = 2,
            Tools = 3,
            Data = 4
        }    

        // logger
        private static readonly ILog logger = LogManager.GetLogger("HouseholdBudgetAddIn_Controller");
        #endregion

        internal static void btnAddNewItems_Click(object sender, RibbonControlEventArgs e)
        {
            DataWorksheetManager.PopulateNewWorksheet(DataWorksheetType.NEW_ENTRIES);
            Globals.Ribbons.HouseholdBudgetRibbon.btnPreProcessItems.Enabled = true;
            Globals.Ribbons.HouseholdBudgetRibbon.btnSave.Enabled = false;
        }

        internal static void btnPreProcessItems_Click(object sender, RibbonControlEventArgs e)
        {
            DataWorksheetManager.PreProcessNewItems();
        }

        internal static void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                // get an instance of the line item mapper being used, and create the importer
                importer = new LineItemImporter(lineItemMapper, DataWorksheetManager.GetLineItemsToSave(DataWorksheetType.NEW_ENTRIES));
                importer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(importer_RunWorkerCompleted);
                importer.ProgressChanged += new ProgressChangedEventHandler(backgroundThread_ProgressChanged);

                // Show a status modal on the main thread.
                // attempt to close it first in case it is still open
                CloseProgressModal();

                progressModal = new ProgressModal();
                progressModal.OnCancelBtnClicked += new EventHandler(progressModal_importerCancelled);
                progressModal.Show();

                // disable screen updating
                ToggleUpdatingAndAlerts(false);
                
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
            ToggleUpdatingAndAlerts(true);

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
                    DataManager.PopulateMasterDataTable(lineItemMapper.GetAllLineItems());
                    RefreshPivotTables();
                    
                    // update the new items worksheet object with the latest line items
                    DataWorksheetManager.BackgroundWorkCompleted(DataWorksheetType.NEW_ENTRIES, lineItems);
                    Globals.Ribbons.HouseholdBudgetRibbon.btnSave.Enabled = false;
                    ShowWorksheetByName(Properties.Resources.DataWorksheetName + EnumUtil.GetFriendlyName(DataWorksheetType.NEW_ENTRIES));
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
            ToggleUpdatingAndAlerts(true);

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
                    DataWorksheetManager.BackgroundWorkCompleted(DataWorksheetType.NEW_ENTRIES, lineItems);
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
            ToggleUpdatingAndAlerts(true);
        }

        internal static void btnAddSubCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newSubCategoryForm = new frmNewSubCategory();
            newSubCategoryForm.SubCategorySaved += new EventHandler<SubCategoryEventArgs>(newSubCategoryForm_SubCategorySaved);
            newSubCategoryForm.UserCancelled += new EventHandler<CategoryControlEventArgs>(categoryForm_UserCancelled);
            newSubCategoryForm.Show();
        }

        internal static void btnAddCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newCategoryForm = new frmNewCategory();
            newCategoryForm.CategorySaved += new EventHandler<CategoryEventArgs>(newCategoryForm_CategorySaved);
            newCategoryForm.UserCancelled += new EventHandler<CategoryControlEventArgs>(categoryForm_UserCancelled);
            newCategoryForm.Show();
        }

        internal static void btnUpdateCategories_Click(object sender, RibbonControlEventArgs e)
        {
            updateCategoriesForm = new frmUpdateCategories();
            updateCategoriesForm.Show();
        }

        internal static void btnRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            RebuildDataSheet();
            RefreshPivotTables();
            ShowFirstWorksheet();
        }

        internal static void btnGetPendingItems_Click(object sender, RibbonControlEventArgs e)
        {
            SearchCriteria sc = new SearchCriteria()
            {
                Status = (short)LineItemStatus.PENDING
            };
            
            List<DenormalizedLineItem> items = lineItemMapper.GetLineItemsByCriteria(sc);
                        
            if (items.Count == 0)
            {
                // if no line items are returned, then notify user
                MessageBox.Show("No pending items were found.");
            }
            else
            {
                // otherwise, populate the worksheet
                DataWorksheetManager.PopulateNewWorksheet(DataWorksheetType.PENDING, items);
            }
        }

        internal static void btnGetFutureItems_Click(object sender, RibbonControlEventArgs e)
        {
            SearchCriteria sc = new SearchCriteria()
            {
                Status = (short)LineItemStatus.FUTURE
            };
            
            List<DenormalizedLineItem> items = lineItemMapper.GetLineItemsByCriteria(sc);

            if (items.Count == 0)
            {
                // if no line items are returned, then notify user
                MessageBox.Show("No future items were found.");
            }
            else
            {
                // otherwise, populate the worksheet
                DataWorksheetManager.PopulateNewWorksheet(DataWorksheetType.FUTURE, items);
            }
        }

        internal static void btnManagePaymentMethods_Click(object sender, RibbonControlEventArgs e)
        {
            paymentMethodsForm = new frmPaymentMethods();
            paymentMethodsForm.Show();
        }

        internal static void btnSearch_Click(object sender, RibbonControlEventArgs e)
        {
            searchItemsForm = new frmSearchItems();
            searchItemsForm.Show();
        }

        internal static void categoryForm_UserCancelled(object sender, CategoryControlEventArgs e)
        {
            if (e.formType == CategoryFormType.ParentCategory)
            {
                // on user request to cancel the New Category form, close the form
                CloseNewCategoryForm();            
            }
            else if (e.formType == CategoryFormType.SubCategory)
            {
                // on user request to cancel the New SubCategory form, close the form
                CloseNewSubCategoryForm();
            }
            
        }

        internal static void newSubCategoryForm_SubCategorySaved(object sender, SubCategoryEventArgs e)
        {
            // attempt to add a new SubCategory to the DB
            logger.Info("Adding a new SubCategory to the DB.");
            OperationStatus addedNewSubCategory = categoryMapper.AddNewSubCategory(e.subCategory.CategoryKey, e.subCategory.SubCategoryName, e.subCategory.SubCategoryPrefix, e.subCategory.IsActive);
            if (addedNewSubCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new subcategory, show a message box to that effect.
                string errorText =  "An error occurred while attempting to add a new SubCategory to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new SubCategory is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present." + Environment.NewLine +
                                    "\t4) The subcategory's code is fits within the constraint of the system.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form, regardless.
            CloseNewSubCategoryForm();
        }

        internal static void newCategoryForm_CategorySaved(object sender, CategoryEventArgs e)
        {
            // attempt to add a new Category to the DB
            logger.Info("Adding a new Category to the DB.");
            OperationStatus addedNewCategory = categoryMapper.AddNewCategory(e.category.CategoryName);
            if (addedNewCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new category, show a message box to that effect.
                string errorText =  "An error occurred while attempting to add a new Category to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new Category is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form, regardless.
            CloseNewCategoryForm();
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

        internal static void importReport_ActionRequested(object sender, ActionEventArgs e)
        {
        //    try
        //    {
        //        // if IMPORT, then attempt to re-import the (hopefully) modified line item
        //        if (e.Action == LineItemActions.IMPORT)
        //        {
        //            logger.Info("Attempting to import a single line item...");
        //            LineItem item = ImportResultsManager.ConvertImportResultToLineItem(e.ListIndex, e.Index);
        //            item = lineItemMapper.AddNewLineItem(item);
        //            ImportResultsManager.ProcessImportResult(item, e.ListIndex, e.Index);
                    
        //            // if the item was successfully saved, update the data sheet
        //            logger.Info("The line item's status is: " + item.Status.ToString());
        //            if (item.Status == LineItemStatus.SAVED)
        //            {
        //                DataManager.PopulateMasterDataTable(lineItemMapper.GetAllLineItems());
        //            }                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // log the exception
        //        logger.Error("An error occurred while attempting to import a line item.", ex);

        //        MessageBox.Show("An error occurred while attempting to import this line item." + Environment.NewLine + Environment.NewLine +
        //                        "Error Details:" + Environment.NewLine +
        //                        ex.Message);
        //    }
        }

        internal static void dataWorksheet_ActionRequested(object sender, ActionEventArgs e)
        {
            try
            {
                // if EDIT, then bring up a dialog to edit or delete the line item
                if (e.Action == LineItemActions.EDIT)
                {
                    logger.Info("Attempting to edit a single line item...");
                    Guid itemKey = DataWorksheetManager.GetItemKey(e.ListIndex, e.worksheetType);

                    DenormalizedLineItem item;
                    if (itemKey != Guid.Empty)
                    {
                        SearchCriteria sc = new SearchCriteria() { UniqueId = itemKey };
                        item = lineItemMapper.GetFirstLineItemByCriteria(sc);
                    }
                    else
                    {
                        item = DataWorksheetManager.GetItem(e.Index, e.worksheetType);
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

        internal static ActionButton AddButtonToListObject(VstoExcel.Worksheet worksheet, 
                                                          VstoExcel.ListObject listObject, 
                                                          LineItemActions action,
                                                          DataWorksheetType worksheetType,
                                                          int resultIndex, 
                                                          int listObjectIndex, 
                                                          int column,
                                                          int NUM_HEADER_ROWS,
                                                          EventHandler<ActionEventArgs> handler)
        {
            // create the button & set its properties
            ActionButton actionButton = new ActionButton();
            actionButton.index = resultIndex;
            actionButton.action = action;
            actionButton.listObjectIndex = listObjectIndex;
            actionButton.worksheetType = worksheetType;
            actionButton.Text = EnumUtil.GetFriendlyName(action);
            string buttonName = "btn" + action.ToString() + (resultIndex).ToString();
            actionButton.Name = buttonName;
                        
            // add it to the list
            int rowIndex = listObjectIndex + NUM_HEADER_ROWS;
            worksheet.Controls.AddControl(actionButton, worksheet.Cells[rowIndex, column], buttonName);
            
            // tie it to the handler
            actionButton.OnActionRequested += handler;

            return actionButton;
        }

        internal static void RebuildDataSheet()
        {
            Globals.ThisAddIn.Application.DisplayAlerts = false;
            DataManager.RemoveSheet();
            Globals.ThisAddIn.Application.DisplayAlerts = true;
            PopulateDataSheet();
        }

        internal static void PopulateDataSheet()
        {
            logger.Info("Populating the data sheet.");
            DataManager.PopulateMasterDataTable(lineItemMapper.GetAllLineItems());
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

        private static void CloseNewCategoryForm()
        {
            if (newCategoryForm != null)
            {
                if (!newCategoryForm.IsDisposed)
                {
                    newCategoryForm.Close();
                }

                newCategoryForm = null;
            }
        }

        private static void CloseNewSubCategoryForm()
        {
            if (newSubCategoryForm != null)
            {
                if (!newSubCategoryForm.IsDisposed)
                {
                    newSubCategoryForm.Close();
                }

                newSubCategoryForm = null;
            }
        }

        internal static void ConfigureWorkbook(NativeExcel.Worksheet configurationWorksheet, string workbookPath)
        {
            // log that configuration is being completed
            logger.Info("Configuring the workbook.");
        }

        internal static LiveDataObject GetCategories()
        {
            return categoryMapper.GetCategories();
        }

        internal static Guid? GetCategoryID(string categoryName)
        {
            return categoryMapper.GetCategoryKeyByName(categoryName);
        }

        internal static String GetCategoryValidationList()
        {
            return categoryMapper.GetCategoryList(',');
        }

        internal static LiveDataObject GetSubCategories()
        {
            return categoryMapper.GetSubCategories();
        }

        internal static String GetSubCategoryValidationList()
        {
            return categoryMapper.GetSubCategoryList(',');
        }

        internal static LiveDataObject GetFilteredSubCategories(Guid categoryKey)
        {
            return categoryMapper.GetFilteredSubCategories(categoryKey);
        }

        internal static Guid? GetSubCategoryID(string subCategoryName)
        {
            return categoryMapper.GetSubCategoryKeyByName(subCategoryName);
        }

        internal static SubCategory GetSubCategoryFor(string description)
        {
            return categoryMapper.GetSubCategoryFor(description);
        }

        internal static LiveDataObject GetPaymentMethods()
        {
            return paymentMethodMapper.GetPaymentMethods();
        }

        internal static String GetPaymentMethodValidationList()
        {
            return paymentMethodMapper.GetPaymentMethodList(',');
        }

        internal static OperationStatus AddNewPaymentMethod(string methodName, bool isActive)
        {
            return paymentMethodMapper.AddNewPaymentMethod(methodName, isActive);
        }

        internal static PaymentMethod GetPaymentMethodByName(string paymentMethodName)
        {
            return paymentMethodMapper.GetPaymentMethodByName(paymentMethodName);
        }

        internal static PaymentMethod GetDefaultPaymentMethod()
        {
            PaymentMethod pm = paymentMethodMapper.GetDefaultPaymentMethod();

            if (pm == null)
            {
                MessageBox.Show("To continue processing items, at least one Payment Method must exist.");
            }

            return pm;
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
                ToggleUpdatingAndAlerts(false);
                
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
            OperationStatus status = lineItemMapper.DeleteLineItem(itemKey);

            if (status == OperationStatus.FAILURE)
            {
                MessageBox.Show("Unable to delete item with key: " + itemKey.ToString() + Environment.NewLine +
                    "Check that the item exists, or check the log for more details.");
            }
        }

        internal static void UpdateLineItem(DenormalizedLineItem lineItem)
        {
            OperationStatus status = lineItemMapper.UpdateLineItem(lineItem);

            if (status == OperationStatus.FAILURE)
            {
                MessageBox.Show("Unable to update item with key: " + lineItem.UniqueKey.ToString() + Environment.NewLine +
                    "Check that the item exists, or check the log for more details.");
            }
        }

        internal static void SearchSubmitted(SearchCriteria sc)
        {
            List<DenormalizedLineItem> items = lineItemMapper.GetLineItemsByCriteria(sc);
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
                DataWorksheetManager.PopulateNewWorksheet(DataWorksheetType.SEARCH_RESULTS, items);
                searchItemsForm.Close();
            }
        }

        internal static DenormalizedLineItem SaveNewLineItem(DenormalizedLineItem lineItem)
        {
            throw new NotImplementedException();
        }
    }
}
