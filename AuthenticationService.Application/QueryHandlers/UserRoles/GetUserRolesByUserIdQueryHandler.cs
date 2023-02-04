using AuthenticationService.Application.Dtos;
using AuthenticationService.Application.Specifications;
using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Infrastructure.CQRS.Queries;
using Newtonsoft.Json;

namespace AuthenticationService.Application.QueryHandlers.UserRoles
{
    public class GetUserRolesByUserIdQuery : IQuery<IEnumerable<UserRoleResponse>>
    {
        public int UserId { get; init; }
    }

    public class GetUserRolesByUserIdQueryHandler : IQueryHandler<GetUserRolesByUserIdQuery, IEnumerable<UserRoleResponse>>
    {
        private readonly IUserRoleRepository userRoleRepository;

        public GetUserRolesByUserIdQueryHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRoleResponse>> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new UserRoleSpecification();
            specification.Expression = x => x.UserId == request.UserId;
            var userRoles = await userRoleRepository.FindAllAsync(specification, cancellationToken);

            return MapUserRoleToUserRoleDto(userRoles.ToList());
        }

        private List<UserRoleResponse> MapUserRoleToUserRoleDto(List<UserRole> userRoles)
        {
            var list = new List<UserRoleResponse>();
            foreach (var userRole in userRoles)
            {
                list.Add(new UserRoleResponse()
                {
                    RoleId = userRole.RoleId,
                    UserId = userRole.UserId,
                    Role = new RoleResponse()
                    {
                        Id = userRole.Role.Id,
                        RoleName = userRole.Role.Name,
                    }
                });
            }

            return list;
        }
    }
}
