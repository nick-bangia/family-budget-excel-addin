using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Attributes;

namespace FamilyBudget.Data.Enums
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
