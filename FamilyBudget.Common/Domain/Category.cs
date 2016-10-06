using System;
using System.ComponentModel;

namespace FamilyBudget.Common.Domain
{
    public class Category : ManagedDataObject
    {
        private string _key;
        private string _categoryName;
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

        public string CategoryName
        {
            get
            {
                return this._categoryName;
            }

            set
            {
                this._categoryName = value;
                NotifyPropertyChanged("CategoryName");
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
