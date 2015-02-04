using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum DataColumns
    {
        [FriendlyName("Year")]
        YEAR = 1,

        [FriendlyName("Month")]
        MONTH = 2,

        [FriendlyName("Month-Year")]
        MONTH_YEAR = 3,

        [FriendlyName("Quarter")]
        QUARTER = 4,

        [FriendlyName("Quarter-Year")]
        QUARTER_YEAR = 5,

        [FriendlyName("Day Of Month")]
        DAY = 6,

        [FriendlyName("Day of Week")]
        DAY_OF_WEEK = 7,

        [FriendlyName("Description")]
        DESCRIPTION = 8,

        [FriendlyName("Category")]
        CATEGORY = 9,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 10,

        [FriendlyName("Amount")]
        AMOUNT = 11,

        [FriendlyName("Line Item Type")]
        TYPE = 12,

        [FriendlyName("Financial Type")]
        SUBTYPE = 13,

        [FriendlyName("Payment Method")]
        PAYMENT_METHOD = 14,

        [FriendlyName("Account")]
        ACCOUNT = 15,

        [FriendlyName("Status")]
        STATUS = 16
    }
}
