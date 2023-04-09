using IdentityService.Application.Dtos;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Queries;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class GetUserInformationByIdQuery : IQuery<UserResponse>
    {
        public int Id { get; init; }
    }

    internal class GetUserInformationByIdQueryHandler : IQueryHandler<GetUserInformationByIdQuery, UserResponse>
    {
        private readonly IUserRepository userRepository;

        public GetUserInformationByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserInformationByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(request.Id, cancellationToken);
            if (user == null)
                return null;

            var userDto = new UserResponse()
            {
                Id = request.Id,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email= user.Email,
                Address= user.Address,
                CreatedTime = user.CreatedTime,
                ModifiedTime = user.ModifiedTime,
            };

            return userDto;
        }
    }
}
