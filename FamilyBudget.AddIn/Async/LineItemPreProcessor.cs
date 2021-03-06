﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using FamilyBudget.AddIn.Controllers;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Utilities;
using log4net;
using VstoExcel = Microsoft.Office.Tools.Excel;

namespace FamilyBudget.AddIn.Async
{
    public partial class LineItemPreprocessor : BackgroundWorker
    {
        // properties
        private VstoExcel.ListObject listObject;
        private List<DenormalizedLineItem> lineItems;

        // ILog interface
        private static readonly ILog logger = LogManager.GetLogger("LineItemPreprocessor");

        // constructor
        public LineItemPreprocessor(VstoExcel.ListObject listObject, List<DenormalizedLineItem> itemsToImport)
        {
            // set up background worker properties
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            this.listObject = listObject;
            this.lineItems = itemsToImport;
        }

        // method that is called when RunWorkerAsync is called from foreground
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            /* Start performing work, reporting progress at measurable points in the process
             * continuously look for the cancellationPending flag, in case the user cancels
             * if an exception occurs, set the proper exception info, and get out of the process
             */
            
            ReportProgress(0, "Initializing...");
            LineItemType type;
            LineItemStatus status;

            // get the total rows to process
            int totalRows = listObject.ListRows.Count;

            // new up the list if it hasn't been initialized before    
            if (this.lineItems == null)
            {
                this.lineItems = new List<DenormalizedLineItem>();
            }

            // set the line item iterator to 1
            int lineItemIterator = 1;

            if (totalRows != 0)
            {
                // iterate through each line and import it into the data store
                logger.Info("Beginning iteration through line items and pre-processing them");
                while (lineItemIterator <= totalRows)
                {
                    if (CancellationPending)
                    {
                        logger.Info("Import cancelled by user!");
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        try
                        {
                            DateTime date = DateTime.FromOADate(listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.DATE].Value2);
                            string description = listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.DESCRIPTION].Value2;
                            object oAmount = listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.AMOUNT].Value2;
                            decimal amount = oAmount != null ? Convert.ToDecimal(oAmount) : 0.00M;
                            string enteredTaxDeductible = listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.TAX_DEDUCTIBLE].Value2;
                            bool taxDeductible = String.IsNullOrEmpty(enteredTaxDeductible) ? false : 
                                ((enteredTaxDeductible.ToLower().Equals("yes") || enteredTaxDeductible.ToLower().Equals("y")) ? true : false);
                            Subcategory categoryInfo = CategoriesController.GetSubCategoryFor(description);
                            string enteredType = listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.TYPE].Value2;
                            type = Enum.TryParse<LineItemType>(enteredType, true, out type) ? 
                                type : LineItemType.EXPENSE;
                            string enteredStatus = listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.STATUS].Value2;
                            status = Enum.TryParse<LineItemStatus>(enteredStatus, true, out status) ?
                                status : LineItemStatus.RECONCILED;
                            PaymentMethod paymentMethodInfo = PaymentMethodsController.GetPaymentMethodByName(
                                listObject.DataBodyRange.Cells[lineItemIterator, (int)DataWorksheetColumns.PAYMENT_METHOD].Value2);
                            PaymentMethod defaultPaymentMethodInfo = PaymentMethodsController.GetDefaultPaymentMethod();
                            
                            // modify description to remove the category prefix, if the category was found
                            if (categoryInfo != null)
                            {
                                int endOfPrefixIndex = description.IndexOf('-');
                                description = description.Substring((endOfPrefixIndex == -1 ? 0 : endOfPrefixIndex + 2));
                            }

                            // populate the denormalized line item
                            DenormalizedLineItem lineItem = new DenormalizedLineItem();
                            lineItem.Key = null;
                            lineItem.Year = date.Year;
                            lineItem.MonthInt = (short)date.Month;
                            lineItem.Day = (short)date.Day;
                            // add 1 to System.DayOfWeek to comply with ODBC standard
                            lineItem.DayOfWeekId = (short)(((short)date.DayOfWeek) + 1);
                            lineItem.Quarter = DateUtil.GetQuarterForMonth(lineItem.MonthInt);
                            lineItem.Description = description;
                            lineItem.Category = categoryInfo != null ? categoryInfo.CategoryName : null;
                            lineItem.CategoryKey = categoryInfo != null ? categoryInfo.CategoryKey : null;
                            lineItem.SubCategory = categoryInfo != null ? categoryInfo.Name : null;
                            lineItem.SubCategoryKey = categoryInfo != null ? categoryInfo.Key : null;
                            lineItem.PaymentMethod = paymentMethodInfo != null ? paymentMethodInfo.PaymentMethodName : defaultPaymentMethodInfo.PaymentMethodName;
                            lineItem.PaymentMethodKey = paymentMethodInfo != null ? paymentMethodInfo.Key : defaultPaymentMethodInfo.Key;
                            lineItem.Amount = amount;
                            lineItem.IsTaxDeductible = taxDeductible;
                            lineItem.Type = type;
                            lineItem.Status = status;
                            lineItem.SubType = amount <= 0 ? LineItemSubType.DEBIT : LineItemSubType.CREDIT;

                            // populate the new line item's special fields
                            lineItem.IsDeleted = false;
                            lineItem.IsDuplicate = false;
                            lineItem.ItemSurrogateKey = lineItemIterator;
                            lineItem.APIState = String.Empty;

                            this.lineItems.Add(lineItem);

                            // update the list object with category information
                            WorksheetDataController.UpdateLineItem(lineItemIterator, lineItemIterator - 1, DataWorksheetType.NEW_ENTRIES, lineItem);

                            // advance the iterator and report progress
                            lineItemIterator += 1;
                            ReportProgress(Convert.ToInt32(Math.Floor((((double)lineItemIterator) / ((double)totalRows)) * 100)),
                                String.Format("Processed {0} out of {1} line item(s)...", lineItemIterator - 1, totalRows));
                        }
                        catch (Exception ex)
                        {
                            // throw the exception back to the foreground
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                // if no items to pre-process, throw an application exception to the foreground
                throw new ApplicationException("There are no items to pre-process.");
            }
            
            // save the report of the import and send to foreground
            e.Result = lineItems;

            logger.Info("Completed iteration through line items and saving them to the data store.");
        }
    }
}