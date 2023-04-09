using IdentityService.Infrastructure.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityService.Application
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
