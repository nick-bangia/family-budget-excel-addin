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

        public OperationStatus AddNewCategory(string categoryName)
        {
            return SaveCategoryToDB(dimCategories.CreatedimCategories(Guid.NewGuid(), categoryName));
        }

        public OperationStatus AddNewSubCategory(Guid categoryKey, string subCategoryName, string subCategoryPrefix, bool isActive)
        {
            return SaveSubCategoryToDB(dimSubCategories.CreatedimSubCategories(Guid.NewGuid(), categoryKey, subCategoryName, subCategoryPrefix, isActive));
        }

        public OperationStatus SetCategoryActiveState(Guid categoryId, bool activeState)
        {
            return OperationStatus.SUCCESS;
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
                ctx.dimCategories.AddObject(newCategory);

                // save changes to DB
                ctx.SaveChanges();

                // return success operation
                return OperationStatus.SUCCESS;
            }
        }

        private OperationStatus SaveSubCategoryToDB(dimSubCategories newSubCategory)
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
                ctx.dimSubCategories.AddObject(newSubCategory);

                // save changes to DB
                ctx.SaveChanges();

                // return success operation
                return OperationStatus.SUCCESS;
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

        #endregion
    }
}
