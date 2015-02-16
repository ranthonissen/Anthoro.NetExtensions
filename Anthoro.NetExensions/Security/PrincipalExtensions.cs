using System.Linq;
using System.Security.Principal;

namespace Anthoro.NetExensions.Security
{
    public static class PrincipalExtensions
    {
        public static bool IsInAnyRole(this IPrincipal principal, params string[] roles)
        {
            return roles.Any(principal.IsInRole);
        }

        public static bool IsNotInAnyRole(this IPrincipal principal, params string[] roles)
        {
            return roles.All(role => !principal.IsInRole(role));
        }

        public static bool IsNotInRole(this IPrincipal principal, string role)
        {
            return !principal.IsInRole(role);
        }
    }
}