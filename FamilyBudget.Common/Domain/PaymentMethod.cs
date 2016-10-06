using System;

namespace FamilyBudget.Common.Domain
{
    public class PaymentMethod : ManagedDataObject
    {
        private string _key;
        private string _paymentMethodName;
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

        public string PaymentMethodName
        {
            get
            {
                return this._paymentMethodName;
            }
            set
            {
                this._paymentMethodName = value;
                NotifyPropertyChanged("PaymentMethodName");
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
