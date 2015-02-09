using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum LineItemStatus
    {
        [FriendlyName("Reconciled")]
        RECONCILED = 0,

        [FriendlyName("Pending")]
        PENDING = 1,

        [FriendlyName("Future")]
        FUTURE = 2,

        [FriendlyName("Goal")]
        GOAL = 3
    }
}
