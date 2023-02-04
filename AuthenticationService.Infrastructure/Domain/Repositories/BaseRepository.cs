using AuthenticationService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Domain.Repositories
{
    internal class BaseRepository<TAggregate> : IBaseRepository<TAggregate>
        where TAggregate : Aggregate
    {
        protected DbContext DbContext { get; }
        
        public BaseRepository(DbContext context)
        {
            DbContext = context ?? throw new ArgumentException(nameof(context));
        }

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<TAggregate> FindOneAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)!;
        }

        public Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            var queryable = DbContext.Set<TAggregate>().AsQueryable();
            return Task.FromResult(queryable.AsNoTracking().AsEnumerable());
        }

        public Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().AddAsync(aggregate, cancellationToken);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().Remove(aggregate);
            return Task.CompletedTask;
        }
    }
}
