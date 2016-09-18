using System;
using System.Collections.Generic;
using System.ComponentModel;
using log4net;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Interfaces;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.DataControllers;

namespace FamilyBudget.AddIn.Async
{
    public partial class LineItemImporter : BackgroundWorker
    {
        // the interface to help with mapping each line item to the persistence source
        private ILineItemAPI dataMap;

        // properties
        private List<DenormalizedLineItem> lineItems;

        // ILog interface
        private static readonly ILog logger = LogManager.GetLogger("LineItemImporter");

        // constructor
        public LineItemImporter(ILineItemAPI lineItemMapper, List<DenormalizedLineItem> itemsToImport)
        {
            // set up background worker properties
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            this.lineItems = itemsToImport;
            this.dataMap = lineItemMapper;
        }

        // method that is called when RunWorkerAsync is called from foreground
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            /* Start performing work, reporting progress at measurable points in the process
             * continuously look for the cancellationPending flag, in case the user cancels
             * if an exception occurs, set the proper exception info, and get out of the process
             */
            
            if (this.lineItems != null && this.lineItems.Count > 0)
            {
                ReportProgress(0, "Initializing...");

                // set the line item iterator to 0
                int lineItemIterator = 0;

                // iterate through each line and import it into the data store
                logger.Info("Beginning iteration through line items and saving them to the data store...");
                while (lineItemIterator < lineItems.Count)
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
                            // check that the description:
                            //   1) doesn't have the word IGNORE at the beginning
                            //   2) doesn't have the phrase "Beginning balance as of" at the beginning
                            // If so, proceed to commit it to data store
                            if (!this.lineItems[lineItemIterator].Description.StartsWith("IGNORE") && !this.lineItems[lineItemIterator].Description.StartsWith("Beginning balance as of"))
                            {
                                DenormalizedLineItem lineItem = this.lineItems[lineItemIterator];
                                // skip the item if it has been deleted by the user
                                if (!lineItem.IsDeleted)
                                {
                                    List<DenormalizedLineItem> lineItemsToInsert = new List<DenormalizedLineItem>();
                                    lineItemsToInsert.Add(lineItem);

                                    this.lineItems[lineItemIterator] = this.dataMap.AddNewLineItems(lineItemsToInsert)[0];
                                    WorksheetDataController.UpdateLineItem(lineItem.ItemSurrogateKey, lineItemIterator, DataWorksheetType.NEW_ENTRIES, this.lineItems[lineItemIterator]);
                                }                               
                            }

                            // advance the iterator and report progress
                            lineItemIterator += 1;
                            ReportProgress(Convert.ToInt32(Math.Floor((((double)lineItemIterator) / ((double)lineItems.Count)) * 100)),
                                String.Format("Imported {0} out of {1} line item(s)...", lineItemIterator, lineItems.Count));
                        }
                        catch (Exception)
                        {
                            // throw the exception back to the foreground
                            throw;
                        }
                    }
                }
            }
            else
            {
                // The the lineItems list is null, which means the background worker was run prematurely
                // throw an exception
                throw new ApplicationException("There are no items defined.");
            }

            // save the report of the import and send to foreground
            e.Result = lineItems;

            logger.Info("Completed iteration through line items and saving them to the data store.");
        }
    }
}