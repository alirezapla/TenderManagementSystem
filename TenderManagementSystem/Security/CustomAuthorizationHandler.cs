using Microsoft.AspNetCore.Authorization;
using TenderManagementSystem.Security.Extensions;
using TenderManagementSystem.Security.Models;

namespace TenderManagementSystem.Security;

public class CustomAuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthorizationRequirement requirement)
    {
        var permissions = context.User.GetPermissions();
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}