using System;
using System.ComponentModel;

namespace FamilyBudget.Data.Domain
{
    public class Account : ManagedDataObject
    {
        private string _accountKey;
        private string _accountName;
        private bool _isActive;

        public string AccountKey
        {
            get
            {
                return this._accountKey;
            }
            set
            {
                this._accountKey = value;
            }
        }

        public string AccountName
        {
            get
            {
                return this._accountName;
            }

            set
            {
                this._accountName = value;
                NotifyPropertyChanged("AccountName");
            }
        }

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }

            set
            {
                this._isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }
    }
}
