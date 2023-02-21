using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Users
{
    public class RemoveUserClaimCommand : ICommand<int>
    {
        public int UsertId { get; init; }
        public int UserClaimId { get; init; }
    }

    public class RemoveUserClaimCommandHandler : ICommandHandler<RemoveUserClaimCommand, int>
    {
        private readonly IUserRepository userRepository;

        public RemoveUserClaimCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<int>> Handle(RemoveUserClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.UsertId, cancellationToken);
            if (user == null)
                return CommandResult<int>.Error("User is not existing");

            user.RemoveClaim(request.UserClaimId);
            await userRepository.UpdateAsync(user, cancellationToken);

            return CommandResult<int>.Success(user.Id);
        }
    }
}
