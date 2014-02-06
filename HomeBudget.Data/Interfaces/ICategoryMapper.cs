﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Protocol;
using System.Data.Objects;

namespace HouseholdBudget.Data.Interfaces
{
    public interface ICategoryMapper
    {
        LiveDataObject GetCategories();
        LiveDataObject GetSubCategories();
        LiveDataObject GetFilteredSubCategories(Guid categoryKey);
        OperationStatus AddNewCategory(string categoryName);
        OperationStatus AddNewSubCategory(Guid categoryKey, string subCategoryName, string subCategoryPrefix, bool isActive);
        //OperationStatus SetCategoryActiveState(Guid categoryId, bool activeState);        
    }
}
