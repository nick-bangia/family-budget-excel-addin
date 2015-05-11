using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Domain;
using System.ComponentModel;
using FamilyBudget.Data.Enums;

namespace FamilyBudget.Data.Interfaces
{
    public interface IAccountAPI
    {
        BindingList<Account> GetAccounts(bool forceGet);
        OperationStatus AddNewAccounts(List<Account> accounts);
        OperationStatus UpdateAccounts(List<Account> accounts);
    }
}
