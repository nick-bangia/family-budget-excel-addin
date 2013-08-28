using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    internal enum ImportResultsColumns
    {
        [FriendlyName("Result")]
        RESULT = 1,

        [FriendlyName("Date")]
        DATE = 2,

        [FriendlyName("Description")]
        DESCRIPTION = 3,

        [FriendlyName("Amount")]
        AMOUNT = 4
    }
}
