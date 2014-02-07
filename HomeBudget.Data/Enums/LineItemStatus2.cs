using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum LineItemStatus2
    {
        [FriendlyName("Reconciled")]
        Reconciled = 0,

        [FriendlyName("Pending")]
        Pending = 1,

        [FriendlyName("Future")]
        Future = 2
    }
}
