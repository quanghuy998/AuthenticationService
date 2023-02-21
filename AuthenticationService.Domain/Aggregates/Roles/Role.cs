using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.Roles
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
            this.Name= name;
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
    }
}
