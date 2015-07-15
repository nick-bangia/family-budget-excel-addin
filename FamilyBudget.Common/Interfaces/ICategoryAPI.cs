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
        BindingList<Subcategory> GetFilteredSubcategories(string categoryKey, bool forceGet);
        OperationStatus AddNewCategories(List<Category> categories);
        OperationStatus AddNewSubcategories(List<Subcategory> subcategories);
        OperationStatus UpdateCategories(List<Category> categories);
        OperationStatus UpdateSubcategories(List<Subcategory> subcategories);
        string GetCategoryKeyByName(string categoryName);
        string GetSubcategoryKeyByName(string subcategoryName);
        Subcategory GetSubcategoryFor(string itemDescription);
        
    }
}
