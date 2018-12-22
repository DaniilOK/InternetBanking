using System;
using System.Collections.Generic;
using IB.Repository.Interface.Models;

namespace IB.Repository.Interface.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount, Guid>
    {
        IReadOnlyCollection<BankAccount> GetUserBankAccounts(Guid userId);
    }
}
