using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.DataModel;
using System.ComponentModel;

namespace HouseholdBudget.Data.Implementation
{
    public class LineItemMapper : ILineItemMapper
    {
        #region Properties
        
        private static readonly ILog logger = LogManager.GetLogger("DBLineItemMapper");
        
        #endregion

        #region Constructor

        public LineItemMapper()
        {
        }

        #endregion

        #region  ILineItemMapper

        public LineItem AddNewLineItem(LineItem lineItem)
        {
            // attempt to insert into the database, and get the status back
            lineItem.Status = SaveLineItemToDB(lineItem);

            return lineItem;
        }
                
        public List<DenormalizedLineItem> GetAllLineItems()
        {
            return GetAllItemsFromDB();
        }

        public LineItem GetLineItem(LineItem lineItem)
        {
            return CheckForDuplicate(lineItem);
        }

        #endregion

        #region Private Methods

        private List<DenormalizedLineItem> GetAllItemsFromDB()
        {
            // log start of method
            logger.Info("Beginning retrieval of all items for active categories from DB...");

            List<DenormalizedLineItem> allLineItems = new List<DenormalizedLineItem>();

            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    logger.Info("Getting all items from DB.");
                    var lineItems = from fli in ctx.factLineItems
                                    where fli.Category.IsActive
                                    select fli;

                    logger.Info("Denormalizing line items.");
                    foreach (factLineItems fli in lineItems)
                    {
                        // convert the factLineitem to a LineItem
                        DenormalizedLineItem lineItem = new DenormalizedLineItem()
                        {
                            Year = fli.YearId,
                            Month = fli.Month.MonthName,
                            Day = fli.DayOfMonthId,
                            DayOfWeek = fli.DayOfWeek.DayName,
                            Amount = fli.Amount,
                            Description = fli.Description,
                            Category = fli.Category.ParentCategory.CategoryName,
                            SubCategory = fli.Category.SubCategoryName,
                            Type = (LineItemType)fli.TypeId,
                            SubType = (LineItemSubType)fli.SubTypeId,
                            Quarter = (Quarters)fli.QuarterId,
                            PaymentMethod = fli.PaymentMethod.PaymentMethodName,
                            Status = fli.Status.StatusName
                        };

                        // save to the final list
                        allLineItems.Add(lineItem);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while retreiving denormalized line items.", ex);
                throw ex;
            }
            
            logger.Info("Completed retrieval of all line items from DB.");
            return allLineItems;
        }

        private LineItemStatus SaveLineItemToDB(LineItem lineItem)
        {
            try
            {
                if (CheckForDuplicate(lineItem) == null)
                {
                    using (BudgetEntities cxt = new BudgetEntities())
                    {
                        /* extract the IDs of the fields that need to be mapped */

                        // if category key is not found, return UNMAPPABLE line item status
                        Guid categoryKey = GetCategoryKeyFor(lineItem.Description);
                        if (categoryKey == Guid.Empty)
                        {
                            return LineItemStatus.UNMAPPABLE;
                        }

                        // month
                        short monthId = (short)lineItem.Date.Month;
                        //day of month
                        short dayOfMonthId = (short)lineItem.Date.Day;
                        // day of week
                        short dayOfWeekId = (short)lineItem.Date.DayOfWeek;
                        // year
                        int yearId = lineItem.Date.Year;
                        // type of transaction
                        // default to EXPENSE
                        int type = (int)LineItemType.EXPENSE;
                        short subType = (short)(lineItem.Amount < 0 ? LineItemSubType.DEBIT : LineItemSubType.CREDIT);
                        // if the description suggests an allocation, change its type
                        if (lineItem.Description.ToLower().Contains("alloc"))
                        {
                            type = (int)LineItemType.ALLOCATION;
                        }
                        // compute the quarter
                        short quarterId = (short)GetQuarterForMonth(monthId);
                        // get the payment method key from the Line Item
                        Guid paymentMethodKey = Guid.Parse("b1c6ae6a-e56b-4c75-8948-255099ec78fe");
                        // get the line item status
                        short statusId = (short)LineItemStatus2.Reconciled;

                        factLineItems fact = factLineItems.CreatefactLineItems(Guid.NewGuid(), monthId, dayOfMonthId, dayOfWeekId, yearId, categoryKey,
                            lineItem.Description, lineItem.Amount, type, quarterId, subType, paymentMethodKey, statusId);

                        // save to DB
                        cxt.factLineItems.AddObject(fact);
                        cxt.SaveChanges();

                        // return SAVED
                        return LineItemStatus.SAVED;
                    }
                }
                else
                {
                    // if duplicate, return a status of duplicate
                    return LineItemStatus.DUPLICATE;
                }
            }
            catch (Exception ex)
            {
                // if an exception is caught when attempting to save the item, return the SAVE_ERROR enum
                logger.Error("An exception was caught while saving a line item!", ex);
                return LineItemStatus.SAVE_ERROR;
            }
        }

        private Quarters GetQuarterForMonth(short monthId)
        {
            Quarters quarter;

            switch (monthId)
            {
                case 1:
                case 2:
                case 3:
                    quarter = Quarters.Q1;
                    break;
                case 4:
                case 5:
                case 6:
                    quarter = Quarters.Q2;
                    break;
                case 7:
                case 8:
                case 9:
                    quarter = Quarters.Q3;
                    break;
                case 10:
                case 11:
                case 12:
                    quarter = Quarters.Q4;
                    break;
                default:
                    // default to Q1 - this shouldn't ever happen
                    quarter = Quarters.Q1;
                    break;
            }


            return quarter;
        }

        private Guid GetCategoryKeyFor(string itemDescription)
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                // find the category key for a given description by matching the prefix with
                // the beginning of the description. First match wins.
                var subCategories = from subCat in ctx.dimSubCategories
                                 select subCat;

                dimSubCategories match = subCategories.FirstOrDefault(f => itemDescription.StartsWith(f.SubCategoryPrefix));
                if (match != null)
                {
                    return match.SubCategoryKey;
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

        private LineItem CheckForDuplicate(LineItem lineItem)
        {
            List<factLineItems> shortList = new List<factLineItems>();
            
            using (BudgetEntities ctx = new BudgetEntities())
            {
                // search for whether a line item w/ the same information already exists
                List<factLineItems> factLineItems = (from fli in ctx.factLineItems
                                                     where fli.Amount == lineItem.Amount
                                                     select fli).ToList<factLineItems>();

                // loop through the short list of same amounts, and check the date. Pull out any matches.
                foreach (factLineItems item in factLineItems)
                {
                    DateTime dateOfTransaction = new DateTime(item.YearId, item.MonthId, item.DayOfMonthId);
                    if (lineItem.Date == dateOfTransaction)
                    {
                        // if the date matches, add to the short list
                        shortList.Add(item);
                    }
                }

                // if there is at least one item in the short list, do a full duplicate check on the uniqueness
                // convert to a LineItem, and check the unique key against each other
                foreach (factLineItems item in shortList)
                {
                    LineItem actualLineItem = new LineItem()
                    {
                        Amount = item.Amount,
                        Description = item.Description,
                        Status = Enums.LineItemStatus.EMPTY
                    };
                    actualLineItem.setDate(item.YearId, item.MonthId, item.DayOfMonthId);

                    // if the checksum for the DB item and the to-be-inserted line item match, return true for duplicate
                    if (actualLineItem.CheckSum == lineItem.CheckSum)
                    {
                        return actualLineItem;
                    }
                }

                // if code reaches here, there is no sign of a duplicate item
                return null;
            }
        }

        #endregion
    }
}
