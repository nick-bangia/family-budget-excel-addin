using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using log4net;
using log4net.Config;
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

        // importing items
        private static LineItemCSVImporter importer;
        private static List<LineItem> importReport;
        private static string selectedFile;
        private static string archiveDirectory;
        
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

        internal static void btnImport_Click(object sender, RibbonControlEventArgs e)
        {
            // get the open file dialog
            System.Windows.Forms.OpenFileDialog fileDialog = new OpenFileDialog();

            // set the filters to *.txt, AllowMultiSelect to false, and set the title of the dialog box
            fileDialog.Title = "Select a statement...";
            fileDialog.Filter = "Comma-Separated File (*.csv)|*.csv";

            DialogResult result = fileDialog.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                try
                {
                    // get the file name and open a stream for it
                    selectedFile = fileDialog.FileName;
                    FileStream stream = new FileStream(selectedFile, FileMode.Open);

                    // get an instance of the line item mapper being used, and create the importer
                    importer = new LineItemCSVImporter(lineItemMapper, stream);
                    importer.RunWorkerCompleted += new RunWorkerCompletedEventHandler(importer_RunWorkerCompleted);
                    importer.ProgressChanged += new ProgressChangedEventHandler(importer_ProgressChanged);

                    // Show a status modal on the main thread.
                    // attempt to close it first in case it is still open
                    CloseProgressModal();

                    progressModal = new ProgressModal();
                    progressModal.OnCancelBtnClicked += new EventHandler(progressModal_CancelClicked);
                    progressModal.Show();

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
        }

        private static void importer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // When the background worker updates progress, send the number/message to the
            // progressModal
            if (progressModal != null)
            {
                progressModal.UpdateProgress(e.ProgressPercentage, (String)e.UserState);
            }
        }

        private static void importer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // null out the tester instance so that another set of tests can be run
            importer = null;

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

                    // archive selected file to archive directory
                    logger.Info("Archiving the imported statement.");
                    if (Directory.Exists(archiveDirectory))
                    {
                        DateTime archiveDT = DateTime.Now;
                        string archiveDTString =
                            archiveDT.Year.ToString() + archiveDT.Month.ToString() + archiveDT.Day.ToString() + "_" +
                            archiveDT.Hour.ToString() + archiveDT.Minute.ToString() + archiveDT.Second.ToString();
                        string archiveFileName = "imported_" + archiveDTString + ".csv";

                        File.Copy(selectedFile, archiveDirectory + "\\" + archiveFileName);
                        File.Delete(selectedFile);
                    }

                    // generate import results
                    logger.Info("Generating import summary.");
                    importReport = e.Result as List<LineItem>;
                    ImportResultsManager.DisplayImportResults(importReport);
                    DataManager.PopulateDataSheet(lineItemMapper.GetAllLineItems());
                    RefreshPivotTables();
                    ShowWorksheetByName(Properties.Resources.ImportResultsListObjectName);
                }
                catch (Exception ex)
                {
                    // close the modal 
                    CloseProgressModal();

                    // log the exception
                    logger.Error("An error occurred when generating the import summary.", ex);

                    // show the exception to the UI
                    MessageBox.Show("An error occurred while generating the import summary." + Environment.NewLine + Environment.NewLine +
                                    "Error Details:" + Environment.NewLine +
                                    ex.Message);
                }
            }
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

        internal static void btnManagePaymentMethods_Click(object sender, RibbonControlEventArgs e)
        {
            paymentMethodsForm = new frmPaymentMethods();
            paymentMethodsForm.Show();
        }

        private static void categoryForm_UserCancelled(object sender, CategoryControlEventArgs e)
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

        private static void newSubCategoryForm_SubCategorySaved(object sender, SubCategoryEventArgs e)
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
                                    "\t3) The required system files for this workbook are present.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form, regardless.
            CloseNewSubCategoryForm();
        }

        private static void newCategoryForm_CategorySaved(object sender, CategoryEventArgs e)
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
        
        private static void progressModal_CancelClicked(object sender, EventArgs e)
        {
            // if the CancelButton is clicked, set the request cancellation of the worker thread
            if (importer != null)
            {
                importer.CancelAsync();
            }
        }

        internal static void importReport_ActionRequested(object sender, ImportActionEventArgs e)
        {
            try
            {
                // if IMPORT, then attempt to re-import the (hopefully) modified line item
                logger.Info("Attempting to import a single line item...");
                if (e.ImportAction == ImportResultActions.IMPORT)
                {
                    LineItem item = ImportResultsManager.ConvertImportResultToLineItem(e.ImportListIndex, e.ImportResultsIndex);
                    item = lineItemMapper.AddNewLineItem(item);
                    ImportResultsManager.ProcessImportResult(item, e.ImportListIndex, e.ImportResultsIndex);
                    
                    // if the item was successfully saved, update the data sheet
                    logger.Info("The line item's status is: " + item.Status.ToString());
                    if (item.Status == LineItemStatus.SAVED)
                    {
                        DataManager.PopulateDataSheet(lineItemMapper.GetAllLineItems());
                    }                    
                }
            }
            catch (Exception ex)
            {
                // log the exception
                logger.Error("An error occurred while attempting to import a line item.", ex);

                MessageBox.Show("An error occurred while attempting to import this line item." + Environment.NewLine + Environment.NewLine +
                                "Error Details:" + Environment.NewLine +
                                ex.Message);
            }
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
            DataManager.PopulateDataSheet(lineItemMapper.GetAllLineItems());
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

            // goes through the configuration sheet, and performs any necessary actions
            string configuredPath = configurationWorksheet.Range[Properties.Resources.ArchiveDirectoryRange].Value2.ToString();
            configuredPath = 
                String.IsNullOrEmpty(configuredPath) ? Properties.Resources.DefaultArchiveDirectory : configuredPath.Replace(@"/", String.Empty);
            
            archiveDirectory = workbookPath + "\\" + configuredPath;
               
            if (!Directory.Exists(archiveDirectory))
            {
                // attempt to create the directory if it doesn't exist yet
                try
                {
                    logger.Info("Archive Directory specified does not exist. Creating it now.");
                    Directory.CreateDirectory(archiveDirectory);
                }
                catch (Exception ex)
                {
                    // log error
                    logger.Error("An error occurred while attempting to create the archive directory.", ex);

                    MessageBox.Show("Unable to create the specified archive directory." + Environment.NewLine +
                        "Please check and fix it, and restart the workbook for changes to take effect." + Environment.NewLine +
                        "Error Details: " + Environment.NewLine + Environment.NewLine +
                        ex.Message);
                }
            }
        }

        internal static LiveDataObject GetCategories()
        {
            return categoryMapper.GetCategories();
        }

        internal static LiveDataObject GetSubCategories()
        {
            return categoryMapper.GetSubCategories();
        }

        internal static LiveDataObject GetFilteredSubCategories(Guid categoryKey)
        {
            return categoryMapper.GetFilteredSubCategories(categoryKey);
        }

        internal static LiveDataObject GetPaymentMethods()
        {
            return paymentMethodMapper.GetPaymentMethods();
        }

        internal static OperationStatus AddNewPaymentMethod(string methodName, bool isActive)
        {
            return paymentMethodMapper.AddNewPaymentMethod(methodName, isActive);
        }
    }
}
