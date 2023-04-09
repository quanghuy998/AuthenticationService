using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;

namespace IdentityService.Domain.Aggregates.UserRoles
{
    public class UserRole : Aggregate
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }

        public void AddRole(Role role)
        {
            Role = role;
        }

        public void AddUser(User user)
        {
            User = user;
        }
    }
}
