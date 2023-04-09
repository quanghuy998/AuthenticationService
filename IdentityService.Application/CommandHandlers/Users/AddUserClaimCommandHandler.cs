using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class AddUserClaimCommand : ICommand<int>
    {
        public int UsertId { get; init; }
        public string ClaimName { get; init; }
    }

    public class AddUserClaimCommandHandler : ICommandHandler<AddUserClaimCommand, int>
    {
        private readonly IUserRepository userRepository;

        public AddUserClaimCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<int>> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.UsertId, cancellationToken);
            if (user == null)
                return CommandResult<int>.Error("User is not existing");

            user.AddClaim(request.ClaimName);
            await userRepository.UpdateAsync(user, cancellationToken);

            return CommandResult<int>.Success(user.Id);
        }
    }
}
