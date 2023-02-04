using AuthenticationService.Application.Dtos;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Queries;

namespace AuthenticationService.Application.QueryHandlers.Users
{
    public class GetUserInformationByIdQuery : IQuery<UserDto>
    {
        public int Id { get; init; }
    }

    internal class GetUserInformationByIdQueryHandler : IQueryHandler<GetUserInformationByIdQuery, UserDto>
    {
        private readonly IUserRepository userRepository;

        public GetUserInformationByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserInformationByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.Id, cancellationToken);
            if (user == null)
                return null;

            var userDto = new UserDto()
            {
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email= user.Email,
                Address= user.Address,
            };

            return userDto;
        }
    }
}
