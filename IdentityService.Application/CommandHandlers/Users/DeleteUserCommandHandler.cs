using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class DeleteUserCommand : ICommand<int>
    {
        public int Id { get; init; }
    }

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, int>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<int>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.Id, cancellationToken);
            if (user == null)
                return CommandResult<int>.Error("User is not existing");

            await userRepository.DeleteAsync(user, cancellationToken);
            return CommandResult<int>.Success(user.Id);
        }
    }
}
