using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Specifications
{
    public class GetUserRoleSpecification : BaseSpecification<UserRole>
    {
        public GetUserRoleSpecification(Expression<Func<UserRole, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.User);
            Includes.Add(x => x.Role);
        }
    }
}
