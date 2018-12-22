using System;
using System.Data.SqlClient;
using IB.Data.EF.Helpers;
using IB.Repository.Interface;
using IB.Repository.Interface.Exceptions;
using IB.Repository.Interface.Models;

namespace IB.Data.EF.Repositories
{
    internal abstract class Repository<TEntity, TKey> : Repository<TEntity>, IRepository<TEntity, TKey>
        where TEntity : Entity<TKey> where TKey : struct
    {
        protected Repository(InternetBankingContext context) : base(context)
        {
        }

        public virtual TEntity Find(TKey id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }
    }
}
