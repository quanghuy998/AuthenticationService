using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.Users;

namespace IdentityService.Application.Dtos
{
    public class UserRoleResponse
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public UserResponse User { get; set; }
        public RoleResponse Role { get; set; }
    }
}
