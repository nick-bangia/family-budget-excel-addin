using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum DataColumns
    {
        [FriendlyName("Year")]
        YEAR = 1,

        [FriendlyName("Month")]
        MONTH = 2,

        [FriendlyName("Quarter")]
        QUARTER = 3,

        [FriendlyName("Day Of Month")]
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
        AMOUNT = 9,

        [FriendlyName("Line Item Type")]
        TYPE = 10,

        [FriendlyName("Financial Type")]
        SUBTYPE = 11,

        [FriendlyName("Payment Method")]
        PAYMENT_METHOD = 12,

        [FriendlyName("Status")]
        STATUS = 13
    }
}
