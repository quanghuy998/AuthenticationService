using MediatR;

namespace IdentityService.Infrastructure.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IRequest<TResponse>
    {
    }
}
