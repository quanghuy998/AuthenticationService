using IdentityService.Application.Dtos;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Queries;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class GetUserInformationsQuery : IQuery<IEnumerable<UserResponse>>
    {

    }

    public class GetUserInformationsQueryHandler : IQueryHandler<GetUserInformationsQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository userRepository;

        public GetUserInformationsQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetUserInformationsQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.FindAllAsync(cancellationToken);

            var userDtos = new List<UserResponse>();
            foreach(var user in users)
            {
                var userDto = new UserResponse()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    CreatedTime= user.CreatedTime,
                    ModifiedTime= user.ModifiedTime,
                };
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
