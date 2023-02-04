using MediatR;

namespace AuthenticationService.Infrastructure.CQRS.Commands
{
    public interface ICommand : IRequest<CommandResult>
    {
    }

    public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
    {
    }
}
