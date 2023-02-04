using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.UserRoles
{
    public class DeleteUserRoleCommand : ICommand<int>
    {
        public int Id { get; set; }
    }

    public class DeleteUserRoleCommandHandler : ICommandHandler<DeleteUserRoleCommand, int>
    {
        private readonly IUserRoleRepository userRoleRepository;

        public DeleteUserRoleCommandHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<CommandResult<int>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await userRoleRepository.FindOneAsync(request.Id, cancellationToken);
            if (userRole == null)
                return CommandResult<int>.Error("User role is not existing");

            await userRoleRepository.DeleteAsync(userRole, cancellationToken);

            return CommandResult<int>.Success(userRole.Id);
        }
    }
}
