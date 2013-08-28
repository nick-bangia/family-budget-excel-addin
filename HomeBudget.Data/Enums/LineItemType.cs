using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum LineItemType
    {
        [FriendlyName("Expense")]
        DEBIT = 0,

        [FriendlyName("Allocation")]
        ALLOCATION = 1,

        [FriendlyName("Credit")]
        CREDIT = 2
    }
}
