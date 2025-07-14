using System.Security.Claims;
using TenderManagementSystem.Security.Models;

namespace TenderManagementSystem.Security.Extensions;

public static class ClaimsExtensions
{
    public static HashSet<string> GetPermissions(this ClaimsPrincipal user)
    {
        IEnumerable<Claim> permissionClaims = user?.FindAll(CustomClaims.Permission) ?? new List<Claim>();

        var permissions = new HashSet<string>();
        foreach (var claim in permissionClaims)
        {
            if (!string.IsNullOrWhiteSpace(claim.Value))
            {
                permissions.Add(claim.Value);
            }
        }

        return permissions;
    }
}