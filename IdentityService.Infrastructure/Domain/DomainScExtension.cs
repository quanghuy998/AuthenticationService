using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.Database;
using IdentityService.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.Domain
{
    internal static class DomainSCExtension
    {
        public static IServiceCollection AddDatabaseRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityService"));
            });

            services.AddScoped<IUnitOfWork>(scope => new UnitOfWork(scope.GetService<AuthDbContext>()));
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            return services;
        }
    }
}
