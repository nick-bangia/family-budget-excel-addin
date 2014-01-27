using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum LineItemType
    {
        [FriendlyName("Expense")]
        EXPENSE = 0,

        [FriendlyName("Allocation")]
        ALLOCATION = 1
    }
}
