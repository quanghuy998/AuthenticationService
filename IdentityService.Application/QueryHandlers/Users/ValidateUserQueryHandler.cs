using IdentityService.Domain.Aggregates.Users;
using IdentityService.Infrastructure.CQRS.Queries;
using System.Security.Claims;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class ValidateUserQuery : IQuery<bool>
    {
        public ClaimsPrincipal User { get; init; }
    }

    public class ValidateUserQueryHandler : IQueryHandler<ValidateUserQuery, bool>
    {
        private readonly IUserRepository userRepository;

        public ValidateUserQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            var claim = request.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
            var user = await userRepository.FindOneAsync(x => x.Email == claim.Value);
            if (user != null)
                return true;

            return false;
        }
    }
}
