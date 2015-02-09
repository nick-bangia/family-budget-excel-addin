using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Data.Enums
{
    public enum LineItemSubType
    {
        [FriendlyName("Debit")]
        DEBIT = 0,

        [FriendlyName("Credit")]
        CREDIT = 1,

        [FriendlyName("Goal")]
        GOAL = 2
    }
}
