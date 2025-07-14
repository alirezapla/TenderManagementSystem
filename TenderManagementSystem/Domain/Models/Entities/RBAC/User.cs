using Microsoft.AspNetCore.Identity;

namespace TenderManagementSystem.Domain.Models.Entities.RBAC;

public class User : IdentityUser<string>
{
    public ICollection<UserRole> UserRoles { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
}