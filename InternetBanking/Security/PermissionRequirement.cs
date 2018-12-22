using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission Permission { get; }

        public PermissionRequirement(Permission permission)
        {
            Permission = permission;
        }

        public override string ToString()
        {
            return $":{(int)Permission}:";
        }
    }
}
