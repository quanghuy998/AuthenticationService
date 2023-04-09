using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Domain.Repositories
{
    internal class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
