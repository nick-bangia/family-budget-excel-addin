using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyBudget.Data.Domain
{
    public class Goal : Subcategory
    {
        private decimal _goalAmount;
        
        public Goal()
        {
            this._isGoal = true;
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
    }
}
