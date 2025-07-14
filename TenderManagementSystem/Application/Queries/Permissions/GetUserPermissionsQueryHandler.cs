using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TenderManagementSystem.Core.Exceptions;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Infrastructure.Data;

namespace TenderManagementSystem.Application.Queries.Permissions;

public class GetUserPermissionsQuery : IRequest<GetUserPermissionsResponse>
{
    public required string Id { get; set; }
}

public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, GetUserPermissionsResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public GetUserPermissionsQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<GetUserPermissionsResponse> Handle(GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
            throw new NotFoundException($"User with id '{request.Id}' not found.");

        var userRoleNames = await _userManager.GetRolesAsync(user);
        if (userRoleNames == null || !userRoleNames.Any())
            return new GetUserPermissionsResponse(new HashSet<string>());

        var permissions = new HashSet<string>();

        foreach (var roleName in userRoleNames)
        {
            var role = await _roleManager.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);

            if (role?.Permissions != null)
            {
                foreach (var permission in role.Permissions)
                    permissions.Add(permission.Name);
            }
        }

        return new GetUserPermissionsResponse(permissions);
    }
}

public record GetUserPermissionsResponse(HashSet<string> Permissions);