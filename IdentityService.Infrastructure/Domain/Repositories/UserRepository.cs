using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.Database;
using IdentityService.Infrastructure.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AuthDbContext context) : base(context)
        {
        }

        public override Task<User> FindOneAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
        {
            var specification = new GetUserSpecification(expression);
            return base.FindOneAsync(specification, cancellationToken);
        }

        public async Task<List<string>> GetUserClaims(Expression<Func<User, bool>> expression, CancellationToken cancellationToken)
        {
            var user = await DbContext.Set<User>()
                .Include(x => x.UserClaims)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(expression, cancellationToken);

            return user == null ? null : user.GetFullClaims();
        }
    }
}
