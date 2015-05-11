using System.Collections.Generic;
using FamilyBudget.Data.Domain;
using FamilyBudget.Data.Enums;

namespace FamilyBudget.Data.Interfaces
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
