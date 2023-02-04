using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.Roles
{
    public class RoleClaim : Entity
    {
        public string ClaimValue { get; }

        public RoleClaim(string claimValue)
        {
            ClaimValue = claimValue;
        }
    }
}
