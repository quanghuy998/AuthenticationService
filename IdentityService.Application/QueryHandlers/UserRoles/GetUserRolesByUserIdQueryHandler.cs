using IdentityService.Application.Dtos;
using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.CQRS.Queries;
using IdentityService.Infrastructure.Domain.Specifications;
using Newtonsoft.Json;

namespace IdentityService.Application.QueryHandlers.UserRoles
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
            var userRoles = await userRoleRepository.FindAllAsync(x => x.UserId == request.UserId, cancellationToken);

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
