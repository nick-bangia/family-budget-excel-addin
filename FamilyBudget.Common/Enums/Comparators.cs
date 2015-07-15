using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Common.Attributes;

namespace FamilyBudget.Common.Enums
{
    public enum Comparators
    {
        [FriendlyName("-- SELECT --")]
        NO_COMPARE = -1,

        [FriendlyName("<")]
        [ApiName("lt")]
        LESS_THAN = 0,

        [FriendlyName("<=")]
        [ApiName("lte")]
        LESS_THAN_EQUAL = 1,

        [FriendlyName("=")]
        [ApiName("eq")]
        EQUAL = 2,

        [FriendlyName(">=")]
        [ApiName("gte")]
        GREATER_THAN_EQUAL = 3,

        [FriendlyName(">")]
        [ApiName("gt")]
        GREATER_THAN = 4,

        [FriendlyName("!=")]
        [ApiName("ne")]
        NOT_EQUAL = 5,

        [FriendlyName("between")]
        [ApiName("btw")]
        BETWEEN = 6
    }
}
