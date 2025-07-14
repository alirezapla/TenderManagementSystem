using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using TenderManagementSystem.Application.Queries.Permissions;
using TenderManagementSystem.Security.Models;

namespace TenderManagementSystem.Security;

public class CustomClaimsTransformation(IServiceProvider serviceProvider) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(c => c.Type == CustomClaims.Permission))
        {
            return principal;
        }

        using IServiceScope scope = serviceProvider.CreateScope();

        var sender = scope.ServiceProvider.GetRequiredService<ISender>();
        var id = principal.FindFirstValue(ClaimTypes.Sid)!;

        var result = await sender.Send(
            new GetUserPermissionsQuery() {Id = id});

        if (principal.Identity is not ClaimsIdentity identity)
        {
            return principal;
        }

        foreach (var permission in result.Permissions)
        {
            identity.AddClaim(
                new Claim(CustomClaims.Permission, permission));
        }

        return principal;
    }
}