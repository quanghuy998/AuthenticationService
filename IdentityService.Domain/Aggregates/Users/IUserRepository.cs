using IdentityService.Domain.SeedWork;
using System.Linq.Expressions;

namespace IdentityService.Domain.Aggregates.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<string>> GetUserClaims(Expression<Func<User, bool>> expression, CancellationToken cancellationToken);
    }
}
