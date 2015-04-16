using FamilyBudget.Data.Attributes;

namespace FamilyBudget.Data.Enums
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
