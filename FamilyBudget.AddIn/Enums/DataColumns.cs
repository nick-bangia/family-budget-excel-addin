using FamilyBudget.Common.Attributes;

namespace FamilyBudget.AddIn.Enums
{
    internal enum DataColumns
    {
        [FriendlyName("Year")]
        YEAR = 1,

        [FriendlyName("Month")]
        MONTH = 2,

        [FriendlyName("Month-Year")]
        MONTH_YEAR = 3,

        [FriendlyName("Day-Month-Year")]
        DAY_MONTH_YEAR = 4,

        [FriendlyName("Quarter-Year")]
        QUARTER_YEAR = 5,
        
        [FriendlyName("Day of Week")]
        DAY_OF_WEEK = 6,

        [FriendlyName("Description")]
        DESCRIPTION = 7,

        [FriendlyName("Category")]
        CATEGORY = 8,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 9,

        [FriendlyName("Prefix - SubCategory")]
        SUBCATEGORY_WITH_PREFIX = 10,
        
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
        STATUS = 16,

        [FriendlyName("Is Goal?")]
        IS_GOAL = 17
    }
}
