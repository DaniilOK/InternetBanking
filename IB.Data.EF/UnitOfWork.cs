using System;
using System.Linq;
using IB.Data.EF.Repositories;
using IB.Repository.Interface;
using IB.Repository.Interface.Exceptions;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IB.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextOptions<InternetBankingContext> _options;
        private InternetBankingContext _context;

        private IUserRepository _userRepository;
        private IPermissionRepository _permissionRepository;
        private IUserPermissionRepository _userPermissionRepository;
        private IBankAccountRepository _bankAccountRepository;
        private IBankCardRepository _bankCardRepository;

        public InternetBankingContext Context => _context ?? (_context = new InternetBankingContext(_options));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(Context));

        public IPermissionRepository PermissionRepository =>
            _permissionRepository ?? (_permissionRepository = new PermissionRepository(Context));

        public IUserPermissionRepository UserPermissionRepository =>
            _userPermissionRepository ?? (_userPermissionRepository = new UserPermissionRepository(Context));

        public IBankAccountRepository BankAccountRepository =>
            _bankAccountRepository ?? (_bankAccountRepository = new BankAccountRepository(Context));

        public IBankCardRepository BankCardRepository =>
            _bankCardRepository ?? (_bankCardRepository = new BankCardRepository(Context));

        public UnitOfWork(IOptions<UnitOfWorkOptions> optionsAccessor) : this(optionsAccessor.Value)
        {
        }

        public UnitOfWork(UnitOfWorkOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InternetBankingContext>();
            optionsBuilder.UseSqlServer(options.ConnectionString, x => x.CommandTimeout(options.CommandTimeout));
            _options = optionsBuilder.Options;
        }

        public void Commit(object contextInfo = null)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
            }
            catch (DbUpdateException ex)
            {
                throw new UpdateException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Commit error.", ex);
            }
        }

        public bool EntityModified<T>(T entity) where T : Entity
        {
            return Context.Entry(entity).State != EntityState.Unchanged;
        }

        public void Rollback()
        {
        }

        #region IDisposable

        private bool _isDisposed;

        public void Dispose()
        {
            if (_context == null)
            {
                return;
            }

            if (!_isDisposed)
            {
                Context.Dispose();
            }

            _isDisposed = true;
        }

        #endregion
    }
}
