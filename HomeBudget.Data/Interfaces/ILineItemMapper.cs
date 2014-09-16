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
        DenormalizedLineItem AddNewLineItem(DenormalizedLineItem lineItem);
        OperationStatus UpdateLineItem(DenormalizedLineItem lineItem);
        DenormalizedLineItem GetLineItem(DenormalizedLineItem lineItem);
        List<DenormalizedLineItem> GetAllLineItems();
        List<DenormalizedLineItem> GetLineItemsByCriteria(SearchCriteria searchCriteria);
        DenormalizedLineItem GetFirstLineItemByCriteria(SearchCriteria searchCriteria);
        OperationStatus DeleteLineItem(Guid itemKey);
    }
}
