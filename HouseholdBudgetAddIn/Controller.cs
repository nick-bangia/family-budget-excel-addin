using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HouseholdBudget.Data;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Enums;
using HouseholdBudget.Events;
using HouseholdBudget.Tools;
using HouseholdBudget.UI;
using Microsoft.Office.Tools.Ribbon;

namespace HouseholdBudget
{
    internal static class Controller
    {
        #region Properties
        // modals
        private static ProgressModal progressModal;
        private static frmNewCategory newCategoryForm;

        // importing items
        private static LineItemCSVImporter importer;
        private static List<LineItem> importReport;
        
        // line item mapper interface
        private static ILineItemMapper _mapper;
        private static ILineItemMapper lineItemMapper
        {
            get
            {
                if (_mapper == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(ILineItemMapper));
                    if (mapperType != null)
                    {
                        _mapper = (ILineItemMapper)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _mapper = null;
                    }
                }

                return _mapper;
            }
        }
        private enum DefaultWorksheets
        {
            Views = 1,
            Charts = 2,
            Tools = 3,
            Data = 4
        }    
        #endregion

        internal static void btnImportStatement_Click(object sender, RibbonControlEventArgs e)
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
                    string selectedFile = fileDialog.FileName;
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

                // if an exception occurred during the run, report it to the UI
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
                    importReport = e.Result as List<LineItem>;
                    ImportResultsManager.DisplayImportResults(importReport);
                    DataManager.PopulateDataSheet(lineItemMapper.GetAllLineItems());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while generating the import summary." + Environment.NewLine + Environment.NewLine +
                                    "Error Details:" + Environment.NewLine +
                                    ex.Message);
                }
                finally
                {
                    // close the modal after the workbook has been updated
                    CloseProgressModal();
                }
            }
        }

        internal static void btnAddCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newCategoryForm = new frmNewCategory();
            newCategoryForm.CategorySaved += new EventHandler<CategoryEventArgs>(newCategoryForm_CategorySaved);
            newCategoryForm.UserCancelled += new EventHandler(newCategoryForm_UserCancelled);
            newCategoryForm.Show();
        }

        private static void newCategoryForm_UserCancelled(object sender, EventArgs e)
        {
            // on user cancel, close the form
            CloseNewCategoryForm();            
        }

        private static void newCategoryForm_CategorySaved(object sender, CategoryEventArgs e)
        {
            // attempt to add a new category to the 
            OperationStatus addedNewCategory = lineItemMapper.AddNewCategory(e.CategoryName, e.CategoryPrefix);
            if (addedNewCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new category, show a message box to that effect.
                MessageBox.Show("An error occurred while attempting to add a new category to the DB:" + Environment.NewLine +
                                "Check that:" + Environment.NewLine +
                                "\t1) The new category is not already in the DB." + Environment.NewLine +
                                "\t2) The DB exists." + Environment.NewLine +
                                "\t3) The required system files for this workbook are present.");
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
                if (e.ImportAction == ImportResultActions.IMPORT)
                {
                    LineItem item = ImportResultsManager.ConvertImportResultToLineItem(e.ImportListIndex, e.ImportResultsIndex);
                    item = lineItemMapper.AddNewLineItem(item);
                    ImportResultsManager.ProcessImportResult(item, e.ImportListIndex, e.ImportResultsIndex);
                    DataManager.PopulateDataSheet(lineItemMapper.GetAllLineItems());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while attempting to import this line item." + Environment.NewLine + Environment.NewLine +
                                "Error Details:" + Environment.NewLine +
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
    }
}
