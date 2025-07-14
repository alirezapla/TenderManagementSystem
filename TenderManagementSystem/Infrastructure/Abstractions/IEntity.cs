
namespace TenderManagementSystem.Infrastructure.Abstractions;

public interface IEntity
{
    string Id { get; set; }
    bool IsDeleted { get; set; }
}