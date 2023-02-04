using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Domain.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
