﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Protocol;
using HouseholdBudget.Data.Enums;
using HouseholdBudget.Data.Domain;

namespace HouseholdBudget.Data.Interfaces
{
    public interface IPaymentMethodMapper
    {
        LiveDataObject GetPaymentMethods();
        OperationStatus AddNewPaymentMethod(string methodName, bool isActive);
        //OperationStatus SetPaymentMethodActiveState(Guid PaymentMethodKey, bool activeState);
    }
}