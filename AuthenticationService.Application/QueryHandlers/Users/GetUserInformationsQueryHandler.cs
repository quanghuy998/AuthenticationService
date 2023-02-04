using AuthenticationService.Application.Dtos;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Queries;

namespace AuthenticationService.Application.QueryHandlers.Users
{
    public class GetUserInformationsQuery : IQuery<IEnumerable<UserDto>>
    {

    }

    public class GetUserInformationsQueryHandler : IQueryHandler<GetUserInformationsQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository userRepository;

        public GetUserInformationsQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUserInformationsQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.FindAllAsync(cancellationToken);

            var userDtos = new List<UserDto>();
            foreach(var user in users)
            {
                var userDto = new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                };
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
