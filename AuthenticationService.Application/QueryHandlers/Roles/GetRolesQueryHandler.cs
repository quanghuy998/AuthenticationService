using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Infrastructure.CQRS.Queries;

namespace AuthenticationService.Application.QueryHandlers.Roles
{
    public class GetRolesQuery : IQuery<IEnumerable<Role>>
    {

    }

    public class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, IEnumerable<Role>>
    {
        private readonly IRoleRepository roleRepository;
        
        public GetRolesQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await roleRepository.FindAllAsync(cancellationToken);
        }
    }
}
