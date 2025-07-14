using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities.RBAC;

namespace TenderManagementSystem.Infrastructure.Configs.RBAC;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.HasMany<Role>(r => r.Roles)
            .WithMany()
            .UsingEntity<RolePermission>();
        builder.HasData(new Permission
        {
            Id = 1,
            Name = "Read"
        });
        builder.HasData(new Permission
        {
            Id = 2,
            Name = "Write"
        });
    }
}