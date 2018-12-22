using System;
using System.Collections.Generic;
using System.Linq;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Data.EF.Repositories
{
    internal sealed class BankCardRepository : Repository<BankCard, Guid>, IBankCardRepository
    {
        public BankCardRepository(InternetBankingContext context) : base(context)
        {
        }

        public IReadOnlyCollection<BankCard> GetUserCards(Guid userId)
        {
            return Filter(x => x.UserId == userId).ToList();
        }
    }
}
