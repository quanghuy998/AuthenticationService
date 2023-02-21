using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Domain.SeedWork;
using System.Linq.Expressions;

namespace AuthenticationService.Application.Specifications
{
    public class GetUserClaimsSpecification : BaseSpecification<User>
    {
        public GetUserClaimsSpecification(Expression<Func<User, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.UserClaims);
            Includes.Add(x => x.UserRoles);
        }
    }
}
