using FamilyBudget.Common.Attributes;

namespace FamilyBudget.AddIn.Enums
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
