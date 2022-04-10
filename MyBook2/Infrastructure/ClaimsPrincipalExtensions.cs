using System.Security.Claims;

namespace MyBook2.Infrastructure
{
    using static WebConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool UserIsAdmin(this ClaimsPrincipal user) 
            => user.IsInRole(AdminRoleName);
    }
}
