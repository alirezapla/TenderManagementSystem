using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities.RBAC;

namespace TenderManagementSystem.Infrastructure.Configs.RBAC;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.HasMany<Permission>(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);
        builder.HasData(
            new Role
            {
                Id = "0021877C-3F5F-45E3-BE2C-A339601532B6",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new Role
            {
                Id = "4AA5C7A2-9769-4D6D-B0FB-4C1EE0CD7B22",
                Name = "Vendor",
                NormalizedName = "VENDOR"
            }
        );
    }
}