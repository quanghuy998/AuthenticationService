using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Domain.Repositories
{
    internal class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
