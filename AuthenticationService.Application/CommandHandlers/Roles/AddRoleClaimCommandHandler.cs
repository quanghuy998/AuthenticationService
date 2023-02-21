using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Roles
{
    public class AddRoleClaimCommand : ICommand<int>
    {
        public int RoleId { get; set; }
        public string ClaimName { get; set; }
    }

    public class AddRoleClaimCommandHandler : ICommandHandler<AddRoleClaimCommand, int>
    {
        private readonly IRoleRepository roleRepository;
        private const string ROLECLAIM_PREFIX = "RoleClaim_";

        public AddRoleClaimCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(AddRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(request.RoleId, cancellationToken);
            if (role == null)
                return CommandResult<int>.Error("Role is not existing");

            role.AddClaim(ROLECLAIM_PREFIX + request.ClaimName);
            await roleRepository.UpdateAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
