using IdentityService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Repositories
{
    internal class BaseRepository<TAggregate> : IBaseRepository<TAggregate>
        where TAggregate : Aggregate
    {
        protected DbContext DbContext { get; }
        
        public BaseRepository(DbContext context)
        {
            DbContext = context ?? throw new ArgumentException(nameof(context));
        }

        public virtual Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public virtual Task<TAggregate> FindOneAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken)!;
        }

        public virtual Task CreateAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().AddAsync(aggregate, cancellationToken);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().Update(aggregate);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken)
        {
            DbContext.Set<TAggregate>().Remove(aggregate);
            return Task.CompletedTask;
        }

        public virtual Task<bool> ExistsAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return DbContext.Set<TAggregate>().AnyAsync(specification.Expression, cancellationToken);
        }

        public virtual Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellationToken)
        {
            return FindOneAsync(new BaseSpecification<TAggregate>(expression), cancellationToken);
        }

        public virtual Task<TAggregate> FindOneAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null) return Task.FromResult(default(TAggregate));
            var queryable = DbContext.Set<TAggregate>().AsQueryable();

            var queryableResultWithIncludes = specification
                .Includes.Aggregate(queryable, (current, include) => current.Include(include));

            var secondaryResult = specification.Includes
                .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            return secondaryResult.FirstOrDefaultAsync(specification.Expression, cancellationToken);
        }

        public virtual Task<IEnumerable<TAggregate>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            var queryable = DbContext.Set<TAggregate>().AsQueryable();
            return Task.FromResult(queryable.AsNoTracking().AsEnumerable());
        }

        public virtual Task<IEnumerable<TAggregate>> FindAllAsync(Expression<Func<TAggregate, bool>> expression, CancellationToken cancellationToken)
        {
            return FindAllAsync(new BaseSpecification<TAggregate>(expression), cancellationToken);
        }

        public virtual Task<IEnumerable<TAggregate>> FindAllAsync(IBaseSpecification<TAggregate> specification, CancellationToken cancellationToken = default)
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
