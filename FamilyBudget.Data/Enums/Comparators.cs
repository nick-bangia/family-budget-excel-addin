using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Attributes;

namespace FamilyBudget.Data.Enums
{
    public enum Comparators
    {
        [FriendlyName("-- SELECT --")]
        NO_COMPARE = -1,

        [FriendlyName("<")]
        LESS_THAN = 0,

        [FriendlyName("<=")]
        LESS_THAN_EQUAL = 1,

        [FriendlyName("=")]
        EQUAL = 2,

        [FriendlyName(">")]
        GREATER_THAN_EQUAL = 3,

        [FriendlyName(">=")]
        GREATER_THAN = 4,

        [FriendlyName("!=")]
        NOT_EQUAL = 5,

        [FriendlyName("between")]
        BETWEEN = 6
    }
}
