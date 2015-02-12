using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Utilities;
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
        public string AccountName { get; set; }
        public LineItemStatus Status { get; set; }
        public string IsGoal { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsDuplicate { get; set; }
        public int ItemSurrogateKey { get; set; }

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
                Status = this.Status,
                IsDeleted = this.IsDeleted,
                ItemSurrogateKey = this.ItemSurrogateKey
            };
        }
        public long CheckSum
        {
            get
            {
                // compute the checksum from the date, description & amount
                DateTime date = new DateTime(this.Year, this.MonthInt, this.Day);
                long checksum =
                    date.Ticks +
                    this.Description.GetStringChecksum() +
                    this.CategoryKey.ToString().GetStringChecksum() +
                    this.SubCategoryKey.ToString().GetStringChecksum() +
                    (long)this.Amount;

                return checksum;
            }
        }
    }
}
