using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using IB.Common;
using IB.Data.EF.Helpers;
using IB.Repository.Interface.Exceptions;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;

namespace IB.Data.EF.Repositories
{
    internal sealed class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(InternetBankingContext context) : base(context)
        {
        }

        public override UserPermission Find(Expression<Func<UserPermission, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, nameof(predicate));

            try
            {
                return DbSet.MakeInclusions().SingleOrDefault(predicate);
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

        public IEnumerable<UserPermission> FilterByUserId(Guid userId)
        {
            return Filter(x => x.UserId == userId);
        }

        public override IEnumerable<UserPermission> Filter(Expression<Func<UserPermission, bool>> predicate)
        {
            Expect.ArgumentNotNull(predicate, nameof(predicate));

            try
            {
                return DbSet.MakeInclusions().Where(predicate)
                    .OrderBy(x => x.User.FirstName)
                    .ThenBy(x => x.User.LastName)
                    .ToList();
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
