using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Infrastructure.Database;
using AuthenticationService.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.Infrastructure.Domain
{
    internal static class DomainSCExtension
    {
        public static IServiceCollection AddDatabaseRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthenticationService"));
            });

            services.AddScoped<IUnitOfWork>(scope => new UnitOfWork(scope.GetService<AuthDbContext>()));
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            //var provider = services.BuildServiceProvider();
            //using (var scope = provider.CreateScope())
            //{
            //    var scopeService = scope.ServiceProvider;
            //    var authDbContext = scopeService.GetRequiredService<AuthDbContext>();
            //    InitialData.CreateInitalData(authDbContext);
            //}

            return services;
        }
    }
}
