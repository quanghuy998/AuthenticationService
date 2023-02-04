using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.Users
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
