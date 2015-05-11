using System;

namespace FamilyBudget.Data.Domain
{
    public class PaymentMethod : ManagedDataObject
    {
        private string _paymentMethodKey;
        private string _paymentMethodName;
        private bool _isActive;

        public string PaymentMethodKey
        {
            get
            {
                return this._paymentMethodKey;
            }
            set
            {
                this._paymentMethodKey = value;
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
