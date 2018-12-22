using System;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(Permission permission) : base(permission.ToString())
        {
        }
    }
}
