using Microsoft.AspNetCore.Authorization;

namespace TenderManagementSystem.Security;

public class DebugAuthorizationHandler : IAuthorizationHandler
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var requirement in context.PendingRequirements.ToList())
        {
            Console.WriteLine($"Evaluating requirement: {requirement.GetType().Name}");
            
            // This will succeed all requirements - useful for debugging flow
            context.Succeed(requirement);
        }
    }
}