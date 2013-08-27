using System;
using HouseholdBudget.Data.Enums;

namespace HouseholdBudget.Data.Domain
{
    public class LineItem
    {
        #region LineItemStatus
        // holds a line item's status
        private LineItemStatus _status = LineItemStatus.EMPTY;
        // gets the status
        public LineItemStatus Status
        {
            get
            {
                return this._status;
            }

            set
            {
                this._status = value;
            }
        }
        #endregion

        #region Date
        // holds a line item's date field
        private DateTime _date;
        // Gets the date
        public DateTime Date
        {
            get
            {
                return this._date;
            }
        }

        // sets the date from an equivalent string value
        public void setDate(string date)
        {
            DateTime convertedDate;
            if (DateTime.TryParse(date, out convertedDate))
            {
                this._date = convertedDate;
            }
            else
            {
                this._date = DateTime.MinValue;
            }
        }

        public void setDate(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            this._date = date;
        }

        public void setDate(DateTime date)
        {
            this._date = date;
        }

        #endregion

        #region Description
        // holds a line item's description field
        private String _description = null;
        public String Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        #endregion

        #region Amount
        // holds a line item's amount field
        private Decimal _amount = 0.0M;
        // Gets/Sets the line item's Amount
        public Decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }
        #endregion

        #region Checksum
        public long CheckSum
        {
            get
            {
                // compute the checksum from the date, description & amount
                long checksum = 
                    this._date.Ticks +
                    this.GetDescriptionCheckSum(this._description) +
                    (long)this._amount;

                return checksum;
            }
        }

        private long GetDescriptionCheckSum(string inputString)
        {
            // prepare to get the checksum
            char[] charArray = inputString.ToCharArray();
            long checksum = 0;

            // loop through the character array, and sum up the
            // character value of each character
            foreach (char character in charArray)
            {
                checksum += (long)character;
            }

            return checksum;
        }
        #endregion
    }
}
