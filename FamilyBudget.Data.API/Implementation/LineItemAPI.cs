using System;
using System.Collections.Generic;
using System.Dynamic;
using FamilyBudget.Common.Config;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Interfaces;
using FamilyBudget.Common.Utilities;
using FamilyBudget.Data.API.Protocol;
using FamilyBudget.Data.API.Utilities;
using log4net;

namespace FamilyBudget.Data.API.Implementation
{
    public class LineItemAPI : ILineItemAPI
    {
        #region Properties
        
        private static readonly ILog logger = LogManager.GetLogger("DBLineItemMapper");
        private static List<DenormalizedLineItem> lineItems;
        
        #endregion

        #region Constructor

        public LineItemAPI()
        {
        }

        #endregion

        #region  ILineItemMapper

        public List<DenormalizedLineItem> GetAllLineItems(bool forceGet = false)
        {
            if (lineItems != null && !forceGet)
            {
                return lineItems;
            }
            else
            {
                return GetAllItemsFromAPI();
            }
        }

        public List<DenormalizedLineItem> GetLineItemsByCriteria(SearchCriteria searchCriteria)
        {
            return GetLineItemsFromAPI(searchCriteria);
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

        public List<DenormalizedLineItem> AddNewLineItems(List<DenormalizedLineItem> lineItemsToAdd)
        {
            // only perform the add if there are items to add
            if (lineItemsToAdd != null && lineItemsToAdd.Count > 0)
            {
                // perform the operation & get the response
                APIResponseObject response = PutToAPI(lineItemsToAdd, AddInConfiguration.APIConfiguration.Routes.AddLineItems);

                // process the results into the original list
                ProcessResults(response, lineItemsToAdd);
            }

            // return the items with updated fields
            return lineItemsToAdd;
        }

        public List<DenormalizedLineItem> UpdateLineItems(List<DenormalizedLineItem> lineItemsToUpdate)
        {
            // only perform the update if there are items to update
            if (lineItemsToUpdate != null && lineItemsToUpdate.Count > 0)
            {
                // perform the operation & get the response
                APIResponseObject response = PutToAPI(lineItemsToUpdate, AddInConfiguration.APIConfiguration.Routes.UpdateLineItems);

                // process the results into the original list
                ProcessResults(response, lineItemsToUpdate);   
            }

            // return the items with updated fields
            return lineItemsToUpdate;
        }

        public OperationStatus DeleteLineItem(string itemKey)
        {
            return DeleteLineItemFromDB(itemKey);
        }

        #endregion

        #region Private Methods

        private APIResponseObject PutToAPI(List<DenormalizedLineItem> lineItems, string target)
        {
            logger.Info("About to begin a PUT operation to " + target);
            APIResponseObject response = null;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            // loop through each line item & add to the post data
            foreach (DenormalizedLineItem lineItem in lineItems)
            {
                // check for duplicates only if the lineItem is new (empty UniqueKey)
                DenormalizedLineItem duplicate = String.IsNullOrEmpty(lineItem.UniqueKey) ? CheckForDuplicate(lineItem) : null;
                if (duplicate == null)
                {
                    postData.data.Add(new
                    {
                        uniqueKey = String.IsNullOrWhiteSpace(lineItem.UniqueKey) ? "nil" : lineItem.UniqueKey,
                        monthId = lineItem.MonthInt,
                        day = lineItem.Day,
                        dayOfWeekId = lineItem.DayOfWeekId,
                        year = lineItem.Year,
                        subcategoryKey = lineItem.SubCategoryKey,
                        description = lineItem.Description,
                        amount = lineItem.Amount,
                        typeId = (int)lineItem.Type,
                        subtypeId = (int)lineItem.SubType,
                        quarter = (int)lineItem.Quarter,
                        paymentMethodKey = lineItem.PaymentMethodKey,
                        statusId = (int)lineItem.Status
                    });
                }
                else
                {
                    // mark the line item if it is a duplicate
                    lineItem.IsDuplicate = true;
                }
            }

            // make the PUT request and return the response
            response = APIUtil.Put(target, postData);

            return response;
        }

        private void ProcessResults(APIResponseObject response, List<DenormalizedLineItem> lineItems)
        {
            List<Object> lineItemResults;
            OperationStatus status = APIUtil.EvaluateResponse(response, out lineItemResults, false);

            for (int i = 0; i < lineItems.Count; i++)
            {
                if (!lineItems[i].IsDuplicate)
                {
                    // if the line item is not a duplicate, evaluate the response, and update the
                    // line item accordingly
                    dynamic lineItemResponse = lineItemResults != null ? lineItemResults[i] : null;

                    if (lineItemResponse != null && APIUtil.IsSuccessful(lineItemResponse))
                    {
                        // if successful item, get the data from it
                        dynamic lineItemObj = lineItemResponse.data;

                        lineItems[i].UniqueKey = lineItemObj.uniqueKey;
                        lineItems[i].APIState = "success";
                    }
                    else if (lineItemResponse != null)
                    {
                        // if unsuccessful, set uniqueKey to Guid.Empty
                        lineItems[i].APIState = "failed - " + lineItemResponse.reason;
                    }
                    else
                    {
                        lineItems[i].APIState = "failed - " + response.reason;
                    }
                }
            }
        }
        
        private List<DenormalizedLineItem> GetAllItemsFromAPI()
        {
            // log start of method
            logger.Info("Beginning retrieval of all items for active categories from the API...");
            List<DenormalizedLineItem> allLineItems = null;

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetLineItems);

            if (APIUtil.IsSuccessful(response))
            {
                allLineItems = new List<DenormalizedLineItem>();
                // loop through response data and add to the list
                foreach (dynamic d in response.data)
                {
                    // get the lineItem from the data item
                    DenormalizedLineItem lineItem = GetDenormalizedItemFromDynamic(d);

                    // add it to the list
                    allLineItems.Add(lineItem);
                }
            }
                        
            logger.Info("Completed retrieval of all line items from DB.");
            lineItems = allLineItems;
            return lineItems;
        }

