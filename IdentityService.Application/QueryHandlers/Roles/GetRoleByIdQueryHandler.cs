using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Infrastructure.CQRS.Queries;

namespace IdentityService.Application.QueryHandlers.Roles
{
    public class GetRoleByIdQuery : IQuery<Role>
    {
        public int Id { get; init; }
    }

    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, Role>
    {
        private readonly IRoleRepository roleRepository;

        public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<Role> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.FindOneAsync(request.Id, cancellationToken);
        }
    }
}
