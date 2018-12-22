using System;
using System.Collections.Generic;
using System.Linq;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Data.EF.Repositories
{
    internal sealed class BankAccountRepository : Repository<BankAccount, Guid>, IBankAccountRepository
    {
        public BankAccountRepository(InternetBankingContext context) : base(context)
        {
        }

        public IReadOnlyCollection<BankAccount> GetUserBankAccounts(Guid userId)
        {
            return Filter(x => x.UserId == userId).ToList();
        }
    }
}
