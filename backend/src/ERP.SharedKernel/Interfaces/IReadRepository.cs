using ERP.SharedKernel.Base;
using System.Linq.Expressions;

namespace ERP.SharedKernel.Interfaces;

public interface IReadRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
