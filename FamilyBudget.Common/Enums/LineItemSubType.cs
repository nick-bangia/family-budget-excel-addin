using FamilyBudget.Common.Attributes;

namespace FamilyBudget.Common.Enums
{
    public enum LineItemSubType
    {
        [FriendlyName("Debit")]
        DEBIT = 0,

        [FriendlyName("Credit")]
        CREDIT = 1
    }
}
