using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Domain.SeedWork;
using System.Linq.Expressions;

namespace AuthenticationService.Application.Specifications
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
