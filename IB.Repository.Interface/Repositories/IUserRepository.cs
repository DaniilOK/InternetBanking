using System;
using IB.Repository.Interface.Models;

namespace IB.Repository.Interface.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User FindByLogin(string login);
    }
}
