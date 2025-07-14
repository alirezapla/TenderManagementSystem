using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Configs;

public class VendorConfiguration: IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> entity)
    {
        entity.HasKey(v => v.Id);
        entity.Property(v => v.Id).HasMaxLength(36);
        entity.Property(v => v.Name).IsRequired().HasMaxLength(200);
        entity.Property(v => v.UserId).IsRequired().HasMaxLength(36);

        entity.HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ToTable("Vendors");
    }
}