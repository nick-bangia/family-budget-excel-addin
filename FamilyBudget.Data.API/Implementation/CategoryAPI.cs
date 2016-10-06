using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FamilyBudget.Common.Config;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Interfaces;
using FamilyBudget.Data.API.Protocol;
using FamilyBudget.Data.API.Utilities;
using log4net;

namespace FamilyBudget.Data.API.Implementation
{
    public class CategoryAPI : ICategoryAPI
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("CategoryAPI");
        private BindingList<Category> categories;
        private BindingList<Subcategory> subcategories;
        private BindingList<Goal> goals;
        private BindingList<GoalSummary> goalSummaries;
        private BindingList<Subcategory> filteredSubcategories;
        private BindingList<Goal> filteredGoals;

        #endregion

        #region ICategoryAPI

        public BindingList<Category> GetCategories(bool forceGet)
        {
            if (categories != null && !forceGet)
            {
                return categories;
            }
            else
            {
                return GetCategoriesFromAPI();
            }
        }

        public BindingList<Subcategory> GetSubcategories(bool forceGet)
        {
            if (subcategories != null && !forceGet)
            {
                return subcategories;
            }
            else
            {
                return GetSubcategoriesFromAPI();
            }
        }

        public BindingList<Goal> GetGoals(bool forceGet)
        {
            if (goals != null && !forceGet)
            {
                return goals;
            }
            else
            {
                return GetGoalsFromAPI();
            }
        }

        public BindingList<GoalSummary> GetGoalSummaries(bool forceGet)
        {
            if (goalSummaries != null && !forceGet)
            {
                return goalSummaries;
            }
            else
            {
                return GetGoalSummariesFromAPI();
            }
        }

        public BindingList<Subcategory> GetFilteredSubcategories(string categoryKey, bool forceGet)
        {
            // Get the full list of subcategories & filter on the categoryKey
            GetSubcategories(forceGet);
            filteredSubcategories = new BindingList<Subcategory>();

            // get filter results
            IEnumerable<Subcategory> results = subcategories.Where(sc => sc.CategoryKey.Equals(categoryKey));

            foreach (Subcategory sc in results)
            {
                filteredSubcategories.Add(new Subcategory()
                {
                    Key = sc.Key,
                    CategoryKey = sc.CategoryKey,
                    CategoryName = sc.CategoryName,
                    AccountKey = sc.AccountKey,
                    AccountName = sc.AccountName,
                    Name = sc.Name,
                    Prefix = sc.Prefix,
                    IsAllocatable = sc.IsAllocatable,
                    IsActive = sc.IsActive
                });
            }

            // attach the ListChanged event handler to the filtered list
            filteredSubcategories.ListChanged += Subcategories_ListChanged;

            return filteredSubcategories;
        }

        public BindingList<Goal> GetFilteredGoals(string categoryKey, bool forceGet)
        {
            // Get the full list of subcategories & filter on the categoryKey
            GetGoals(forceGet);
            filteredGoals = new BindingList<Goal>();

            // get filter results
            IEnumerable<Goal> results = goals.Where(g => g.CategoryKey.Equals(categoryKey));

            foreach (Goal g in results)
            {
                filteredGoals.Add(new Goal()
                {
                    Key = g.Key,
                    CategoryKey = g.CategoryKey,
                    CategoryName = g.CategoryName,
                    AccountKey = g.AccountKey,
                    AccountName = g.AccountName,
                    Name = g.Name,
                    Prefix = g.Prefix,
                    GoalAmount = g.GoalAmount,
                    EstimatedCompletionDate = g.EstimatedCompletionDate,
                    IsAllocatable = g.IsAllocatable,
                    IsActive = g.IsActive
                });
            }

            // attach the ListChanged event handler to the filtered list
            filteredGoals.ListChanged += Goals_ListChanged;

            return filteredGoals;
        }

