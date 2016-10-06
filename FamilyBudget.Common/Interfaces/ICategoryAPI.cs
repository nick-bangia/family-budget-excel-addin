using System.Collections.Generic;
using System.ComponentModel;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Interfaces
{
    public interface ICategoryAPI
    {
        BindingList<Category> GetCategories(bool forceGet);
        BindingList<Subcategory> GetSubcategories(bool forceGet);
        BindingList<Goal> GetGoals(bool forceGet);
        BindingList<Subcategory> GetFilteredSubcategories(string categoryKey, bool forceGet);
        BindingList<Goal> GetFilteredGoals(string categoryKey, bool forceGet);
        BindingList<GoalSummary> GetGoalSummaries(bool forceGet);
        OperationStatus AddNewCategories(List<Category> categories);
        OperationStatus AddNewSubcategories(List<Subcategory> subcategories);
        OperationStatus AddNewGoals(List<Goal> goals);
        OperationStatus UpdateCategories(List<Category> categories);
        OperationStatus UpdateSubcategories(List<Subcategory> subcategories);
        OperationStatus UpdateGoals(List<Goal> goals);
        string GetCategoryKeyByName(string categoryName);
        string GetSubcategoryKeyByName(string subcategoryName);
        Subcategory GetSubcategoryFor(string itemDescription);
        
    }
}
