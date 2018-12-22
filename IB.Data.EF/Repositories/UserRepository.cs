using System;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Data.EF.Repositories
{
    internal sealed class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(InternetBankingContext context) : base(context)
        {
        }

        public User FindByLogin(string login)
        {
            login = login.ToUpper();
            return Find(x => x.Login.ToUpper() == login);
        }
    }
}
