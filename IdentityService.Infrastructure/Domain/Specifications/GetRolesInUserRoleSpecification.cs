using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.SeedWork;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Specifications
{
    public class GetRolesInUserRoleSpecification : BaseSpecification<UserRole>
    {
        public GetRolesInUserRoleSpecification(Expression<Func<UserRole, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.Role);
        }
    }
}
