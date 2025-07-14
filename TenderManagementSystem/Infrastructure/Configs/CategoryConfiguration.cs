using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Configs;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(c => c.Id);
        entity.Property(t => t.Id).HasMaxLength(36);
        entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

        entity.ToTable("Categories");
        entity.HasData(
            new Category
            {
                Id = "1c2b5d67-f332-4b4b-a806-8b6e329fced7",
                Name = "Construction",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Category
            {
                Id = "338b72c6-4141-4c4f-ba31-4dbfdd6efe69",
                Name = "IT Services",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Category
            {
                Id = "6d7e48e5-78b3-458d-be56-f5fbf38fdc68",
                Name = "Healthcare",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Category
            {
                Id = "b4044062-529f-4fce-91e5-3954f2322310",
                Name = "Education",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            },
            new Category
            {
                Id = "b602b39d-edb0-48e1-a998-f7d0c75f3641",
                Name = "Transportation",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedDate = null,
                UpdatedBy = "",
                IsDeleted = false,
                DeletedBy = "",
            }
        );
    }
}