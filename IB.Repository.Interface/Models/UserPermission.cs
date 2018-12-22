using System;

namespace IB.Repository.Interface.Models
{
    public class UserPermission : Entity
    {
        public Guid UserId { get; set; }
        public int PermissionId { get; set; }

        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}
