using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeBudget.Data.Interfaces;
using HomeBudget.Data.Domain;
using HomeBudget.Data.Enums;
using HomeBudget.DataModel;

namespace HomeBudget.Data.Implementation
{
    public class LineItemMapper : ILineItemMapper
    {
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

        public OperationStatus AddNewCategory(string name, string prefix)
        {
            return SaveCategoryToDB(dimCategories.CreatedimCategories(Guid.NewGuid(), name, prefix));
        }

        #endregion

        #region Private Methods

        private List<DenormalizedLineItem> GetAllItemsFromDB()
        {
            List<DenormalizedLineItem> allLineItems = new List<DenormalizedLineItem>();

            using (BudgetEntities ctx = new BudgetEntities())
            {
                var lineItems = from fli in ctx.factLineItems
                                select fli;

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
                        Category = fli.Category.CategoryName,
                        Type = (LineItemType)fli.TypeId
                    };
                    
                    // save to the final list
                    allLineItems.Add(lineItem);
                }
            }

            return allLineItems;
        }

        private LineItemStatus SaveLineItemToDB(LineItem lineItem)
        {
            try
            {
                if (CheckForDuplicate(lineItem) == null)
                {
                    // no duplicate, go on to save the line item
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
                        int type = (int)(lineItem.Amount < 0 ? LineItemType.DEBIT : LineItemType.CREDIT);

                        factLineItems fact = factLineItems.CreatefactLineItems(Guid.NewGuid(), monthId, dayOfMonthId, dayOfWeekId, yearId, categoryKey,
                            lineItem.Description, lineItem.Amount, type);

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
            catch (Exception)
            {
                // if an exception is caught when attempting to save the item, return the SAVE_ERROR enum
                return LineItemStatus.SAVE_ERROR;
            }
        }

        private Guid GetCategoryKeyFor(string itemDescription)
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                // find the category key for a given description by matching the prefix with
                // the beginning of the description. First match wins.
                var categories = from cat in ctx.dimCategories
                                 select cat;

                dimCategories match = categories.FirstOrDefault(f => itemDescription.StartsWith(f.CategoryPrefix));
                if (match != null)
                {
                    return match.CategoryKey;
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

        private OperationStatus SaveCategoryToDB(dimCategories newCategory)
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                var categories = from cat in ctx.dimCategories
                                 where cat.CategoryName == newCategory.CategoryName &&
                                       cat.CategoryPrefix == newCategory.CategoryPrefix
                                 select cat;

                // if a category already exists, return failure
                if (categories.Count() > 0)
                {
                    return OperationStatus.FAILURE;
                }
                               
                // otherwise, add to the DB
                // add object to context
                ctx.dimCategories.AddObject(newCategory);

                // save changes to DB
                ctx.SaveChanges();

                // return success operation
                return OperationStatus.SUCCESS;
            }
        }

        #endregion
    }
}
