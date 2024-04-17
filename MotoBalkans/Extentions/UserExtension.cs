using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace MotoBalkans.Web.Extentions
{
    public static class UserExtension
    {
        public static bool IsInAdminRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return false;
            }

            var roleClaimType = ClaimTypes.Role;
            return principal.HasClaim(roleClaimType, "Admin");
        }

        public static bool IsInUserRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return false;
            }

            var roleClaimType = ClaimTypes.Role;
            return principal.HasClaim(roleClaimType, "User");
        }
    }
}
