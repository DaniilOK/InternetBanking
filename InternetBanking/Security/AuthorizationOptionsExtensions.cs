using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Security
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddPermissionPolicy(this AuthorizationOptions options, Permission permission)
        {
            options.AddPolicy(permission.ToString(),
                policy => policy.Requirements.Add(new PermissionRequirement(permission)));
        }

        public static void AddPermissionPolicies(this AuthorizationOptions options)
        {
            foreach (var permission in Enum.GetValues(typeof(Permission)).Cast<Permission>())
            {
                options.AddPermissionPolicy(permission);
            }
        }
    }
}
