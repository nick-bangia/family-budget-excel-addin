using System;
using System.Collections.Generic;
using System.ComponentModel;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.UI;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Common;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;
using FamilyBudget.Common.Interfaces;
using log4net;
using Microsoft.Office.Tools.Ribbon;

namespace FamilyBudget.AddIn.Controllers
{
    internal static class CategoriesController
    {
        #region Properties
        private static frmCategories categoriesForm;
        private static frmNewSubcategory newSubCategoryForm;
        private static frmUpdateCategories updateCategoriesForm;
        private static frmUpdateGoals updateGoalsForm;
        private static frmNewGoal newGoalForm;

        // category mapper interface
        private static ICategoryAPI _categoryApi;
        private static ICategoryAPI categoryApi
        {
            get
            {
                if (_categoryApi == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = APIResolver.ResolveTypeForInterface(typeof(ICategoryAPI));
                    if (mapperType != null)
                    {
                        _categoryApi = (ICategoryAPI)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _categoryApi = null;
                    }
                }

                return _categoryApi;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_CategoriesController");
        #endregion

        #region Event Handlers
        internal static void btnAddSubCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newSubCategoryForm = new frmNewSubcategory();
            newSubCategoryForm.Show();
        }

        internal static void btnAddCategory_Click(object sender, RibbonControlEventArgs e)
        {
            categoriesForm = new frmCategories();
            categoriesForm.Show();
        }

        internal static void btnUpdateCategories_Click(object sender, RibbonControlEventArgs e)
        {
            updateCategoriesForm = new frmUpdateCategories();
            updateCategoriesForm.Show();
        }

        internal static void btnNewGoal_Click(object sender, RibbonControlEventArgs e)
        {
            newGoalForm = new frmNewGoal();
            newGoalForm.Show();
        }

        internal static void btnUpdateGoals_Click(object sender, RibbonControlEventArgs e)
        {
            updateGoalsForm = new frmUpdateGoals();
            updateGoalsForm.Show();
        }

        internal static OperationStatus AddNewSubcategory(Subcategory newSubcategory)
        {
            // create the list of subcategories to add and add the new one to it
            List<Subcategory> newSubcategories = new List<Subcategory>();
            newSubcategories.Add(newSubcategory);

            // send to the API and return the response
            return categoryApi.AddNewSubcategories(newSubcategories);
        }

        internal static OperationStatus AddNewGoal(Goal newGoal)
        {
            // create the list of goals to add and add the new one to it
            List<Goal> newGoals = new List<Goal>();
            newGoals.Add(newGoal);

            // send to the API and return the response
            return categoryApi.AddNewGoals(newGoals);
        }
        #endregion

        #region Internal Methods
        internal static void PopulateSubcategoriesSheet(bool rebuild)
        {
            if (rebuild)
            {
                logger.Info("Removing the subcategory data sheet.");
                Globals.ThisAddIn.Application.DisplayAlerts = false;
                SubCategoriesDataController.RemoveSheet();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }

            logger.Info("Populating the subcategory data sheet.");
            SubCategoriesDataController.PopulateSubcategoriesDataTable(categoryApi.GetSubcategories(rebuild));
        }

        internal static void PopulateGoalsSheet(bool rebuild)
        {
            if (rebuild)
            {
                logger.Info("Removing the goals data sheet.");
                Globals.ThisAddIn.Application.DisplayAlerts = false;
                GoalsDataController.RemoveSheet();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }

            logger.Info("Populating the subcategory data sheet.");
            GoalsDataController.PopulateGoalsDataTable(categoryApi.GetGoalSummaries(rebuild));
        }

        internal static OperationStatus AddNewCategory(string categoryName, bool isActive)
        {
            List<Category> newCategories = new List<Category>();
            newCategories.Add(new Category() { CategoryName = categoryName, IsActive = isActive });
            return categoryApi.AddNewCategories(newCategories);
        }

        internal static BindingList<Category> GetCategories(bool forceGet)
        {
            return categoryApi.GetCategories(forceGet);
        }

        internal static string GetCategoryID(string categoryName)
        {
            return categoryApi.GetCategoryKeyByName(categoryName);
        }

        internal static BindingList<Subcategory> GetSubcategories(bool forceGet)
        {
            return categoryApi.GetSubcategories(forceGet);
        }

        internal static BindingList<Goal> GetGoals(bool forceGet)
        {
            return categoryApi.GetGoals(forceGet);
        }

        internal static BindingList<Subcategory> GetFilteredSubcategories(string categoryKey, bool forceGet = false)
        {
            return categoryApi.GetFilteredSubcategories(categoryKey, forceGet);
        }

        internal static BindingList<Goal> GetFilteredGoals(string categoryKey, bool forceGet = false)
        {
            return categoryApi.GetFilteredGoals(categoryKey, forceGet);
        }

        internal static string GetSubCategoryID(string subCategoryName)
        {
            return categoryApi.GetSubcategoryKeyByName(subCategoryName);
        }

        internal static Subcategory GetSubCategoryFor(string description)
        {
            return categoryApi.GetSubcategoryFor(description);
        }
        #endregion

        #region Private Methods
        private static void CloseNewSubCategoryForm()
        {
            if (newSubCategoryForm != null)
            {
                if (!newSubCategoryForm.IsDisposed)
                {
                    newSubCategoryForm.Close();
                }

                newSubCategoryForm = null;
            }
        }

        private static void CloseNewGoalForm()
        {
            if (newGoalForm != null)
            {
                if (!newGoalForm.IsDisposed)
                {
                    newGoalForm.Close();
                }

                newGoalForm = null;
            }
        }
        #endregion
    }
}
