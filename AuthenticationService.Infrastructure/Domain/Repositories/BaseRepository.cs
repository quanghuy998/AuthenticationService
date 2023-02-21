using AuthenticationService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public Task<bool> ExistsAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().AnyAsync(specification.Expression, cancellationToken);
        }

        public Task<TAggregate> FindOneAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null) return Task.FromResult(default(TAggregate));
            var queryable = DbContext.Set<TAggregate>().AsQueryable();

            var queryableResultWithIncludes = specification
                .Includes.Aggregate(queryable, (current, include) => current.Include(include));

            var secondaryResult = specification.Includes
                .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return secondaryResult.FirstOrDefaultAsync(specification.Expression, cancellationToken);
        }

        public Task<IEnumerable<TAggregate>> FindAllAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            var queryable = DbContext.Set<TAggregate>().AsQueryable();
            if (specification == null) return Task.FromResult(queryable.AsNoTracking().AsEnumerable());

            var queryableResultWithIncludes = specification.Includes
                .Aggregate(queryable, (current, include) => current.Include(include));

            var secondaryResult = specification.Includes
                .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            var result = secondaryResult
                .Where(specification.Expression)
                .AsNoTracking()
                .AsEnumerable();

            return Task.FromResult(result);
        }
    }
}
