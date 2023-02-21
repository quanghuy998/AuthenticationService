using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Users
{
    public class AddUserClaimCommand : ICommand<int>
    {
        public int UsertId { get; init; }
        public string ClaimName { get; init; }
    }

    public class AddUserClaimCommandHandler : ICommandHandler<AddUserClaimCommand, int>
    {
        private readonly IUserRepository userRepository;
        private const string USERCLAIM_PREFIX = "UserClaim_";

        public AddUserClaimCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<int>> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.UsertId, cancellationToken);
            if (user == null)
                return CommandResult<int>.Error("User is not existing");

            user.AddClaim(USERCLAIM_PREFIX +    request.ClaimName);
            await userRepository.UpdateAsync(user, cancellationToken);

            return CommandResult<int>.Success(user.Id);
        }
    }
}
