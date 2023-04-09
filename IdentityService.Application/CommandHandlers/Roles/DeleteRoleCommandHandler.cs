using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Roles
{
    public class DeleteRoleCommand : ICommand<int>
    {
        public int Id { get; init; }
    }

    public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(request.Id);
            if (role == null)
                return CommandResult<int>.Error("Role is not existing");

            await roleRepository.DeleteAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
