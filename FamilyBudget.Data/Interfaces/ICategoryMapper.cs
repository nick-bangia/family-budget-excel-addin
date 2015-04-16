using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Protocol;
using System.Data.Objects;
using FamilyBudget.Data.Domain;

namespace FamilyBudget.Data.Interfaces
{
    public interface ICategoryMapper
    {
        LiveDataObject GetCategories();
        LiveDataObject GetSubCategories();
        LiveDataObject GetFilteredSubCategories(Guid categoryKey);
        List<SubCategory> GetAllSubCategories();
        Guid? GetCategoryKeyByName(string categoryName);
        Guid? GetSubCategoryKeyByName(string subCategoryName);
        SubCategory GetSubCategoryFor(string itemDescription);
        OperationStatus AddNewCategory(string categoryName);
        OperationStatus AddNewSubCategory(Guid categoryKey, string subCategoryName, string subCategoryPrefix, string accountName, bool isActive, bool isGoal);
        String GetCategoryList(char delimiter);
        String GetSubCategoryList(char delimiter);
    }
}
