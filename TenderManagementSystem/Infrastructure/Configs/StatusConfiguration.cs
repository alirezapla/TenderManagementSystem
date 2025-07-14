using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Configs;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> entity)
    {
        entity.HasKey(s => s.Id);
        entity.Property(t => t.Id).HasMaxLength(36);
        entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
        entity.HasData(
            new Status
            {
                Id = "12bd2cd6-63e8-43b6-ab7a-fb59cd800b14",
                Name = "open",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Status
            {
                Id = "3823eede-fb7e-42c1-afda-1c1038aab5b4",
                Name = "close ",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Status
            {
                Id = "993396ba-3576-498a-8802-5bcef5ec81b6",
                Name = "pending",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Status
            {
                Id = "adec15d9-5b5c-4e11-85c0-850ec4b7e80f",
                Name = "approved",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Status
            {
                Id = "bb2b4e12-1742-4137-b172-582b93a71e15",
                Name = "rejected",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            }
        );
        entity.ToTable("Statuses");
    }
}