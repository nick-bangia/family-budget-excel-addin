using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseholdBudget.Data.Domain
{
    public class PaymentMethod
    {
        public Guid PaymentMethodKey { get; set; }
        public string PaymentMethodName { get; set; }
        public bool IsActive { get; set; }
    }
}
