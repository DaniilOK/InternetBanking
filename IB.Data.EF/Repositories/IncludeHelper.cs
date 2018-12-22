using System.Linq;
using IB.Repository.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace IB.Data.EF.Repositories
{
    internal static class IncludeHelper
    {
        public static IQueryable<UserPermission> MakeInclusions(this IQueryable<UserPermission> query)
        {
            return query.Include(x => x.User)
                .Include(x => x.Permission);
        }

        public static IQueryable<BankCard> MakeInclusions(this IQueryable<BankCard> query)
        {
            return query.Include(x => x.BankAccount);
        }
    }
}
