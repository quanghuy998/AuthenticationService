using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Application.Specifications
{
    public class UserRoleSpecification : BaseSpecification<UserRole>
    {
        public UserRoleSpecification()
        {
            Includes.Add(x => x.User);
            Includes.Add(x => x.Role);
        }
    }
}
