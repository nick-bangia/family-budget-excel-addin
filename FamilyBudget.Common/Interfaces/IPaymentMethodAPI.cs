using System.Collections.Generic;
using System.ComponentModel;
using FamilyBudget.Common.Domain;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Interfaces
{
    public interface IPaymentMethodAPI
    {
        BindingList<PaymentMethod> GetPaymentMethods(bool forceGet);
        OperationStatus AddNewPaymentMethods(List<PaymentMethod> paymentMethods);
        OperationStatus UpdatePaymentMethods(List<PaymentMethod> paymentMethods);
        PaymentMethod GetPaymentMethodByName(string paymentMethodName);
        PaymentMethod GetDefaultPaymentMethod();
    }
}