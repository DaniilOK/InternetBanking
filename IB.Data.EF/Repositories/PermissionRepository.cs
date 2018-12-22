using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using IB.Common;
using IB.Data.EF.Helpers;
using IB.Repository.Interface.Exceptions;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Data.EF.Repositories
{
    internal sealed class PermissionRepository : Repository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(InternetBankingContext context) : base(context)
        {
        }

        public override IEnumerable<Permission> Filter(Expression<Func<Permission, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, nameof(predicate));

            try
            {
                return DbSet.Where(predicate).ToList();
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

        public override void Add(Permission entity)
        {
            throw new NotSupportedException("Cannot add permission entity");
        }

        public override void Remove(Permission entity)
        {
            throw new NotSupportedException("Cannot remove permission entity");
        }
    }
}
