using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Interfaces;
using LumenWorks.Framework.IO.Csv;
using HouseholdBudget.Data.Enums;

namespace HouseholdBudget.Tools
{
    partial class LineItemCSVImporter : BackgroundWorker
    {
        // the interface to help with mapping each line item to the persistence source
        private ILineItemMapper dataMap;

        // properties
        private FileStream lineItemStream;
        private List<LineItem> importReport;

        // the enum that defines the various string[] positions for an item
        private enum LineItemPosition
        {
            DATE = 0,
            DESCRIPTION = 1,
            AMOUNT = 2
        }

        // constructor
        public LineItemCSVImporter(ILineItemMapper lineItemMapper, FileStream lineItemStream)
        {
            // set up background worker properties
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

            this.dataMap = lineItemMapper;
            this.lineItemStream = lineItemStream;
            this.importReport = new List<LineItem>();
        }

        // method that is called when RunWorkerAsync is called from foreground
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            /* Start performing work, reporting progress at measurable points in the process
             * continuously look for the cancellationPending flag, in case the user cancels
             * if an exception occurs, set the proper exception info, and get out of the process
             */
            
            if (this.lineItemStream != null)
            {
                ReportProgress(0, "Initializing...");

                // use the csv reader to read in the lines
                StreamReader reader = new StreamReader(this.lineItemStream);
                List<string[]> lineItems = ReadCSVFile(reader);
                // set the line item iterator to 0
                int lineItemIterator = 0;

                // iterate through each line and import it into the data store
                while (lineItemIterator < lineItems.Count)
                {
                    if (CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        try
                        {
                            // convert one line item and add it to the data store
                            LineItem lineItem = ConvertToObject(lineItems[lineItemIterator]);

                            // check that the description:
                            //   1) doesn't have the word IGNORE at the beginning
                            //   2) doesn't have the phrase "Beginning balance as of" at the beginning
                            // If so, proceed to commit it to data store
                            if (!lineItem.Description.StartsWith("IGNORE") && !lineItem.Description.StartsWith("Beginning balance as of"))
                            {
                                // check that the line item was successfully imported from CSV file before continuing on
                                if (lineItem.Status != LineItemStatus.IMPORT_ERROR)
                                {
                                    lineItem = this.dataMap.AddNewLineItem(lineItem);

                                    // add the line item to the import report
                                    this.importReport.Add(lineItem);
                                }
                                else
                                {
                                    // if an import error, add the line item to the report only
                                    this.importReport.Add(lineItem);
                                }
                            }

                            // advance the iterator and report progress
                            lineItemIterator += 1;
                            ReportProgress(Convert.ToInt32(Math.Floor((((double)lineItemIterator) / ((double)lineItems.Count)) * 100)),
                                String.Format("Imported {0} out of {1} line item(s)...", lineItemIterator, lineItems.Count));
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
                // The lineItemFileStream property is null, which means the background worker was run prematurely
                // throw an exception
                throw new ApplicationException("The CSV file is not properly defined.");
            }

            // save the report of the import and send to foreground
            e.Result = this.importReport;
        }

        #region Helper Methods
        private LineItem ConvertToObject(string[] line)
        {
            // set up a new line item
            LineItem lineItem = new LineItem();

            try
            {
                lineItem.setDate(line[(int)LineItemPosition.DATE]);
                lineItem.Description = line[(int)LineItemPosition.DESCRIPTION];
                lineItem.Amount = decimal.Parse(line[(int)LineItemPosition.AMOUNT]);
                lineItem.Status = LineItemStatus.IMPORTED;
            }
            catch (Exception)
            {
                // if an exception is caught while converting to a LineItem, set the status to IMPORT error
                lineItem.Status = LineItemStatus.IMPORT_ERROR;
            }

            return lineItem;
        }

        private List<string[]> ReadCSVFile(TextReader reader)
        {
            List<string[]> lineItems = new List<string[]>();
            
            // read in the first 6 lines
            for (int i = 0; i < 6; i++)
            {
                // advance the csv reader until we get past the next 5 lines
                reader.ReadLine();
            }

            // begin reading in the actual csv contents
            using (CsvReader csv = new CsvReader(reader, true))
            {
                // set up the csv reader
                csv.SkipEmptyLines = true;
                csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                csv.DefaultHeaderName = "EmptyHeader";
                csv.DefaultParseErrorAction = ParseErrorAction.ThrowException;
                
                // get the # of fields to account for
                int fieldCount = csv.FieldCount;
                // get the headers
                string[] headers = csv.GetFieldHeaders();

                // iterate through the rest of the CSV file, saving each record to 
                // the line items collection
                while (csv.ReadNextRecord())
                {
                    List<String> line = new List<String>();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        line.Add(csv[i]);
                    }

                    lineItems.Add(line.ToArray());
                }
            }

            return lineItems;
        }

        #endregion
    }
}