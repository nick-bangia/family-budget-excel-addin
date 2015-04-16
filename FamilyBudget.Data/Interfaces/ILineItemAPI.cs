using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;

namespace FamilyBudget.Data.Interfaces
{
    public interface ILineItemAPI
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
