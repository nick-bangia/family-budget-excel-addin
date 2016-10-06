using FamilyBudget.Common.Attributes;
using System.ComponentModel;

namespace FamilyBudget.Common.Enums
{
    public enum LineItemType
    {
        [FriendlyName("Expense")]
        [Description("Describes a budget bound expense or refund of a bucket")]
        EXPENSE = 0,

        [FriendlyName("Allocation")]
        [Description("Describes a budget bound allocation or deallocation of a bucket")]
        ALLOCATION = 1,

        [FriendlyName("Adjustment")]
        [Description("Describes a line item that is not budget bound to adjust a particular bucket's value")]
        ADJUSTMENT = 2,

        [FriendlyName("Income")]
        [Description("Describes a budget bound credit due to income")]
        INCOME = 3,

        [FriendlyName("Journal Entry")]
        [Description("Describes a special adjustment that usually comes in pairs and nets to 0 for moving money between buckets")]
        JOURNAL = 4
    }
}
