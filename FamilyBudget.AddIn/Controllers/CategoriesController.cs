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
            // create the list of subcategories to add and add the goal to it
            List<Subcategory> newGoals = new List<Subcategory>();
            newGoals.Add(newGoal);

            // send to the API and get the response
            OperationStatus goalStatus = categoryApi.AddNewSubcategories(newGoals);

            if (goalStatus == OperationStatus.SUCCESS)
            {
                // Add the negation of the goal amount as a line item for the new subcategory
                decimal negatedGoalAmount = -1 * newGoal.GoalAmount;

                // get the current date to use
                DateTime currentDate = DateTime.Now;

                // get the Payment method to use
                PaymentMethod paymentMethod = PaymentMethodsController.GetPaymentMethodByName("Logical");
                if (paymentMethod == null)
                {
                    paymentMethod = PaymentMethodsController.GetDefaultPaymentMethod();
                }

                // get the new subcategory key
                string goalKey = categoryApi.GetSubcategoryKeyByName(newGoal.SubcategoryName);

                if (!String.IsNullOrWhiteSpace(goalKey))
                {
                    // build out the line item that represents the new goal
                    DenormalizedLineItem goalLineItem = new DenormalizedLineItem()
                    {
                        Year = currentDate.Year,
                        MonthInt = (short)currentDate.Month,
                        Day = (short)currentDate.Day,
                        DayOfWeekId = (short)currentDate.DayOfWeek,
                        Description = "New Goal Set",
                        CategoryKey = newGoal.CategoryKey,
                        SubCategoryKey = goalKey,
                        Amount = negatedGoalAmount,
                        Type = LineItemType.GOAL,
                        SubType = LineItemSubType.GOAL,
                        PaymentMethodKey = paymentMethod.PaymentMethodKey,
                        Status = LineItemStatus.GOAL
                    };

                    // save the line item
                    LineItemsController.AddNewLineItem(goalLineItem);

                    // refresh data & pivot tables
                    MasterDataController.PopulateMasterDataTable(LineItemsController.GetAllLineItems(true));
                    WorkbookUtil.RefreshPivotTables();
                }
            }
            
            return goalStatus;
        }
        #endregion

        #region Internal Methods
        internal static void PopulateSubcategoriesSheet(bool rebuild)
        {
            if (rebuild)
            {
                logger.Info("Removing the subcategory data sheet.");
                Globals.ThisAddIn.Application.DisplayAlerts = false;
                SubCategoriesDataManager.RemoveSheet();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }

            logger.Info("Populating the subcategory data sheet.");
            SubCategoriesDataManager.PopulateSubcategoriesDataTable(categoryApi.GetSubcategories(rebuild));
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
                
        internal static BindingList<Subcategory> GetFilteredSubcategories(string categoryKey, bool forceGet = false)
        {
            return categoryApi.GetFilteredSubcategories(categoryKey, forceGet);
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
