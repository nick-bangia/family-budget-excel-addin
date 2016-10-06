using System;

namespace FamilyBudget.Common.Domain
{
    public class Goal : Subcategory
    {
        protected decimal _goalAmount;
        protected DateTime _estimatedCompletionDate;

        public decimal GoalAmount
        {
            get
            {
                return this._goalAmount;
            }

            set
            {
                this._goalAmount = value;
                NotifyPropertyChanged("GoalAmount");
            }
        }

        public DateTime EstimatedCompletionDate
        {
            get
            {
                return this._estimatedCompletionDate;
            }

            set
            {
                this._estimatedCompletionDate = value.Date;
                NotifyPropertyChanged("EstimatedCompletionDate");
            }
        }
    }
}
