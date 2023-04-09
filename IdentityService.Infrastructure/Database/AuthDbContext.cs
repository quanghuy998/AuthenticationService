using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Database
{
    internal class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AuthDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
