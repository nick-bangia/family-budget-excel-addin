using System;
using System.ComponentModel;

namespace FamilyBudget.Common.Domain
{
    public class Account : ManagedDataObject
    {
        private string _key;
        private string _accountName;
        private bool _isActive;

        public string Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
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
