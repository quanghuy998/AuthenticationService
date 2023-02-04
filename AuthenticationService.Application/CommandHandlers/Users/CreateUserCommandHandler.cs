using AuthenticationService.Application.Helpers;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Users
{
    public class CreateUserCommand : ICommand<int>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string Address { get; init; }
    }

    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository userRepository;
        
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var hashedPassword = HashPasswordHelper.HashPassword(command.Password);
            var user = new User(command.FirstName, command.LastName, command.Email, command.UserName, hashedPassword, command.Address);
            await userRepository.CreateAsync(user, cancellationToken);

            return CommandResult<int>.Success(user.Id);
        }
    }
}
