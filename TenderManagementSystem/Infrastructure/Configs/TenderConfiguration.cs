using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Configs;

public class TenderConfiguration: IEntityTypeConfiguration<Tender>
{
    public void Configure(EntityTypeBuilder<Tender> entity)
    {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.Id).HasMaxLength(36);
        entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
        entity.Property(t => t.Description).IsRequired();
        entity.Property(t => t.Deadline).IsRequired();
        entity.Property(t => t.CategoryId).IsRequired().HasMaxLength(36);
        entity.Property(t => t.StatusId).IsRequired().HasMaxLength(36);

        entity.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(t => t.Status)
            .WithMany()
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ToTable("Tenders");
    }
}