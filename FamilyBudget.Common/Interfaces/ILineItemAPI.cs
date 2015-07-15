using System.Collections.Generic;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Interfaces
{
    public interface ILineItemAPI
    {
        List<DenormalizedLineItem> GetAllLineItems(bool forceGet = false);
        List<DenormalizedLineItem> GetLineItemsByCriteria(SearchCriteria searchCriteria);
        DenormalizedLineItem GetFirstLineItemByCriteria(SearchCriteria searchCriteria);
        List<DenormalizedLineItem> AddNewLineItems(List<DenormalizedLineItem> lineItemsToAdd);
        List<DenormalizedLineItem> UpdateLineItems(List<DenormalizedLineItem> lineItemsToUpdate);        
        OperationStatus DeleteLineItem(string itemKey);
    }
}
