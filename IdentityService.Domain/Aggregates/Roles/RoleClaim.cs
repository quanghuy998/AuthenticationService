using IdentityService.Domain.SeedWork;

namespace IdentityService.Domain.Aggregates.Roles
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
