using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Specifications
{
    public class GetUserSpecification : BaseSpecification<User>
    {
        public GetUserSpecification(Expression<Func<User, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.UserClaims);
            Includes.Add(x => x.UserRoles);
        }
    }
}
