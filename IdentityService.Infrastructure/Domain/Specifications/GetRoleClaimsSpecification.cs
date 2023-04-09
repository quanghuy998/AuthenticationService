using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.SeedWork;
using System.Linq.Expressions;

namespace IdentityService.Infrastructure.Domain.Specifications
{
    public class GetRoleClaimsSpecification : BaseSpecification<Role>
    {
        public GetRoleClaimsSpecification(Expression<Func<Role, bool>> expression) : base(expression)
        {
            Includes.Add(x => x.RoleClaims);
        }
    }
}
