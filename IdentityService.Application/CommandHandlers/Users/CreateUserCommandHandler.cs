using IdentityService.Application.Dtos;
using IdentityService.Application.Helpers;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class CreateUserCommand : ICommand<User>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Address { get; init; }
    }

    internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository userRepository;
        
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<CommandResult<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var hashedPassword = HashPasswordHelper.HashPassword(command.Password);
            var user = new User(command.FirstName, command.LastName, command.Email, command.Email, hashedPassword, command.Address);
            await userRepository.CreateAsync(user, cancellationToken);

            return CommandResult<User>.Success(user);
        }
    }
}