        private List<DenormalizedLineItem> GetLineItemsFromAPI(SearchCriteria searchCriteria)
        {
            // log start of method
            logger.Info("Beginning retrieval of all items matching search criteria");
            List<DenormalizedLineItem> matchingLineItems = null;

            // construct the search post data
            APIDataObject searchPostData = new APIDataObject();
            dynamic searchObject = GetSearchObject(searchCriteria);
            searchPostData.data = new List<Object>();
            searchPostData.data.Add(searchObject);

            // make the call to the API & get the response
            APIResponseObject response = APIUtil.Post(AddInConfiguration.APIConfiguration.Routes.SearchLineItems, searchPostData);
            
            // based on response, loop through and construct the final list
            if (APIUtil.IsSuccessful(response))
            {
                matchingLineItems = new List<DenormalizedLineItem>();
                // loop through response data and add to the list
                foreach (dynamic d in response.data)
                {
                    // get the lineItem from the data item
                    DenormalizedLineItem lineItem = GetDenormalizedItemFromDynamic(d);

                    // add it to the list
                    matchingLineItems.Add(lineItem);
                }
            }
            
            logger.Info("Completed retrieval of all line items from DB.");
            return matchingLineItems;
        }

        private OperationStatus DeleteLineItemFromDB(string itemKey)
        {
            // set up the response object
            APIResponseObject response;

            // set up the querystring dictionary
            Dictionary<string, string> queryParams = new Dictionary<string,string>();
            queryParams.Add("key", itemKey);

            // make the call to the API to delete the item with key itemKey
            response = APIUtil.Delete(AddInConfiguration.APIConfiguration.Routes.DeleteLineItem, queryParams);

            // evaluate and return the operation status
            return APIUtil.EvaluateResponse(response);
        }

        private DenormalizedLineItem CheckForDuplicate(DenormalizedLineItem lineItem)
        {
            // initialize a short list of potential matches
            List<DenormalizedLineItem> shortList = null;
            // set up the initial search criteria to find any duplicates (amount has to be the same)
            SearchCriteria sc = new SearchCriteria() 
            { 
                AmountCompareOperator = Comparators.EQUAL,
                CompareToMinAmount = lineItem.Amount 
            };

            // get the short list
            shortList = GetLineItemsFromAPI(sc);

            // if there is at least one item in the short list, do a full duplicate check on the uniqueness
            // convert to a LineItem, and check the unique key against each other
            foreach (DenormalizedLineItem item in shortList)
            {
                // if the checksum for the DB item and the to-be-inserted line item match, return true for duplicate
                if (lineItem.CheckSum == item.CheckSum)
                {
                    lineItem.IsDuplicate = true;
                    return lineItem;
                }
            }

            // if code reaches here, there is no sign of a duplicate item
            return null;
        }

