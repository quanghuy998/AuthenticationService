namespace AuthenticationService.Domain.SeedWork
{
    public interface IBaseRepository<TAggregate> where TAggregate : Aggregate
    {
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<TAggregate> FindOneAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default);
        Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken);
        Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken);
        Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken);
    }
}
