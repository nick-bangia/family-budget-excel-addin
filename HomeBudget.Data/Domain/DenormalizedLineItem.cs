using HouseholdBudget.Data.Enums;

namespace HouseholdBudget.Data.Domain
{
    public class DenormalizedLineItem
    {
        public int Year { get; set; }
        public string Month { get; set; }
        public short Day { get; set; }
        public string DayOfWeek { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public decimal Amount { get; set; }
        public LineItemType Type { get; set; }
        public LineItemSubType SubType { get; set; }
        public Quarters Quarter { get; set; }
    }
}
