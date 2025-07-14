using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using TenderManagementSystem.Security.Models;

namespace TenderManagementSystem.Security;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy= await base.GetPolicyAsync(policyName);
        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new AuthorizationRequirement(policyName))
            .Build();
    }
}