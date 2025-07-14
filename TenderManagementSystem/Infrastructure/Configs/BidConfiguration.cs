using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Configs;

public class BidConfiguration: IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> entity)
    {
        entity.HasKey(b => b.Id);
        entity.Property(b => b.Id).HasMaxLength(36);
        entity.Property(b => b.TenderId).IsRequired().HasMaxLength(36);
        entity.Property(b => b.VendorId).IsRequired().HasMaxLength(36);
        entity.Property(b => b.StatusId).IsRequired().HasMaxLength(36);
        entity.Property(b => b.Amount).HasColumnType("decimal(18,2)").IsRequired();
        entity.Property(b => b.SubmissionDate).IsRequired();
        entity.Property(b => b.Comments).HasMaxLength(500);
        entity.HasIndex(u => new {u.VendorId, u.TenderId})
            .IsUnique();
        entity.HasOne(b => b.Tender)
            .WithMany(t => t.Bids)
            .HasForeignKey(b => b.TenderId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(b => b.Vendor)
            .WithMany(v => v.Bids)
            .HasForeignKey(b => b.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey(b => b.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ToTable("Bids");
    }
    
}