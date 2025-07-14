using System.Linq.Expressions;

namespace TenderManagementSystem.Infrastructure.Abstractions;

public interface IRichSpecification<T>
{
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}