using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Roles
{
    public class RemoveRoleClaimCommand : ICommand<int>
    {
        public int RoleId { get; set; }
        public int ClaimId { get; set; }
    }

    public class RemoveRoleClaimCommandHandler : ICommandHandler<RemoveRoleClaimCommand, int>
    {
        private readonly IRoleRepository roleRepository;

        public RemoveRoleClaimCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(RemoveRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(request.RoleId, cancellationToken);
            if (role == null)
                return CommandResult<int>.Error("Role is not existing");

            role.RemoveClaim(request.ClaimId);
            await roleRepository.UpdateAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
