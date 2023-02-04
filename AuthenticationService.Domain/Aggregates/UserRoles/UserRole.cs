using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Aggregates.UserRoles
{
    public class UserRole : Aggregate
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
