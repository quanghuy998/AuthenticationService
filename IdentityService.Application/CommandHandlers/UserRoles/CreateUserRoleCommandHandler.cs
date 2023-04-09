using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.UserRoles
{
    public class CreateUserRoleCommand : ICommand<int>
    {
        public int UserId { get; init; }
        public int RoleId { get; init; }
    }

    public class CreateUserRoleCommandHandler : ICommandHandler<CreateUserRoleCommand, int>
    {
        private readonly IUserRoleRepository userRoleRepository;

        public CreateUserRoleCommandHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<CommandResult<int>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = new UserRole()
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            };

            await userRoleRepository.CreateAsync(userRole, cancellationToken);

            return CommandResult<int>.Success(userRole.Id);
        }
    }
}
