﻿using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.CQRS.Commands;

namespace AuthenticationService.Application.CommandHandlers.Roles
{
    public class CreateRoleCommand : ICommand<int>
    {
        public string Name { get; init; }
    }

    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<CommandResult<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Name);
            await roleRepository.CreateAsync(role, cancellationToken);

            return CommandResult<int>.Success(role.Id);
        }
    }
}
