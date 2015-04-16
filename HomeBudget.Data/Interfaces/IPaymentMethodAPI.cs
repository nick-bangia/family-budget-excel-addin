using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Domain;
using System.ComponentModel;

namespace HouseholdBudget.Data.Interfaces
{
    public interface IPaymentMethodAPI
    {
        BindingList<PaymentMethod> GetPaymentMethods(bool forceGet = false);
        OperationStatus AddNewPaymentMethods(List<PaymentMethod> paymentMethods);
        OperationStatus UpdatePaymentMethods(List<PaymentMethod> paymentMethods);
        PaymentMethod GetPaymentMethodByName(string paymentMethodName);
        PaymentMethod GetDefaultPaymentMethod();
    }
}