using System;
using System.Collections.Generic;
using IB.Repository.Interface.Models;

namespace IB.Repository.Interface.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        IEnumerable<UserPermission> FilterByUserId(Guid userId);
    }
}
