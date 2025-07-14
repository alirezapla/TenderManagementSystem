using System.ComponentModel.DataAnnotations;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Domain.Models.Entities;

public abstract class BaseEntity : IEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();  
    }
    [Key]
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public string DeletedBy { get; set; }
}