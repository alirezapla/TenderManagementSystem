using Microsoft.AspNetCore.Identity;

namespace TenderManagementSystem.Domain.Models.Entities.RBAC;

public class Role : IdentityRole<string>
{
    public ICollection<Permission> Permissions { get; set; } = default!;
    public ICollection<UserRole> UserRoles { get; set; }}
