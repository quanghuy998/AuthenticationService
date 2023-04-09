using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Infrastructure.CQRS.Commands;

namespace IdentityService.Application.CommandHandlers.Roles
{
    public class UpdateRoleCommand : ICommand<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(request.Id);
            if (role is null)
                return CommandResult<int>.Error("Role is not existing");

            role.Update(request.Name);
            await roleRepository.UpdateAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
