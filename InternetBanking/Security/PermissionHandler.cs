using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Security
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var claim = context.User.FindFirst("permissions");
            if (claim != null && claim.Value.Contains(requirement.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
