using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Interfaces;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.DataModel;
using log4net;
using System.Data.Objects;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Utilities;
using HouseholdBudget.Data.Domain;

namespace HouseholdBudget.Data.Implementation
{
    public class CategoryMapper : ICategoryMapper
    {
        #region Properties

        private static readonly ILog logger = LogManager.GetLogger("DBCategoryMapper");

        #endregion

        #region Constructor

        public CategoryMapper()
        { 
        }

        #endregion

        #region ICategoryMapper

        public LiveDataObject GetCategories()
        {
            return GetCategoryDataSource();
        }

        public LiveDataObject GetSubCategories()
        {
            return GetSubCategoryDataSource();
        }

        public LiveDataObject GetFilteredSubCategories(Guid categoryKey)
        {
            return GetSubCategoryDataSource(categoryKey);
        }

        public List<SubCategory> GetAllSubCategories()
        {
            return GetAllSubCategoriesFromDB();
        }

        private List<SubCategory> GetAllSubCategoriesFromDB()
        {
            // log start of method
            logger.Info("Beginning retrieval of all subcategories from DB...");
            List<SubCategory> allSubCategories;

            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    logger.Info("Getting all subcategories from DB.");
                    var subCategories = from sc in ctx.dimSubCategories
                                        orderby sc.SubCategoryName
                                        select sc;

                    logger.Info("translating to subcategory list");
                    allSubCategories = GetSubCategoryList(subCategories);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while retreiving subcategories.", ex);
                throw ex;
            }

            logger.Info("Completed retrieval of all subcategories from DB.");
            return allSubCategories;
        }

        private List<SubCategory> GetSubCategoryList(IQueryable<dimSubCategories> subCategories)
        {
            List<SubCategory> subCategoryList = new List<SubCategory>();

            foreach (dimSubCategories sc in subCategories)
            {
                // convert the dimSubCategory to a SubCategory
                SubCategory subCategory = new SubCategory()
                {
                    SubCategoryKey = sc.SubCategoryKey,
                    SubCategoryName = sc.SubCategoryName,
                    SubCategoryPrefix = sc.SubCategoryPrefix,
                    CategoryKey = sc.CategoryKey,
                    CategoryName = sc.ParentCategory.CategoryName,
                    AccountName = sc.AccountName,
                    IsActive = sc.IsActive,
                    IsGoal = sc.IsGoal
                };
                
                // save to the final list
                subCategoryList.Add(subCategory);
            }

            // return the populated list
            return subCategoryList;
        }

        public Guid? GetCategoryKeyByName(string categoryName)
        {
            return GetCategoryKeyByNameFromDB(categoryName);
        }

        public Guid? GetSubCategoryKeyByName(string subCategoryName)
        {
            return GetSubCategoryKeyByNameFromDB(subCategoryName);
        }

