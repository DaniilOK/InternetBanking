using System;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IUserPermissionRepository UserPermissionRepository { get; }
        IBankAccountRepository BankAccountRepository { get; }
        IBankCardRepository BankCardRepository { get; }

        void Commit(object contextInfo = null);

        void Rollback();

        bool EntityModified<T>(T entity) where T : Entity;
    }
}
