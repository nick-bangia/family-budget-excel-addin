using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum SearchFields
    {
        [FriendlyName("Date")]
        [Comparable]
        DATE = 0,

        [FriendlyName("Year")]
        YEAR = 1,

        [FriendlyName("Quarter")]
        QUARTER = 2,

        [FriendlyName("Month")]
        MONTH = 3,

        [FriendlyName("Day")]
        DAY = 4,

        [FriendlyName("Day of Week")]
        DAY_OF_WEEK = 5,

        [FriendlyName("Description")]
        DESCRIPTION = 6,

        [FriendlyName("Category")]
        CATEGORY = 7,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 8,

        [FriendlyName("Amount")]
        [Comparable]
        AMOUNT = 9,

        [FriendlyName("Type")]
        TYPE = 10,

        [FriendlyName("SubType")]
        SUBTYPE = 11,

        [FriendlyName("Payment Method")]
        PAYMENT_METHOD = 12,

        [FriendlyName("Status")]
        STATUS = 13
    }
}
