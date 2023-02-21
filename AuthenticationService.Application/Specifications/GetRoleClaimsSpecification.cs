using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.SeedWork;
using System.Linq.Expressions;

namespace AuthenticationService.Application.Specifications
{
    public class GetRoleClaimsSpecification : BaseSpecification<Role>
    {
        public GetRoleClaimsSpecification(Expression<Func<Role, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.RoleClaims);
        }
    }
}
