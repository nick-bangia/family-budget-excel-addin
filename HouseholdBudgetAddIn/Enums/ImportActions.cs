using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum ImportResultActions
    {
        [FriendlyName("Import")]
        IMPORT,

        [FriendlyName("Delete")]
        DELETE,
    }
}
