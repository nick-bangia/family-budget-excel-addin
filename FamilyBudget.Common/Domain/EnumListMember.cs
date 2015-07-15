using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyBudget.Common.Domain
{
    public class EnumListMember
    {
        public EnumListMember(string displayValue, int actualValue)
        {
            this.DisplayValue = displayValue;
            this.ActualValue = actualValue;
        }

        public string DisplayValue { get; set; }
        public int ActualValue { get; set; }
    }
}
