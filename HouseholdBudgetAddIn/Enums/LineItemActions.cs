using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum LineItemActions
    {
        [FriendlyName("Import")]
        IMPORT,

        [FriendlyName("Edit")]
        EDIT,

        [FriendlyName("Remove")]
        DELETE,
    }
}
