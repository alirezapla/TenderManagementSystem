using System.Linq.Expressions;

namespace TenderManagementSystem.Infrastructure.Abstractions
{
    public interface IBaseRepository<T>
    {

        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdatePartialAsync(string id, Action<T> updateAction); 
        Task<T> DeleteAsync(string id);
        Task<T> SoftDeleteAsync(T entity);
        Task<int> SoftDeleteAsync(string id);
    }
}