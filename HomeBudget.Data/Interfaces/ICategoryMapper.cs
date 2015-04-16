using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Protocol;
using System.Data.Objects;
using HouseholdBudget.Data.Domain;

namespace HouseholdBudget.Data.Interfaces
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
