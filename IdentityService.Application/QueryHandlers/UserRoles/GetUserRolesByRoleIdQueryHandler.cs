using IdentityService.Application.Dtos;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.CQRS.Queries;
using IdentityService.Infrastructure.Domain.Specifications;

namespace IdentityService.Application.QueryHandlers.UserRoles
{
    public class GetUserRolesByRoleIdQuery : IQuery<IEnumerable<UserRoleResponse>>
    {
        public int RoleId { get; init; }
    }

    public class GetUserRolesByRoleIdQueryHandler : IQueryHandler<GetUserRolesByRoleIdQuery, IEnumerable<UserRoleResponse>>
    {
        private readonly IUserRoleRepository userRoleRepository;

        public GetUserRolesByRoleIdQueryHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRoleResponse>> Handle(GetUserRolesByRoleIdQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await userRoleRepository.FindAllAsync(x => x.RoleId == request.RoleId, cancellationToken);

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
                    User = new UserResponse()
                    {
                        Id = userRole.User.Id,
                        FirstName = userRole.User.FirstName,
                        Address = userRole.User.Address,
                        LastName = userRole.User.LastName,
                        Email = userRole.User.Email,
                        CreatedTime = userRole.User.CreatedTime,
                        ModifiedTime = userRole.User.ModifiedTime,
                    },
                });
            }

            return list;
        }

    }
}
