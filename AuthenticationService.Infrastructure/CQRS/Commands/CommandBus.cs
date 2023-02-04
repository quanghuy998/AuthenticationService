using Microsoft.Extensions.DependencyInjection;
using AuthenticationService.Domain.SeedWork;
using MediatR;

namespace AuthenticationService.Infrastructure.CQRS.Commands
{
    internal class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        
        public CommandBus(IServiceProvider serviceProvider)
        {
            mediator = serviceProvider.GetRequiredService<IMediator>();
            unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public Task<CommandResult> SendAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            return unitOfWork.ExecuteAsync(() => mediator.Send(command, cancellationToken), cancellationToken);
        }

        public Task<CommandResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            return unitOfWork.ExecuteAsync(() => mediator.Send(command, cancellationToken), cancellationToken);
        }
    }
}
