using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum SubCategoryDataColumns
    {
        [FriendlyName("Prefix")]
        PREFIX = 1,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 2,

        [FriendlyName("Account Name")]
        ACCOUNT_NAME = 3,

        [FriendlyName("Is Active")]
        IS_ACTIVE = 4
    }
}
