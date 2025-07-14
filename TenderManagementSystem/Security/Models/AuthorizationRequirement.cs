using Microsoft.AspNetCore.Authorization;

namespace TenderManagementSystem.Security.Models;

public sealed class AuthorizationRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}