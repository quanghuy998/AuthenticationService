using AuthenticationService.Infrastructure.Domain;
using AuthenticationService.Infrastructure.CQRS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace AuthenticationService.Infrastructure
{
    public static class InfrastructureScExtention
    {
        public static IServiceCollection AddInfrastructureServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseRegistration(configuration);

            return services;
        }
    }
}
