using MediatR;

namespace AuthenticationService.Infrastructure.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult>
        where TCommand : IRequest<CommandResult>
    {
    }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, CommandResult<TResponse>>
        where TCommand : IRequest<CommandResult<TResponse>>
    {
    }
}
