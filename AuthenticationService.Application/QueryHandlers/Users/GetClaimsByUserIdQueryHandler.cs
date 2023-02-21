using AuthenticationService.Application.Specifications;
using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Infrastructure.CQRS.Queries;

namespace AuthenticationService.Application.QueryHandlers.Users
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
            var specification = new GetUserClaimsSpecification(x => x.Id == request.UserId);

            var user = await userRepository.FindOneAsync(specification, cancellationToken);
            if (user == null)
                return null;

            return user.UserClaims;
        }
    }
}
