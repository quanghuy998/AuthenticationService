using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.Roles
{
    public class Role : Aggregate
    {
        public string Name { get; private set; }
        public List<RoleClaim> RoleClaims { get; }
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
    }
}
