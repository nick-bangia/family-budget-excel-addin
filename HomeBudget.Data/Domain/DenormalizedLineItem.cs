using HouseholdBudget.Data.Enums;
using System;

namespace HouseholdBudget.Data.Domain
{
    public class DenormalizedLineItem : ICloneable
    {
        public Guid UniqueKey { get; set; }
        public int Year { get; set; }
        public short MonthInt { get; set; }
        public string Month { get; set; }
        public short Day { get; set; }
        public short DayOfWeekId { get; set; }
        public string DayOfWeek { get; set; }
        public string Description { get; set; }
        public Guid CategoryKey { get; set; }
        public string Category { get; set; }
        public Guid SubCategoryKey { get; set; }
        public string SubCategory { get; set; }
        public decimal Amount { get; set; }
        public LineItemType Type { get; set; }
        public LineItemSubType SubType { get; set; }
        public Quarters Quarter { get; set; }
        public Guid PaymentMethodKey { get; set; }
        public string PaymentMethod { get; set; }
        public LineItemStatus2 Status { get; set; }

        public object Clone()
        {
            return new DenormalizedLineItem()
            {
                UniqueKey = this.UniqueKey,
                Year = this.Year,
                MonthInt = this.MonthInt,
                Month = this.Month,
                Day = this.Day,
                DayOfWeekId = this.DayOfWeekId,
                DayOfWeek = this.DayOfWeek,
                Description = this.Description,
                CategoryKey = this.CategoryKey,
                Category = this.Category,
                SubCategoryKey = this.SubCategoryKey,
                SubCategory = this.SubCategory,
                Amount = this.Amount,
                Type = this.Type,
                SubType = this.SubType,
                Quarter = this.Quarter,
                PaymentMethodKey = this.PaymentMethodKey,
                PaymentMethod = this.PaymentMethod,
                Status = this.Status
            };
        }
    }
}
