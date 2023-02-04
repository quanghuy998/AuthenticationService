using AuthenticationService.Infrastructure.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthenticationService.Application
{
    public static class ApplicationScExtention
    {
        public static IServiceCollection AddApplicationServiceRegistration(this IServiceCollection services)
        {
            services.AddCqrsRegistration(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
