using System;

namespace FamilyBudget.Common.Domain
{
    public class Subcategory : ManagedDataObject
    {
        protected string _subcategoryKey;
        protected string _categoryName;
        protected string _categoryKey;
        protected string _accountKey;
        protected string _accountName;
        protected string _subcategoryName;
        protected string _subcategoryPrefix;
        protected bool _isActive;

        public string SubcategoryKey
        {
            get
            {
                return this._subcategoryKey;
            }

            set
            {
                this._subcategoryKey = value;
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

        public string SubcategoryName
        {
            get
            {
                return this._subcategoryName;
            }

            set
            {
                this._subcategoryName = value;
                NotifyPropertyChanged("SubcategoryName");
            }
        }

        public string SubcategoryPrefix
        {
            get
            {
                return this._subcategoryPrefix;
            }

            set
            {
                this._subcategoryPrefix = value;
                NotifyPropertyChanged("SubcategoryPrefix");
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