        public string GetCategoryKeyByName(string categoryName)
        {
            // get the list of categories and find the first element that satisifes the categoryName constraint
            BindingList<Category> categories = GetCategories(false);
            Category selectedCategory = categories.FirstOrDefault(c => c.CategoryName.Equals(categoryName));

            // if no category found, return null. Otherwise, return the category key
            return selectedCategory != null ? selectedCategory.Key : null;
        }

        public string GetSubcategoryKeyByName(string subcategoryName)
        {
            // get the list of subcategories and find the first element that satisifes the subcategoryName constraint
            BindingList<Subcategory> subcategories = GetSubcategories(false);
            Subcategory selectedSubcategory = subcategories.FirstOrDefault(sc => sc.Name.Equals(subcategoryName));

            // if no category found, return null. Otherwise, return the category key
            return selectedSubcategory != null ? selectedSubcategory.Key : null;
        }

        public Subcategory GetSubcategoryFor(string itemDescription)
        {
            // get the list of subcategories and return the element that satisifes the prefix of the itemDescription, or null if none
            BindingList<Subcategory> subcategories = GetSubcategories(false);
            return subcategories.FirstOrDefault(sc => itemDescription.StartsWith(sc.Prefix));
        }

        public OperationStatus AddNewCategories(List<Category> categories)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (categories != null && categories.Count > 0)
            {
                // make the call to the API if the categories list is not null & has members
                APIResponseObject response = PutToAPI(categories, AddInConfiguration.APIConfiguration.Routes.AddCategories);

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> categoryResults;
                status = APIUtil.EvaluateResponse(response, out categoryResults, true);

                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in categoryResults)
                {
                    dynamic dynObj = itemResponse.data;

                    // create the Category from each item dynamically
                    Category cat = new Category()
                    {
                        Key = dynObj.key,
                        CategoryName = dynObj.categoryName,
                        IsActive = dynObj.isActive
                    };

                    // add to the persisted list
                    this.categories.Add(cat);
                }
            }

            // get the operation status based on the API Response
            return status;
        }

        public OperationStatus AddNewSubcategories(List<Subcategory> subcategories)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (subcategories != null && subcategories.Count > 0)
            {
                // make the call to the API if the categories list is not null & has members
                APIResponseObject response = PutToAPI(subcategories, AddInConfiguration.APIConfiguration.Routes.AddSubcategories);

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> subcategoryResults;
                status = APIUtil.EvaluateResponse(response, out subcategoryResults, true);

                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in subcategoryResults)
                {
                    dynamic dynObj = itemResponse.data;

                    // create the PaymentMethod from each item dynamically
                    Subcategory sc = new Subcategory()
                    {
                        Key = dynObj.key,
                        CategoryKey = dynObj.categoryKey,
                        CategoryName = dynObj.categoryName,
                        AccountKey = dynObj.accountKey,
                        AccountName = dynObj.accountName,
                        Name = dynObj.name,
                        Prefix = dynObj.prefix,
                        IsAllocatable = dynObj.isAllocatable,
                        IsActive = dynObj.isActive
                    };

                    // resolve the category name & account name before adding it to the list
                    this.subcategories.Add(sc);
                }
            }

            // get the operation status based on the API Response
            return status;
        }

        public OperationStatus AddNewGoals(List<Goal> goals)
        {
            // initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (goals != null && goals.Count > 0)
            {
                // make the call to the API if the goals list is not null & has members
                APIResponseObject response = PutToAPI(goals, AddInConfiguration.APIConfiguration.Routes.AddGoals);

                // initialize the list of output items, and evaluate the response
                // to get the list & status back
                List<Object> goalResults;
                status = APIUtil.EvaluateResponse(response, out goalResults, true);

                // loop through the response if it is successful, 
                // and add each successful item to the persisted list
                foreach (dynamic itemResponse in goalResults)
                {
                    dynamic dynObj = itemResponse.data;

                    // create the PaymentMethod from each item dynamically
                    Goal g = new Goal()
                    {
                        Key = dynObj.key,
                        CategoryKey = dynObj.categoryKey,
                        CategoryName = dynObj.categoryName,
                        AccountKey = dynObj.accountKey,
                        AccountName = dynObj.accountName,
                        Name = dynObj.name,
                        Prefix = dynObj.prefix,
                        GoalAmount = dynObj.goalAmount,
                        EstimatedCompletionDate = new DateTime(dynObj.estimatedCompletionDate.Value.Ticks).Date,
                        IsAllocatable = dynObj.isAllocatable,
                        IsActive = dynObj.isActive
                    };

                    // resolve the category name & account name before adding it to the list
                    this.goals.Add(g);
                }
            }

            // get the operation status based on the API Response
            return status;
        }

        public OperationStatus UpdateCategories(List<Category> categories)
        {
            // Initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (categories != null && categories.Count > 0)
            {
                APIResponseObject response = PutToAPI(categories, AddInConfiguration.APIConfiguration.Routes.UpdateCategories);

                // get the operation status based on the API Response
                status = APIUtil.EvaluateResponse(response);
            }

            return status;
        }

        public OperationStatus UpdateSubcategories(List<Subcategory> subcategories)
        {
            // Initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (subcategories != null && subcategories.Count > 0)
            {
                APIResponseObject response = PutToAPI(subcategories, AddInConfiguration.APIConfiguration.Routes.UpdateSubcategories);

                // get the operation status based on the API Response
                status = APIUtil.EvaluateResponse(response);
            }

            return status;
        }

        public OperationStatus UpdateGoals(List<Goal> goals)
        {
            // Initialize the return value
            OperationStatus status = OperationStatus.FAILURE;

            if (goals != null && goals.Count > 0)
            {
                APIResponseObject response = PutToAPI(goals, AddInConfiguration.APIConfiguration.Routes.UpdateGoals);

                // get the operation status based on the API Response
                status = APIUtil.EvaluateResponse(response);
            }

            return status;
        }

        #endregion

        #region Private Methods

        private BindingList<Category> GetCategoriesFromAPI()
        {
            // initialize the return list
            categories = new BindingList<Category>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetCategories);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through response data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    Category cat = new Category()
                    {
                        Key = d.key,
                        CategoryName = d.categoryName,
                        IsActive = d.isActive
                    };

                    // add to the list
                    categories.Add(cat);
                }

                // attach to the listUpdated event for this binding list
                categories.ListChanged += Categories_ListChanged;
            }

            // return the list
            return categories;
        }

        private BindingList<Subcategory> GetSubcategoriesFromAPI()
        {
            // initialize the return list
            subcategories = new BindingList<Subcategory>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetSubcategories);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through response data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    Subcategory sc = new Subcategory()
                    {
                        Key = d.key,
                        CategoryKey = d.categoryKey,
                        CategoryName = d.categoryName,
                        AccountKey = d.accountKey,
                        AccountName = d.accountName,
                        Name = d.name,
                        Prefix = d.prefix,
                        IsAllocatable = d.isAllocatable,
                        IsActive = d.isActive
                    };

                    // resolve category name & account name fields and add to the list
                    subcategories.Add(sc);                 
                }

                // attach to the listUpdated event for this binding list
                subcategories.ListChanged += Subcategories_ListChanged;
            }

            // return the list
            return subcategories;
        }

        private BindingList<Goal> GetGoalsFromAPI()
        {
            // initialize the return list
            goals = new BindingList<Goal>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetGoals);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through response data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    Goal g = new Goal()
                    {
                        Key = d.key,
                        CategoryKey = d.categoryKey,
                        CategoryName = d.categoryName,
                        AccountKey = d.accountKey,
                        AccountName = d.accountName,
                        Name = d.name,
                        Prefix = d.prefix,
                        GoalAmount = d.goalAmount,
                        EstimatedCompletionDate = new DateTime(d.estimatedCompletionDate.Value.Ticks).Date,
                        IsAllocatable = d.isAllocatable,
                        IsActive = d.isActive
                    };

                    // resolve category name & account name fields and add to the list
                    goals.Add(g);
                }

                // attach to the listUpdated event for this binding list
                goals.ListChanged += Goals_ListChanged;
            }

            // return the list
            return goals;
        }

        private BindingList<GoalSummary> GetGoalSummariesFromAPI()
        {
            // initialize the return list
            goalSummaries = new BindingList<GoalSummary>();

            // make the call to the API
            APIResponseObject response = APIUtil.Get(AddInConfiguration.APIConfiguration.Routes.GetGoalSummary);

            if (APIUtil.IsSuccessful(response))
            {
                // loop through response data and add to the binding list
                foreach (dynamic d in response.data)
                {
                    GoalSummary gs = new GoalSummary()
                    {
                        Name = d.goalName,
                        TotalSaved = d.totalSaved,
                        GoalAmount = d.goalAmount,
                        TargetCompletionDate = new DateTime(d.targetCompletionDate.Value.Ticks).Date,
                    };

                    // resolve category name & account name fields and add to the list
                    goalSummaries.Add(gs);
                }
            }

            // return the list
            return goalSummaries;
        }

        private APIResponseObject PutToAPI(List<Category> categoriesToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            foreach (Category cat in categoriesToPost)
            {
                postData.data.Add(new
                {
                    key = cat.Key,
                    categoryName = cat.CategoryName,
                    isActive = cat.IsActive
                });
            }

            // make the POST request & return response
            response = APIUtil.Put(target, postData);

            return response;
        }

        private APIResponseObject PutToAPI(List<Subcategory> subcategoriesToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            foreach (Subcategory sc in subcategoriesToPost)
            {
                postData.data.Add(new
                {
                    key = sc.Key,
                    categoryKey = sc.CategoryKey,
                    accountKey = sc.AccountKey,
                    name = sc.Name,
                    prefix = sc.Prefix,
                    isAllocatable = sc.IsAllocatable,
                    isActive = sc.IsActive
                });
            }

            // make the POST request & return response
            response = APIUtil.Put(target, postData);

            return response;
        }

        private APIResponseObject PutToAPI(List<Goal> goalsToPost, string target)
        {
            // initialize the response
            APIResponseObject response = null;

            // construct the data object that will be posted to the API
            APIDataObject postData = new APIDataObject();
            postData.data = new List<Object>();

            foreach (Goal g in goalsToPost)
            {
                postData.data.Add(new
                {
                    key = g.Key,
                    categoryKey = g.CategoryKey,
                    accountKey = g.AccountKey,
                    name = g.Name,
                    prefix = g.Prefix,
                    goalAmount = g.GoalAmount,
                    estimatedCompletionDate = g.EstimatedCompletionDate.ToString("o"),
                    isAllocatable = g.IsAllocatable,
                    isActive = g.IsActive
                });
            }

            // make the POST request & return response
            response = APIUtil.Put(target, postData);

            return response;
        }
        #endregion

        #region Event Handlers
        internal void Categories_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (categories != null)
                {
                    List<Category> categoriesToUpdate = new List<Category>();

                    // get the affected payment method
                    Category catChanged = categories[e.NewIndex];
                    categoriesToUpdate.Add(catChanged);

                    // send the list off to the update method.
                    UpdateCategories(categoriesToUpdate);
                }
            }
        }

        internal void Subcategories_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                List<Subcategory> subcategoriesToUpdate = new List<Subcategory>();

                // get the affected payment method
                BindingList<Subcategory> theList = (BindingList<Subcategory>)sender;
                Subcategory scChanged = theList[e.NewIndex];
                subcategoriesToUpdate.Add(scChanged);

                // send the list off to the update method.
                UpdateSubcategories(subcategoriesToUpdate);
            }
        }

        internal void Goals_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                List<Goal> goalsToUpdate = new List<Goal>();

                // get the affected goal
                BindingList<Goal> theList = (BindingList<Goal>)sender;
                Goal gChanged = theList[e.NewIndex];
                goalsToUpdate.Add(gChanged);

                // send the list off to the update method.
                UpdateGoals(goalsToUpdate);
            }
        }
        #endregion
    }
}