        public SubCategory GetSubCategoryFor(string itemDescription)
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
                    return new SubCategory()
                    {
                        SubCategoryKey = match.SubCategoryKey,
                        SubCategoryName = match.SubCategoryName,
                        SubCategoryPrefix = match.SubCategoryPrefix,
                        CategoryKey = match.CategoryKey,
                        CategoryName = match.ParentCategory.CategoryName
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public OperationStatus AddNewCategory(string categoryName)
        {
            return SaveCategoryToDB(dimCategories.CreatedimCategories(Guid.NewGuid(), categoryName));
        }

        public OperationStatus AddNewSubCategory(Guid categoryKey, string subCategoryName, string subCategoryPrefix, string accountName, bool isActive, bool isGoal)
        {
            return SaveSubCategoryToDB(dimSubCategories.CreatedimSubCategories(Guid.NewGuid(), categoryKey, subCategoryName, subCategoryPrefix, isActive, accountName, isGoal));
        }

        public string GetCategoryList(char delimiter)
        {
            // get the enumerable to build list from, and convert it to a delimited string
            return GetCategoryEnumerable().ToDelimitedString(delimiter);
        }

        public string GetSubCategoryList(char delimiter)
        {
            // get the enumerable to build list from, and convert it to a delimited string
            return GetSubCategoryEnumerable().ToDelimitedString(delimiter);
        }

        #endregion

        #region Private Methods

        private OperationStatus SaveCategoryToDB(dimCategories newCategory)
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                var categories = from cat in ctx.dimCategories
                                 where cat.CategoryName == newCategory.CategoryName
                                 select cat;

                // if a category already exists, return failure
                if (categories.Count() > 0)
                {
                    logger.Info("Attempted to add a new category that already exists!");
                    return OperationStatus.FAILURE;
                }

                // otherwise, add to the DB
                // add object to context
                logger.Info(String.Format("Adding a new Category: {0}.", newCategory.CategoryName));
                ctx.dimCategories.AddObject(newCategory);

                // save changes to DB
                ctx.SaveChanges();

                // return success operation
                return OperationStatus.SUCCESS;
            }
        }

        private OperationStatus SaveSubCategoryToDB(dimSubCategories newSubCategory)
        {
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    var subCategories = from subCat in ctx.dimSubCategories
                                        where subCat.SubCategoryName == newSubCategory.SubCategoryName &&
                                              subCat.SubCategoryPrefix == newSubCategory.SubCategoryPrefix
                                        select subCat;

                    // if a category already exists, return failure
                    if (subCategories.Count() > 0)
                    {
                        logger.Info("Attempted to add a new subcategory that already exists!");
                        return OperationStatus.FAILURE;
                    }

                    // otherwise, add to the DB
                    // add object to context
                    logger.Info(String.Format("Adding a new SubCategory: {0}/{1}.", newSubCategory.CategoryKey, newSubCategory.SubCategoryName));
                    ctx.dimSubCategories.AddObject(newSubCategory);

                    // save changes to DB
                    ctx.SaveChanges();

                    // return success operation
                    return OperationStatus.SUCCESS;
                }
            }
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : String.Empty);
                logger.Error(String.Format("An error occurred while attempting to add a new subcategory. Error Text: {0}", exceptionMsg));

                return OperationStatus.FAILURE;
            }
        }

        private LiveDataObject GetCategoryDataSource()
        {
            BudgetEntities ctx = new BudgetEntities();
            var categories = ctx.dimCategories.OrderBy("it.CategoryName");

            return new LiveDataObject()
            {
                dataSource = categories.Execute(MergeOption.OverwriteChanges),
                objectContext = ctx
            };
        }

        private LiveDataObject GetSubCategoryDataSource()
        {
            return GetSubCategoryDataSource((Guid?)null);
        }

        private LiveDataObject GetSubCategoryDataSource(Guid? categoryKey)
        {
            BudgetEntities ctx = new BudgetEntities();
            var subCategories = ctx.dimSubCategories
                                .Where(sc => !categoryKey.HasValue || sc.CategoryKey == categoryKey.Value)
                                .OrderBy(sc => sc.SubCategoryName);
            
            // construct the return object
            return new LiveDataObject()
            {
                dataSource = ((ObjectQuery<dimSubCategories>)subCategories).Execute(MergeOption.OverwriteChanges),
                objectContext = ctx
            };
        }

        private Guid? GetCategoryKeyByNameFromDB(string categoryName)
        {
            Guid categoryKey = Guid.Empty;
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    // get a list of Guids that match the category name
                    // there should realistically only ever be one match
                    List<Guid> matches = (from c in ctx.dimCategories
                                          where c.CategoryName == categoryName
                                          select c.CategoryKey).ToList();

                    if (matches.Count > 0)
                    {
                        // if a match exists, take the first one (there should only by one)
                        categoryKey = matches[0];
                    }

                    return categoryKey;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while attempting to get the category key for the category " + categoryName + "."
                    + Environment.NewLine + Environment.NewLine +
                    "Error details: " + Environment.NewLine +
                    ex.Message);

                return (Guid?)null;
            }
        }

        private Guid? GetSubCategoryKeyByNameFromDB(string subCategoryName)
        {
            Guid subCategoryKey = Guid.Empty;
            try
            {
                using (BudgetEntities ctx = new BudgetEntities())
                {
                    // get a list of Guids that match the category name
                    // there should realistically only ever be one match
                    List<Guid> matches = (from sc in ctx.dimSubCategories
                                          where sc.SubCategoryName == subCategoryName
                                          select sc.SubCategoryKey).ToList();

                    if (matches.Count > 0)
                    {
                        // if a match exists, take the first one (there should only by one)
                        subCategoryKey = matches[0];
                    }

                    return subCategoryKey;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred while attempting to get the SubCategory key for the subcategory " + subCategoryName + "."
                    + Environment.NewLine + Environment.NewLine +
                    "Error details: " + Environment.NewLine +
                    ex.Message);

                return (Guid?)null;
            }
        }

        private IEnumerable<string> GetCategoryEnumerable()
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<string> categories = (from cat in ctx.dimCategories
                                           orderby cat.CategoryName
                                           select cat.CategoryName).ToList<string>();

                return categories;
            }
        }

        private IEnumerable<string> GetSubCategoryEnumerable()
        {
            using (BudgetEntities ctx = new BudgetEntities())
            {
                List<string> subcategories = (from subcat in ctx.dimSubCategories
                                              orderby subcat.SubCategoryName
                                              select subcat.SubCategoryName).ToList<string>();

                return subcategories;
            }
        }
        #endregion
    }
}
