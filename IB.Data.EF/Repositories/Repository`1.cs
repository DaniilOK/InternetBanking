using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using IB.Common;
using IB.Data.EF.Helpers;
using IB.Repository.Interface;
using IB.Repository.Interface.Exceptions;
using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace IB.Data.EF.Repositories
{
    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly InternetBankingContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(InternetBankingContext context)
        {
            Expect.ArgumentNotNull(context, nameof(context));

            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, nameof(predicate));

            try
            {
                return DbSet.SingleOrDefault(predicate);
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

        public virtual IEnumerable<TEntity> All()
        {
            return Filter(x => true);
        }

        public virtual IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, nameof(predicate));

            try
            {
                return DbSet.Where(predicate).AsNoTracking().ToList();
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

        public virtual void Add(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Add(entity);
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

        public virtual void Remove(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Remove(entity);
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
