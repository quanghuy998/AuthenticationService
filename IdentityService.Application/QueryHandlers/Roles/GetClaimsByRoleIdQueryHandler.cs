using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Infrastructure.CQRS.Queries;
using IdentityService.Infrastructure.Domain.Specifications;

namespace IdentityService.Application.QueryHandlers.Roles
{
    public class GetClaimsByRoleIdQuery : IQuery<IEnumerable<RoleClaim>>
    {
        public int RoleId { get; init; }
    }

    public class GetClaimsByRoleIdQueryHandler : IQueryHandler<GetClaimsByRoleIdQuery, IEnumerable<RoleClaim>>
    {
        private readonly IRoleRepository roleRepository;

        public GetClaimsByRoleIdQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleClaim>> Handle(GetClaimsByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var role = await roleRepository.FindOneAsync(x => x.Id == request.RoleId, cancellationToken);
            if (role == null)
                return null;

            return role.RoleClaims;
        }
    }
}
