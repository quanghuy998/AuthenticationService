using AuthenticationService.Application.Specifications;
using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.CQRS.Queries;

namespace AuthenticationService.Application.QueryHandlers.Roles
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
            var specification = new GetRoleClaimsSpecification(x => x.Id == request.RoleId);

            var role = await roleRepository.FindOneAsync(specification, cancellationToken);
            if (role == null)
                return null;

            return role.RoleClaims;
        }
    }
}
