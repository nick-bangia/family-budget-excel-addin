using System;

namespace FamilyBudget.Common.Domain
{
    public class Subcategory : ManagedDataObject
    {
        protected string _key;
        protected string _categoryName;
        protected string _categoryKey;
        protected string _accountKey;
        protected string _accountName;
        protected string _name;
        protected string _prefix;
        protected bool _isAllocatable;
        protected bool _isActive;

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

        public string CategoryKey
        {
            get
            {
                return this._categoryKey;
            }

            set
            {
                this._categoryKey = value;
                NotifyPropertyChanged("CategoryKey");
            }
        }

        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }

            set
            {
                this._categoryName = value;
            }
        }

        public string AccountKey
        {
            get
            {
                return this._accountKey;
            }

            set
            {
                this._accountKey = value;
                NotifyPropertyChanged("AccountKey");
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
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Prefix
        {
            get
            {
                return this._prefix;
            }

            set
            {
                this._prefix = value;
                NotifyPropertyChanged("Prefix");
            }
        }

        public bool IsAllocatable
        {
            get
            {
                return this._isAllocatable;
            }

            set
            {
                this._isAllocatable = value;
                NotifyPropertyChanged("IsAllocatable");
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
