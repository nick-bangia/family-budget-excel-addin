using System;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Domain
{
    public class SearchCriteria
    {
        public string UniqueId { get; set; }

        private Comparators _dateComparator = Comparators.NO_COMPARE;
        public Comparators DateCompareOperator
        {
            get
            {
                return this._dateComparator;
            }

            set
            {
                this._dateComparator = value;
            }
        }
        public DateTime? CompareToMinDate { get; set; }
        public DateTime? CompareToMaxDate { get; set; }
        public bool ValidateDate(DateTime minDate, DateTime maxDate, Comparators selectedComparator)
        {
            if (selectedComparator == Comparators.BETWEEN)
            {
                if (minDate <= maxDate)
                {
                    this._dateComparator = Comparators.BETWEEN;
                    this.CompareToMinDate = minDate;
                    this.CompareToMaxDate = maxDate;
                }
                else
                {
                    // min has to be less than or equal to max
                    return false;
                }
            }
            else if (selectedComparator != Comparators.NO_COMPARE)
            {
                this._dateComparator = selectedComparator;
                this.CompareToMinDate = minDate;
            }
            else
            {
                // comparator was not chosen
                return false;
            }

            // if we get here, everything is valid and is set now
            return true;
        }

        public int? Year { get; set; }
        public bool ValidateYear(string sYear)
        {   
            Int32 iYear;
            if (Int32.TryParse(sYear, out iYear) && iYear > 2000 && iYear < 2100)
            {
                this.Year = iYear;
            }
            else
            {
                // the year entered is not a valid integer or 
                // it is out of possible range (2000 - 2100?)
                return false;
            }

            // if we get here, then the year is valid
            return true;
        }

        public short? Quarter { get; set; }
        
        public short? Month { get; set; }
        
        public short? Day { get; set; }
        
        public short? DayOfWeek { get; set; }
        
        public string DescriptionContains { get; set; }
        
        public string Category { get; set; }
        
        public string SubCategory { get; set; }

        private Comparators _amountComparator = Comparators.NO_COMPARE;
        public Comparators AmountCompareOperator
        {
            get
            {
                return this._amountComparator;
            }
            set
            {
                this._amountComparator = value;
            }
        }        
        public decimal? CompareToMinAmount { get; set;}
        public decimal? CompareToMaxAmount { get; set;}
        public bool ValidateAmount(string sMinAmount, string sMaxAmount, Comparators selectedComparator)
        {
            decimal dMinAmount = 0.0M;
            if (Decimal.TryParse(sMinAmount, out dMinAmount))
            {
                if (selectedComparator == Comparators.BETWEEN)
                {
                    decimal dMaxAmount = 0.0M;
                    if (Decimal.TryParse(sMaxAmount, out dMaxAmount))
                    {
                        this._amountComparator = Comparators.BETWEEN;
                        this.CompareToMinAmount = dMinAmount;
                        this.CompareToMaxAmount = dMaxAmount;
                    }
                    else
                    {
                        // max amount not a decimal
                        return false;
                    }
                }
                else if (selectedComparator != Comparators.NO_COMPARE)
                {
                    this._amountComparator = selectedComparator;
                    this.CompareToMinAmount = dMinAmount;
                }
                else
                {
                    // no comparator chosen
                    return false;
                }
            }
            else
            {
                // min amount not a decimal
                return false;
            }

            // if we get here, everything is valid and is set now
            return true;
        }
        
        public short? Type { get; set; }
        
        public short? SubType { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public short? Status { get; set; }

        public bool? IsTaxDeductible { get; set; }

        public void ClearField(SearchFields fieldToClear)
        {
            switch (fieldToClear)
            {
                case SearchFields.AMOUNT:
                    this.AmountCompareOperator = Comparators.NO_COMPARE;
                    this.CompareToMaxAmount = (decimal?)null;
                    this.CompareToMinAmount = (decimal?)null;
                    break;
                case SearchFields.CATEGORY:
                    this.Category = null;
                    break;
                case SearchFields.DATE:
                    this.DateCompareOperator = Comparators.NO_COMPARE;
                    this.CompareToMaxDate = (DateTime?)null;
                    this.CompareToMinDate = (DateTime?)null;
                    break;
                case SearchFields.DAY:
                    this.Day = (short?)null;
                    break;
                case SearchFields.DAY_OF_WEEK:
                    this.DayOfWeek = (short?)null;
                    break;
                case SearchFields.DESCRIPTION:
                    this.DescriptionContains = null;
                    break;
                case SearchFields.MONTH:
                    this.Month = (short?)null;
                    break;
                case SearchFields.PAYMENT_METHOD:
                    this.PaymentMethod = null;
                    break;
                case SearchFields.QUARTER:
                    this.Quarter = (short?)null;
                    break;
                case SearchFields.STATUS:
                    this.Status = (short?)null;
                    break;
                case SearchFields.SUBCATEGORY:
                    this.SubCategory = null;
                    break;
                case SearchFields.SUBTYPE:
                    this.SubType = (short?)null;
                    break;
                case SearchFields.TYPE:
                    this.Type = (short?)null;
                    break;
                case SearchFields.YEAR:
                    this.Year = (int?)null;
                    break;
                default:
                    break;
            }
        }        
    }
}
