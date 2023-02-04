using AuthenticationService.Infrastructure.CQRS.Commands;
using AuthenticationService.Infrastructure.CQRS.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthenticationService.Infrastructure.CQRS
{
    public static class CqrsSCExtention
    {
        public static IServiceCollection AddCqrsRegistration(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<ICommandBus, CommandBus>();

            return services;
        }
    }
}
