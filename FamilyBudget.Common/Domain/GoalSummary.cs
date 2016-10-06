using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyBudget.Common.Domain
{
    public class GoalSummary
    {
        protected string _name;
        protected decimal _goalAmount;
        protected decimal _totalSaved;
        protected DateTime _targetCompletionDate;

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
            }
        }

        public decimal GoalAmount
        {
            get
            {
                return this._goalAmount;
            }

            set
            {
                this._goalAmount = value;
            }
        }

        public decimal TotalSaved
        {
            get
            {
                return this._totalSaved;
            }

            set
            {
                this._totalSaved = value;
            }
        }

        public DateTime TargetCompletionDate
        {
            get
            {
                return this._targetCompletionDate;
            }

            set
            {
                this._targetCompletionDate = value;
            }
        }
    }
}
