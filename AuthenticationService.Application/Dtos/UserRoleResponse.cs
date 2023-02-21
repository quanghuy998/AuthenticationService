using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.Users;

namespace AuthenticationService.Application.Dtos
{
    public class UserRoleResponse
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserResponse User { get; set; }
        public RoleResponse Role { get; set; }
    }
}
