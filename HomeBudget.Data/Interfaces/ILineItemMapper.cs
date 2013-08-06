using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeBudget.Data.Domain;
using HomeBudget.Data.Enums;

namespace HomeBudget.Data.Interfaces
{
    public interface ILineItemMapper
    {
        LineItem AddNewLineItem(LineItem lineItem);
        LineItem GetLineItem(LineItem lineItem);
        List<DenormalizedLineItem> GetAllLineItems();
        OperationStatus AddNewCategory(string name, string prefix);
    }
}
