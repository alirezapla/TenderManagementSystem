using Microsoft.AspNetCore.Identity;

namespace TenderManagementSystem.Domain.Models.Entities.RBAC;

public class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public Role Role { get; set; }
}