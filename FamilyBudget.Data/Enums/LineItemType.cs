using FamilyBudget.Data.Attributes;
using System.ComponentModel;

namespace FamilyBudget.Data.Enums
{
    public enum LineItemType
    {
        [FriendlyName("Expense")]
        [Description("Describes a budget bound expense or refund of a bucket")]
        EXPENSE = 0,

        [FriendlyName("Allocation")]
        [Description("Describes a budget bound allocation or deallocation of a bucket")]
        ALLOCATION = 1,

        [FriendlyName("Bucket Adjustment")]
        [Description("Describes a line item that is not budget bound to adjust a particular bucket's value")]
        ADJUSTMENT = 2,
        
        [FriendlyName("Savings Goal")]
        [Description("Describes a savings goal")]
        GOAL = 3,

        [FriendlyName("Mixed")]
        [Description("Describes a mixed, rolled up value, usually condensed for brevity")]
        MIXED = 4
    }
}
