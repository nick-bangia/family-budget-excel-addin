using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Domain;
using HouseholdBudget.Data.Enums;

namespace HouseholdBudget.Data.Interfaces
{
    public interface ILineItemMapper
    {
        LineItem AddNewLineItem(LineItem lineItem);
        LineItem GetLineItem(LineItem lineItem);
        List<DenormalizedLineItem> GetAllLineItems();
        OperationStatus AddNewCategory(string categoryName, string subCategoryName, string subCategoryPrefix);
    }
}
