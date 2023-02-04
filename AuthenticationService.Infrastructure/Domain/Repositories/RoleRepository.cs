using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Domain.Repositories
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
