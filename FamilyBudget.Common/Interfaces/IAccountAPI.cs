using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Common.Domain;
using System.ComponentModel;
using FamilyBudget.Common.Enums;

namespace FamilyBudget.Common.Interfaces
{
    public interface IAccountAPI
    {
        BindingList<Account> GetAccounts(bool forceGet);
        OperationStatus AddNewAccounts(List<Account> accounts);
        OperationStatus UpdateAccounts(List<Account> accounts);
    }
}
