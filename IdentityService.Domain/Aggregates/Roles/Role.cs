using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;

namespace IdentityService.Domain.Aggregates.Roles
{
    public class Role : Aggregate
    {
        public string Name { get; private set; }
        public List<RoleClaim> RoleClaims { get; private set; }
        public List<UserRole> UserRoles { get; }

        public Role(string name)
        {
            Name = name;
            RoleClaims = new();
        }

        public void Update(string name)
        {
            Name= name;
        }

        public void AddRangeClaims(List<RoleClaim> claims)
        {
            RoleClaims.AddRange(claims);
        }

        public void AddClaim(string claimName)
        {
            var claim = new RoleClaim(claimName);
            RoleClaims.Add(claim);
        }

        public void RemoveClaim(int claimId)
        {
            var claim = RoleClaims.FirstOrDefault(x => x.Id == claimId);
            RoleClaims.Remove(claim);
            if (RoleClaims is null)
                RoleClaims = new();
        }

        public List<string> GetRoleClaims()
        {
            return RoleClaims.Select(x => x.ClaimValue).ToList();
        }
    }
}
