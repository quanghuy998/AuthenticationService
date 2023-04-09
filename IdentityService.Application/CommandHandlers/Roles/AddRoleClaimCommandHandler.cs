using IdentityService.Infrastructure.CQRS.Commands;
using IdentityService.Domain.Aggregates.Roles;

namespace IdentityService.Application.CommandHandlers.Roles
{
    public class AddRoleClaimCommand : ICommand<int>
    {
        public int RoleId { get; set; }
        public string ClaimName { get; set; }
    }

    public class AddRoleClaimCommandHandler : ICommandHandler<AddRoleClaimCommand, int>
    {
        private readonly IRoleRepository roleRepository;

        public AddRoleClaimCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(AddRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(request.RoleId, cancellationToken);
            if (role == null)
                return CommandResult<int>.Error("Role is not existing");

            role.AddClaim(request.ClaimName);
            await roleRepository.UpdateAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
