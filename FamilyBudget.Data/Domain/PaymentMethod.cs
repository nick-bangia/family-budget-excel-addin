using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FamilyBudget.Data.Domain
{
    public class PaymentMethod : INotifyPropertyChanged
    {
        private Guid _paymentMethodKey;
        private string _paymentMethodName;
        private bool _isActive;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid PaymentMethodKey
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

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
