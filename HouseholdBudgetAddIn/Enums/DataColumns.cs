using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum DataColumns
    {
        [FriendlyName("Year")]
        YEAR = 1,

        [FriendlyName("Month")]
        MONTH = 2,

        [FriendlyName("Day Of Month")]
        DAY = 3,

        [FriendlyName("Day of Week")]
        DAY_OF_WEEK = 4,

        [FriendlyName("Description")]
        DESCRIPTION = 5,

        [FriendlyName("Category")]
        CATEGORY = 6,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 7,

        [FriendlyName("Amount")]
        AMOUNT = 8,

        [FriendlyName("Line Item Type")]
        TYPE = 9
    }
}
