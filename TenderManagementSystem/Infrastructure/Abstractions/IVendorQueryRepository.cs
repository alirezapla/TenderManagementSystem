using TenderManagementSystem.Domain.Models.Entities;

namespace TenderManagementSystem.Infrastructure.Abstractions

{
    public interface IVendorQueryRepository
    {
        Task<Vendor> GetByIdAsync(string id);
        Task<Vendor> GetByUserIdAsync(string userId);
        Task<IEnumerable<Vendor>> GetAllAsync();
        Task<Vendor> GetWithDetailsAsync(string id);
    }
}