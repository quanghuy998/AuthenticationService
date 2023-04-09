using IdentityService.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Domain
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool isActiveTransaction;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            if (isActiveTransaction)
            {
                await action();
                return;
            }

            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                isActiveTransaction = true;
                try
                {
                    await action();
                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    isActiveTransaction = false;
                }
            });
        }

        public Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> action, CancellationToken cancellationToken = default)
        {
            if (isActiveTransaction) return action();

            var strategy = context.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                isActiveTransaction = true;
                try
                {
                    var result = await action();
                    await context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    isActiveTransaction = false;
                }
            });
        }
    }
}
