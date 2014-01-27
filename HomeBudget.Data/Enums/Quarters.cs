using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum Quarters
    {
        [FriendlyName("Q1")]
        Q1 = 1,

        [FriendlyName("Q2")]
        Q2 = 2,

        [FriendlyName("Q3")]
        Q3 = 3,

        [FriendlyName("Q4")]
        Q4 = 4
    }
}
