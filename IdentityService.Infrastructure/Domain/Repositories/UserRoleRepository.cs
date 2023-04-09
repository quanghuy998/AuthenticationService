using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.Database;
using IdentityService.Infrastructure.Domain.Specifications;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Repositories
{
    internal class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AuthDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<UserRole>> FindAllAsync(Expression<Func<UserRole, bool>> expression, CancellationToken cancellationToken)
        {
            var specification = new GetUserRoleSpecification(expression);
            return base.FindAllAsync(specification, cancellationToken);
        }
    }
}
