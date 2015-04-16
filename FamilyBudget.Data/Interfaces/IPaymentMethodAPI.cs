using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Protocol;
using FamilyBudget.Data.Enums;
using FamilyBudget.Data.Domain;
using System.ComponentModel;

namespace FamilyBudget.Data.Interfaces
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