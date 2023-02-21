using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;
using System.Linq.Expressions;

namespace AuthenticationService.Application.Specifications
{
    public class GetRolesInUserRoleSpecification : BaseSpecification<UserRole>
    {
        public GetRolesInUserRoleSpecification(Expression<Func<UserRole, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.Role);
        }
    }
}
