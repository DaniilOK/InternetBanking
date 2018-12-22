using System;
using System.Collections.Generic;
using IB.Repository.Interface.Models;

namespace IB.Repository.Interface.Repositories
{
    public interface IBankCardRepository : IRepository<BankCard, Guid>
    {
        IReadOnlyCollection<BankCard> GetUserCards(Guid userId);
    }
}
