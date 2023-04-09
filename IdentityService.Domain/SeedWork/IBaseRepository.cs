using System.Linq.Expressions;

namespace IdentityService.Domain.SeedWork
{
    public interface IBaseRepository<TAggregate> where TAggregate : Aggregate
    {
        Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken);
        Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken);
        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        Task<TAggregate> FindOneAsync(int id, CancellationToken cancellationToken = default);
        Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellationToken = default);
        Task<TAggregate> FindOneAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<TAggregate>> FindAllAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default);
    }
}
