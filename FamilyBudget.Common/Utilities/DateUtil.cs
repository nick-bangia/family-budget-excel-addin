using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Utilities
{
    public static class DateUtil
    {
        public static Quarters GetQuarterForMonth(int month)
        {
            Quarters quarter;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    quarter = Quarters.Q1;
                    break;
                case 4:
                case 5:
                case 6:
                    quarter = Quarters.Q2;
                    break;
                case 7:
                case 8:
                case 9:
                    quarter = Quarters.Q3;
                    break;
                case 10:
                case 11:
                case 12:
                    quarter = Quarters.Q4;
                    break;
                default:
                    // default to Q1 - this shouldn't ever happen
                    quarter = Quarters.Q1;
                    break;
            }

            return quarter;
        }
    }
}
