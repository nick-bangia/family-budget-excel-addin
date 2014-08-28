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
        OperationStatus UpdateLineItem(DenormalizedLineItem lineItem);
        LineItem GetLineItem(LineItem lineItem);
        List<DenormalizedLineItem> GetAllLineItems();
        List<DenormalizedLineItem> GetLineItemsByCriteria(SearchCriteria searchCriteria);
        DenormalizedLineItem GetFirstLineItemByCriteria(SearchCriteria searchCriteria);
        OperationStatus DeleteLineItem(Guid itemKey);
    }
}
