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

        public OperationStatus AddNewCategory(string categoryName, string subCategoryName, string subCategoryPrefix, bool isActive)
        {
            return SaveCategoryToDB(dimCategories.CreatedimCategories(Guid.NewGuid(), categoryName, subCategoryName, subCategoryPrefix, isActive));
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
                                 where cat.CategoryName == newCategory.CategoryName &&
                                       cat.SubCategoryName == newCategory.SubCategoryName &&
                                       cat.SubCategoryPrefix == newCategory.SubCategoryPrefix
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

        private LiveDataObject GetCategoryDataSource()
        {
            BudgetEntities ctx = new BudgetEntities();
            var categories = ctx.dimCategories.OrderBy("it.CategoryName, it.SubCategoryName");

            return new LiveDataObject()
            {
                dataSource = categories.Execute(MergeOption.OverwriteChanges),
                objectContext = ctx
            };
        }

        #endregion
    }
}
