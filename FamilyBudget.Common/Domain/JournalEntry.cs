using System;

namespace FamilyBudget.Common.Domain
{
    public class JournalEntry : ICloneable
    {
        public decimal Amount { get; set; }
        public Subcategory FromSubcategory { get; set; }
        public Subcategory ToSubcategory { get; set; }
        public DateTime OnDate { get; set; }
        public string Reason { get; set; }

        public string Description
        {
            get
            {
                return String.Format("Move {0:C2} from {1} to {2} on {3}; Reason: {4}",
                            Amount, FromSubcategory.Name, ToSubcategory.Name, OnDate.ToShortDateString(), Reason);
            }
        }

        public object Clone()
        {
            return new JournalEntry()
            {
                Amount = this.Amount,
                FromSubcategory = this.FromSubcategory,
                ToSubcategory = this.ToSubcategory,
                OnDate = this.OnDate,
                Reason = this.Reason
            };
        }
    }
}
