using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetBanking.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, Permission permission)
        {
            var claim = user.FindFirst("permissions");
            return claim != null && claim.Value.Contains($":{(int)permission}:");
        }

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("userId");
            if (claim == null)
            {
                return Guid.Empty;
            }

            return Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("userFullName");
            return claim?.Value;
        }
    }
}
