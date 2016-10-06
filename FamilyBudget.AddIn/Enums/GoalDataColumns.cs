using FamilyBudget.Common.Attributes;

namespace FamilyBudget.AddIn.Enums
{
    internal enum GoalDataColumns
    {
        [FriendlyName("Name")]
        NAME = 1,

        [FriendlyName("Total Saved")]
        TOTAL_SAVED = 2,

        [FriendlyName("Goal Amount")]
        GOAL_AMOUNT = 3,

        [FriendlyName("Targeted Completion")]
        TARGET_COMPLETION = 4
    }
}
