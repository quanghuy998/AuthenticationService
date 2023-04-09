using IdentityService.Domain.SeedWork;

namespace IdentityService.Domain.Aggregates.Users
{
    public class UserClaim : Entity
    {
        public string ClaimValue { get; }

        public UserClaim(string claimValue)
        {
            ClaimValue = claimValue;
        }
    }
}
