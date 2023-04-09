using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Queries;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class GetClaimsByUserIdQuery : IQuery<IEnumerable<UserClaim>>
    {
        public int UserId { get; init; }
    }

    public class GetClaimsByUserIdQueryHandler : IQueryHandler<GetClaimsByUserIdQuery, IEnumerable<UserClaim>>
    {
        private readonly IUserRepository userRepository;

        public GetClaimsByUserIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserClaim>> Handle(GetClaimsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
                return null;

            return user.UserClaims;
        }
    }
}
