using IdentityService.Infrastructure.CQRS.Commands;
using IdentityService.Infrastructure.CQRS.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityService.Infrastructure.CQRS
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
