using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Attributes;
using HouseholdBudget.DataModel;
using System.ComponentModel;

namespace HouseholdBudget.Data.Domain
{
    public class SearchCriteria
    {
        public Guid? UniqueId { get; set; }

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
        
        public Guid? Category { get; set; }
        
        public Guid? SubCategory { get; set; }

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
        
        public Guid? PaymentMethod { get; set; }
        
        public short? Status { get; set; }

        /// <summary>
        /// Builds on a query using terms from the object
        /// </summary>
        /// <param name="lineItemQuery"></param>
        /// <returns>IQueryable object for factLineItems</returns>
        public IQueryable<factLineItems> BuildQueryFromCriteria(IQueryable<factLineItems> lineItemQuery)
        {
            #region unique id
            // unique id
            if (this.UniqueId.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.UniqueKey == this.UniqueId.Value);
            }
            #endregion

            #region full date compare
            // full date compare
            if (this.CompareToMinDate.HasValue)
            {
                switch (this.DateCompareOperator)
                {
                    case Comparators.EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId == this.CompareToMinDate.Value.Year && fli.MonthId == this.CompareToMinDate.Value.Month && fli.DayOfMonthId == this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.GREATER_THAN:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId > this.CompareToMinDate.Value.Year && fli.MonthId > this.CompareToMinDate.Value.Month && fli.DayOfMonthId > this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.GREATER_THAN_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId >= this.CompareToMinDate.Value.Year && fli.MonthId >= this.CompareToMinDate.Value.Month && fli.DayOfMonthId >= this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.LESS_THAN:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId < this.CompareToMinDate.Value.Year && fli.MonthId < this.CompareToMinDate.Value.Month && fli.DayOfMonthId < this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.LESS_THAN_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId <= this.CompareToMinDate.Value.Year && fli.MonthId <= this.CompareToMinDate.Value.Month && fli.DayOfMonthId > this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.NOT_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => (fli.YearId != this.CompareToMinDate.Value.Year && fli.MonthId != this.CompareToMinDate.Value.Month && fli.DayOfMonthId != this.CompareToMinDate.Value.Day));
                        break;
                    case Comparators.BETWEEN:
                        if (this.CompareToMaxDate.HasValue)
                        {
                            lineItemQuery = lineItemQuery.Where(fli =>
                                (fli.YearId >= this.CompareToMinDate.Value.Year && fli.MonthId >= this.CompareToMinDate.Value.Month && fli.DayOfMonthId >= this.CompareToMinDate.Value.Day) &&
                                (fli.YearId <= this.CompareToMaxDate.Value.Year && fli.MonthId <= this.CompareToMaxDate.Value.Month && fli.DayOfMonthId <= this.CompareToMaxDate.Value.Day));
                        }
                        break;
                    default:
                        // in the case that the comparator is set to the default NO_COMPARE, just break;
                        break;
                }
            }
            #endregion

            #region year
            // year
            if (this.Year.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.YearId == this.Year.Value);
            }
            #endregion

            #region month
            // month
            if (this.Month.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.MonthId == this.Month.Value);
            }
            #endregion

            #region day of month
            // day of month
            if (this.Day.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.DayOfMonthId == this.Day.Value);
            }
            #endregion

            #region day of week
            // day of week
            if (this.DayOfWeek.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.DayOfWeekId == this.DayOfWeek.Value);
            }
            #endregion

            #region description
            // description
            if (!String.IsNullOrEmpty(this.DescriptionContains))
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.Description.Contains(this.DescriptionContains));
            }
            #endregion

            #region category
            // category
            if (this.Category.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.Category.CategoryKey == this.Category.Value);
            }
            #endregion

            #region subcategory
            // subcategory
            if (this.SubCategory.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.CategoryKey == this.SubCategory.Value);
            }
            #endregion

            #region amount compare
            // amount
            if (this.CompareToMinAmount.HasValue)
            {
                switch (this.AmountCompareOperator)
                {
                    case Comparators.EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount == this.CompareToMinAmount.Value);
                        break;
                    case Comparators.GREATER_THAN:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount > this.CompareToMinAmount.Value);
                        break;
                    case Comparators.GREATER_THAN_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount >= this.CompareToMinAmount.Value);
                        break;
                    case Comparators.LESS_THAN:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount < this.CompareToMinAmount.Value);
                        break;
                    case Comparators.LESS_THAN_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount <= this.CompareToMinAmount.Value);
                        break;
                    case Comparators.NOT_EQUAL:
                        lineItemQuery = lineItemQuery.Where(fli => fli.Amount != this.CompareToMinAmount.Value);
                        break;
                    case Comparators.BETWEEN:
                        if (this.CompareToMaxAmount.HasValue)
                        {
                            lineItemQuery = lineItemQuery.Where(fli => fli.Amount >= this.CompareToMinAmount.Value && fli.Amount <= this.CompareToMaxAmount);
                        }
                        break;
                    default:
                        // in the case that the comparator is set to the default NO_COMPARE, just break;
                        break;
                }
            }
            #endregion

            #region type
            // type
            if (this.Type.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.TypeId == this.Type.Value);
            }
            #endregion

            #region subtype
            // subtype
            if (this.SubType.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.SubTypeId == this.SubType.Value);
            }
            #endregion

            #region quarter
            // quarter
            if (this.Quarter.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.QuarterId == this.Quarter.Value);
            }
            #endregion

            #region payment method
            // payment method
            if (this.PaymentMethod.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.PaymentMethodId == this.PaymentMethod.Value);
            }
            #endregion

            #region status
            // status
            if (this.Status.HasValue)
            {
                lineItemQuery = lineItemQuery.Where(fli => fli.StatusId == this.Status.Value);
            }
            #endregion

            // return the modified IQueryable
            return lineItemQuery;
        }

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
                    this.Category = (Guid?)null;
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
                    this.PaymentMethod = (Guid?)null;
                    break;
                case SearchFields.QUARTER:
                    this.Quarter = (short?)null;
                    break;
                case SearchFields.STATUS:
                    this.Status = (short?)null;
                    break;
                case SearchFields.SUBCATEGORY:
                    this.SubCategory = (Guid?)null;
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