        private DenormalizedLineItem GetDenormalizedItemFromDynamic(dynamic d)
        {
            DenormalizedLineItem lineItem = new DenormalizedLineItem()
            {
                UniqueKey = d.uniqueKey,
                Year = d.year,
                Month = d.month,
                MonthInt = d.monthId,
                Day = d.day,
                DayOfWeek = d.dayOfWeek,
                DayOfWeekId = d.dayOfWeekId,
                Amount = d.amount,
                Description = d.description,
                Category = d.categoryName,
                CategoryKey = d.categoryKey,
                SubCategory = d.subcategoryName,
                SubCategoryKey = d.subcategoryKey,
                SubCategoryPrefix = d.subcategoryPrefix,
                Type = (LineItemType)d.typeId,
                SubType = (LineItemSubType)d.subtypeId,
                Quarter = (Quarters)d.quarter,
                PaymentMethod = d.paymentMethodName,
                PaymentMethodKey = d.paymentMethodKey,
                AccountName = d.accountName,
                IsGoal = d.isGoal,
                Status = (LineItemStatus)d.statusId,
                IsDeleted = false,
                IsDuplicate = false
            };

            return lineItem;
        }

        private dynamic GetSearchObject(SearchCriteria searchCriteria)
        {
            dynamic searchObject = new ExpandoObject();

            // evaluate each search criteria, and fill in the searchObject if it is valid
            if (!String.IsNullOrWhiteSpace(searchCriteria.UniqueId))
            {
                searchObject.uniqueKey = searchCriteria.UniqueId;
            }

            if (searchCriteria.DateCompareOperator != Comparators.NO_COMPARE)
            {
                searchObject.dateCompareOperator = EnumUtil.GetApiName(searchCriteria.DateCompareOperator);
            }

            if (searchCriteria.CompareToMinDate.HasValue)
            {
                searchObject.minDate = searchCriteria.CompareToMinDate.Value.ToString("u");
            }

            if (searchCriteria.CompareToMaxDate.HasValue)
            {
                searchObject.maxDate = searchCriteria.CompareToMaxDate.Value.ToString("u");
            }

            if (searchCriteria.Year.HasValue)
            {
                searchObject.year = searchCriteria.Year.Value;
            }

            if (searchCriteria.Quarter.HasValue)
            {
                searchObject.quarter = searchCriteria.Quarter.Value;
            }

            if (searchCriteria.Month.HasValue)
            {
                searchObject.month = searchCriteria.Month.Value;
            }

            if (searchCriteria.Day.HasValue)
            {
                searchObject.day = searchCriteria.Day.Value;
            }

            if (searchCriteria.DayOfWeek.HasValue)
            {
                searchObject.dayOfWeek = searchCriteria.DayOfWeek.Value;
            }

            if (!String.IsNullOrWhiteSpace(searchCriteria.DescriptionContains))
            {
                searchObject.descriptionContains = searchCriteria.DescriptionContains;
            }

            if (!String.IsNullOrWhiteSpace(searchCriteria.Category))
            {
                searchObject.categoryKey = searchCriteria.Category;
            }

            if (!String.IsNullOrWhiteSpace(searchCriteria.SubCategory))
            {
                searchObject.subcategoryKey = searchCriteria.SubCategory;
            }

            if (searchCriteria.AmountCompareOperator != Comparators.NO_COMPARE)
            {
                searchObject.amountCompareOperator = EnumUtil.GetApiName(searchCriteria.AmountCompareOperator);
            }

            if (searchCriteria.CompareToMinAmount.HasValue)
            {
                searchObject.minAmount = searchCriteria.CompareToMinAmount.Value;
            }

            if (searchCriteria.CompareToMaxAmount.HasValue)
            {
                searchObject.maxAmount = searchCriteria.CompareToMaxAmount.Value;
            }

            if (searchCriteria.Type.HasValue)
            {
                searchObject.type = searchCriteria.Type.Value;
            }

            if (searchCriteria.SubType.HasValue)
            {
                searchObject.subtype = searchCriteria.SubType.Value;
            }

            if (!String.IsNullOrWhiteSpace(searchCriteria.PaymentMethod))
            {
                searchObject.paymentMethodKey = searchCriteria.PaymentMethod;
            }

            if (searchCriteria.Status.HasValue)
            {
                searchObject.status = searchCriteria.Status.Value;
            }

            // return the search object
            return searchObject;
        }

        #endregion
    }
}
