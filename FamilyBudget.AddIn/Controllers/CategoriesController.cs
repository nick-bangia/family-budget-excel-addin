using System;
using System.Windows.Forms;
using FamilyBudget.AddIn.DataControllers;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.AddIn.Events;
using FamilyBudget.AddIn.UI;
using FamilyBudget.AddIn.Utilities;
using FamilyBudget.Data;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Interfaces;
using FamilyBudget.Data.Protocol;
using log4net;
using Microsoft.Office.Tools.Ribbon;

namespace FamilyBudget.AddIn.Controllers
{
    internal static class CategoriesController
    {
        #region Properties
        private static frmNewCategory newCategoryForm;
        private static frmNewSubCategory newSubCategoryForm;
        private static frmUpdateCategories updateCategoriesForm;
        private static frmNewGoal newGoalForm;

        // category mapper interface
        private static ICategoryMapper _categoryMapper;
        private static ICategoryMapper categoryMapper
        {
            get
            {
                if (_categoryMapper == null)
                {
                    // get the configured name of the interface to use to import line items to the DB
                    Type mapperType = MapResolver.ResolveTypeForInterface(typeof(ICategoryMapper));
                    if (mapperType != null)
                    {
                        _categoryMapper = (ICategoryMapper)Activator.CreateInstance(mapperType);
                    }
                    else
                    {
                        _categoryMapper = null;
                    }
                }

                return _categoryMapper;
            }
        }

        // logger
        private static readonly ILog logger = LogManager.GetLogger("FamilyBudget.AddIn_CategoriesController");
        #endregion

        #region Event Handlers
        internal static void btnAddSubCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newSubCategoryForm = new frmNewSubCategory();
            newSubCategoryForm.SubCategorySaved += new EventHandler<SubCategoryEventArgs>(newSubCategoryForm_SubCategorySaved);
            newSubCategoryForm.UserCancelled += new EventHandler<CategoryControlEventArgs>(categoryForm_UserCancelled);
            newSubCategoryForm.Show();
        }

        internal static void btnAddCategory_Click(object sender, RibbonControlEventArgs e)
        {
            newCategoryForm = new frmNewCategory();
            newCategoryForm.CategorySaved += new EventHandler<CategoryEventArgs>(newCategoryForm_CategorySaved);
            newCategoryForm.UserCancelled += new EventHandler<CategoryControlEventArgs>(categoryForm_UserCancelled);
            newCategoryForm.Show();
        }

        internal static void btnUpdateCategories_Click(object sender, RibbonControlEventArgs e)
        {
            updateCategoriesForm = new frmUpdateCategories();
            updateCategoriesForm.Show();
        }

        internal static void btnNewGoal_Click(object sender, RibbonControlEventArgs e)
        {
            newGoalForm = new frmNewGoal();
            newGoalForm.GoalSaved += new EventHandler<GoalEventArgs>(newGoalForm_GoalSaved);
            newGoalForm.UserCancelled += new EventHandler<CategoryControlEventArgs>(categoryForm_UserCancelled);
            newGoalForm.Show();
        }

        internal static void categoryForm_UserCancelled(object sender, CategoryControlEventArgs e)
        {
            if (e.formType == CategoryFormType.ParentCategory)
            {
                // on user request to cancel the New Category form, close the form
                CloseNewCategoryForm();
            }
            else if (e.formType == CategoryFormType.SubCategory)
            {
                // on user request to cancel the New SubCategory form, close the form
                CloseNewSubCategoryForm();
            }
            else if (e.formType == CategoryFormType.Goal)
            {
                CloseNewGoalForm();
            }

        }

