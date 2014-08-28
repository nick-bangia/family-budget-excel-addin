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
using HouseholdBudget.Data.Utilities;

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

        public OperationStatus UpdateLineItem(DenormalizedLineItem lineItem)
        {
            return UpdateLineItemInDB(lineItem);
        }
                
        public List<DenormalizedLineItem> GetAllLineItems()
        {
            return GetAllItemsFromDB();
        }

        public List<DenormalizedLineItem> GetLineItemsByCriteria(SearchCriteria searchCriteria)
        {
            return GetLineItemsFromDB(searchCriteria);
        }

        public DenormalizedLineItem GetFirstLineItemByCriteria(SearchCriteria searchCriteria)
        {
            List<DenormalizedLineItem> list = GetLineItemsByCriteria(searchCriteria);
            
            // process list result
            if (list.Count == 0)
            {
                // if the list has zero items, then return null
                return null;
            }
            else
            {
                // otherwise, return the first item in the list
                return list[0];
            }
        }

        public LineItem GetLineItem(LineItem lineItem)
        {
            return CheckForDuplicate(lineItem);
        }

        public OperationStatus DeleteLineItem(Guid itemKey)
        {
            return DeleteLineItemFromDB(itemKey);
        }

        #endregion

        #region Private Methods

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
                        // if the description suggests an allocation or an adjustment, change its type
                        if (lineItem.Description.ToLower().Contains("alloc"))
                        {
                            type = (int)LineItemType.ALLOCATION;
                        }
                        else if (lineItem.Description.ToLower().Contains("adj"))
                        {
                            type = (int)LineItemType.ADJUSTMENT;
                        }
                        // compute the quarter
                        short quarterId = (short)DateUtil.GetQuarterForMonth(monthId);
                        // get the payment method key from the Line Item
                        Guid paymentMethodKey = Guid.Parse("b1c6ae6a-e56b-4c75-8948-255099ec78fe");
                        // get the line item status
                        short statusId = (short)LineItemStatus2.RECONCILED;

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

        private OperationStatus UpdateLineItemInDB(DenormalizedLineItem lineItem)
        {
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    factLineItems currentLineItem = (from fli in ctx.factLineItems
                                                     where fli.UniqueKey == lineItem.UniqueKey
                                                     select fli).ToList()[0];

                    currentLineItem.YearId = lineItem.Year;
                    currentLineItem.MonthId = lineItem.MonthInt;
                    currentLineItem.DayOfMonthId = lineItem.Day;
                    currentLineItem.DayOfWeekId = lineItem.DayOfWeekId;
                    currentLineItem.QuarterId = (short)lineItem.Quarter;
                    currentLineItem.CategoryKey = lineItem.SubCategoryKey;
                    currentLineItem.Category.CategoryKey = lineItem.CategoryKey;
                    currentLineItem.Description = lineItem.Description;
                    currentLineItem.Amount = lineItem.Amount;
                    currentLineItem.TypeId = (int)lineItem.Type;
                    currentLineItem.PaymentMethodId = lineItem.PaymentMethodKey;
                    currentLineItem.StatusId = (short)lineItem.Status;

                    // save changes
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // if an error occurs, log it, and return failure
                logger.Error("An error occurred while attempting to update a line item." + Environment.NewLine +
                    "Error Details:" + Environment.NewLine +
                    ex.Message);

                return OperationStatus.FAILURE;
            }

            // if code gets here, return success
            return OperationStatus.SUCCESS;
        }

        private List<DenormalizedLineItem> GetAllItemsFromDB()
        {
            // log start of method
            logger.Info("Beginning retrieval of all items for active categories from DB...");
            List<DenormalizedLineItem> allLineItems;

            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    logger.Info("Getting all items from DB.");
                    var lineItems = from fli in ctx.factLineItems
                                    where fli.Category.IsActive
                                    orderby fli.YearId descending, fli.MonthId descending, fli.DayOfMonthId descending
                                    select fli;

                    logger.Info("Denormalizing line items.");
                    allLineItems = DenormalizeLineItems(lineItems);
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

        private List<DenormalizedLineItem> GetLineItemsFromDB(SearchCriteria searchCriteria)
        {
            // log start of method
            logger.Info("Beginning retrieval of all items matching search criteria");
            List<DenormalizedLineItem> matchingLineItems;

            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    logger.Info("Getting items from DB that match search criteria.");
                        
                    // set up the initial base query
                    IQueryable<factLineItems> lineItemQuery = ctx.factLineItems.Where(fli => fli.Category.IsActive);

                    // build the query up based on the search criteria
                    lineItemQuery = searchCriteria.BuildQueryFromCriteria(lineItemQuery);

                    // add ordering to the query
                    lineItemQuery = lineItemQuery.OrderByDescending(fli => fli.YearId)
                                                 .ThenByDescending(fli => fli.MonthId)
                                                 .ThenByDescending(fli => fli.DayOfMonthId);
                    
                    logger.Info("Denormalizing line items.");
                    matchingLineItems = DenormalizeLineItems(lineItemQuery);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while retreiving denormalized line items.", ex);
                throw ex;
            }

            logger.Info("Completed retrieval of all line items from DB.");
            return matchingLineItems;
        }

        private List<DenormalizedLineItem> DenormalizeLineItems(IQueryable<factLineItems> lineItems)
        {
            List<DenormalizedLineItem> lineItemList = new List<DenormalizedLineItem>();

            foreach (factLineItems fli in lineItems)
            {
                // convert the factLineitem to a LineItem
                DenormalizedLineItem lineItem = new DenormalizedLineItem()
                {
                    UniqueKey = fli.UniqueKey,
                    Year = fli.YearId,
                    Month = fli.Month.MonthName,
                    MonthInt = fli.Month.MonthId,
                    Day = fli.DayOfMonthId,
                    DayOfWeek = fli.DayOfWeek.DayName,
                    DayOfWeekId = fli.DayOfWeekId,
                    Amount = fli.Amount,
                    Description = fli.Description,
                    Category = fli.Category.ParentCategory.CategoryName,
                    CategoryKey = fli.Category.ParentCategory.CategoryKey,
                    SubCategory = fli.Category.SubCategoryName,
                    SubCategoryKey = fli.CategoryKey,
                    Type = (LineItemType)fli.TypeId,
                    SubType = (LineItemSubType)fli.SubTypeId,
                    Quarter = (Quarters)fli.QuarterId,
                    PaymentMethod = fli.PaymentMethod.PaymentMethodName,
                    PaymentMethodKey = fli.PaymentMethodId,
                    Status = (LineItemStatus2)fli.Status.StatusId
                };

                // save to the final list
                lineItemList.Add(lineItem);
            }

            // return the populated list
            return lineItemList;
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
        
        private OperationStatus DeleteLineItemFromDB(Guid itemKey)
        {
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    factLineItems lineItem = (from fli in ctx.factLineItems
                                              where fli.UniqueKey == itemKey
                                              select fli).ToList()[0];

                    ctx.factLineItems.DeleteObject(lineItem);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while attempting to delete the line item with key: " +
                    itemKey.ToString() + ". Please make sure this item exists in the DB before deleting it." +
                    Environment.NewLine + Environment.NewLine +
                    "Error Details: " + Environment.NewLine +
                    ex.Message);

                // return failed operation if an error occurs
                return OperationStatus.FAILURE;
            }

            // if we get here, return a successful operation
            return OperationStatus.SUCCESS;
        }

        #endregion
    }
}
