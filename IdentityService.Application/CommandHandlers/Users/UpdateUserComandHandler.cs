using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class UpdateUserCommand : ICommand<User>
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
    }

    public class UpdateUserComandHandler : ICommandHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository userRepository;
        public UpdateUserComandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.Id);
            if (user == null)
                return CommandResult<User>.Error("User is not existing");

            user.UpdateUser(request.FirstName, request.LastName, request.Email, request.Address);
            await userRepository.UpdateAsync(user, cancellationToken);

            return CommandResult<User>.Success(user);
        }
    }
}