        internal static void newSubCategoryForm_SubCategorySaved(object sender, SubCategoryEventArgs e)
        {
            // attempt to add a new SubCategory to the DB
            logger.Info("Adding a new SubCategory to the DB.");
            OperationStatus addedNewSubCategory = categoryMapper.AddNewSubCategory(e.subCategory.CategoryKey, e.subCategory.SubCategoryName, e.subCategory.SubCategoryPrefix, e.subCategory.AccountName, e.subCategory.IsActive, e.subCategory.IsGoal);
            if (addedNewSubCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new subcategory, show a message box to that effect.
                string errorText = "An error occurred while attempting to add a new SubCategory to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new SubCategory is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present." + Environment.NewLine +
                                    "\t4) The subcategory's code is fits within the constraint of the system.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form, regardless.
            CloseNewSubCategoryForm();
        }

        internal static void newGoalForm_GoalSaved(object sender, GoalEventArgs e)
        {
            // attempt to add a new goal to the DB. A goal is just a subcategory, but with an added fact of the negation of the goal amount (if it is a Logical goal)
            logger.Info("Adding a new goal to the DB.");
            OperationStatus addedNewSubCategory = categoryMapper.AddNewSubCategory(e.subCategory.CategoryKey, e.subCategory.SubCategoryName, e.subCategory.SubCategoryPrefix, e.subCategory.AccountName, e.subCategory.IsActive, e.subCategory.IsGoal);
            if (addedNewSubCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new subcategory, show a message box to that effect.
                string errorText = "An error occurred while attempting to add a new goal to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new goal (subcategory) is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present." + Environment.NewLine +
                                    "\t4) The goal's (subcategory's) code fits within the constraint of the system.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }
            else if (e.subCategory.IsGoal)
            {
                // Add the negation of the goal amount as a line item for the new subcategory
                decimal negatedGoalAmount = -1 * e.GoalAmount;

                // get the current date to use
                DateTime currentDate = DateTime.Now;

                // get the Payment method to use
                PaymentMethod paymentMethod = PaymentMethodsController.GetPaymentMethodByName("Logical");
                if (paymentMethod == null)
                {
                    paymentMethod = PaymentMethodsController.GetDefaultPaymentMethod();
                }

                // get the new subcategory key
                Guid? goalKey = GetSubCategoryID(e.subCategory.SubCategoryName);

                if (goalKey.HasValue)
                {
                    // build out the line item that represents the new goal
                    DenormalizedLineItem goalLineItem = new DenormalizedLineItem()
                    {
                        Year = currentDate.Year,
                        MonthInt = (short)currentDate.Month,
                        Day = (short)currentDate.Day,
                        DayOfWeekId = (short)currentDate.DayOfWeek,
                        Description = "New Goal Set",
                        CategoryKey = e.subCategory.CategoryKey,
                        SubCategoryKey = goalKey.Value,
                        Amount = negatedGoalAmount,
                        Type = LineItemType.GOAL,
                        SubType = LineItemSubType.GOAL,
                        PaymentMethodKey = paymentMethod.PaymentMethodKey,
                        Status = LineItemStatus.GOAL
                    };

                    // save the line item
                    LineItemsController.AddNewLineItem(goalLineItem);

                    // refresh data & pivot tables
                    MasterDataController.PopulateMasterDataTable(LineItemsController.GetAllLineItems());
                    WorkbookUtil.RefreshPivotTables();
                }
                else
                {
                    string noSubCategoryError = String.Format("Unable to save this goal ({0}), as it was not found in the DB.", e.subCategory.SubCategoryName);
                    logger.Error(noSubCategoryError);
                    MessageBox.Show(noSubCategoryError);
                }
            }

            // close the form, regardless.
            CloseNewGoalForm();
        }

        internal static void newCategoryForm_CategorySaved(object sender, CategoryEventArgs e)
        {
            // attempt to add a new Category to the DB
            logger.Info("Adding a new Category to the DB.");
            OperationStatus addedNewCategory = categoryMapper.AddNewCategory(e.category.CategoryName);
            if (addedNewCategory == OperationStatus.FAILURE)
            {
                // if an error occurred while attempting to write the new category, show a message box to that effect.
                string errorText = "An error occurred while attempting to add a new Category to the DB:" + Environment.NewLine +
                                    "Check that:" + Environment.NewLine +
                                    "\t1) The new Category is not already in the DB." + Environment.NewLine +
                                    "\t2) The DB exists." + Environment.NewLine +
                                    "\t3) The required system files for this workbook are present.";
                logger.Error(errorText);
                MessageBox.Show(errorText);
            }

            // close the form, regardless.
            CloseNewCategoryForm();
        }
        #endregion

        #region Internal Methods
        internal static void PopulateSubCategoriesSheet(bool rebuild)
        {
            if (rebuild)
            {
                logger.Info("Removing the subcategory data sheet.");
                Globals.ThisAddIn.Application.DisplayAlerts = false;
                SubCategoriesDataManager.RemoveSheet();
                Globals.ThisAddIn.Application.DisplayAlerts = true;
            }

            logger.Info("Populating the subcategory data sheet.");
            SubCategoriesDataManager.PopulateSubCategoriesDataTable(categoryMapper.GetAllSubCategories());
        }

        internal static LiveDataObject GetCategories()
        {
            return categoryMapper.GetCategories();
        }

        internal static Guid? GetCategoryID(string categoryName)
        {
            return categoryMapper.GetCategoryKeyByName(categoryName);
        }

        internal static String GetCategoryValidationList()
        {
            return categoryMapper.GetCategoryList(',');
        }

        internal static LiveDataObject GetSubCategories()
        {
            return categoryMapper.GetSubCategories();
        }

        internal static String GetSubCategoryValidationList()
        {
            return categoryMapper.GetSubCategoryList(',');
        }

        internal static LiveDataObject GetFilteredSubCategories(Guid categoryKey)
        {
            return categoryMapper.GetFilteredSubCategories(categoryKey);
        }

        internal static Guid? GetSubCategoryID(string subCategoryName)
        {
            return categoryMapper.GetSubCategoryKeyByName(subCategoryName);
        }

        internal static SubCategory GetSubCategoryFor(string description)
        {
            return categoryMapper.GetSubCategoryFor(description);
        }
        #endregion

        #region Private Methods
        private static void CloseNewCategoryForm()
        {
            if (newCategoryForm != null)
            {
                if (!newCategoryForm.IsDisposed)
                {
                    newCategoryForm.Close();
                }

                newCategoryForm = null;
            }
        }

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
