using AuthenticationService.Application.Extensions;
using AuthenticationService.Application.Helpers;
using AuthenticationService.Application.Specifications;
using AuthenticationService.Domain.Aggregates.Roles;
using AuthenticationService.Domain.Aggregates.UserRoles;
using AuthenticationService.Domain.Aggregates.Users;
using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Infrastructure.CQRS.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Application.CommandHandlers.Users
{
    public class UserLoginCommand : ICommand<string>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }

    public class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, string>
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IConfiguration configuration;

        public UserLoginCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.configuration = configuration;
        }

        public async Task<CommandResult<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetUserClaimsSpecification(x => x.UserName == request.UserName);

            var user = await userRepository.FindOneAsync(specification, cancellationToken);
            if (user == null && !HashPasswordHelper.Verify(request.Password, user.PasswordHash))
                return CommandResult<string>.Error("User is not existing");

            var roleIds = user.UserRoles.Select(x => x.RoleId).ToList();

            var spec = new GetRoleClaimsSpecification(x => roleIds.Any(y => y == x.Id));
            var roles = await roleRepository.FindAllAsync(spec, cancellationToken);

            var claims = new List<string>();
            claims.AddRange(GetUserClaims(user));
            claims.AddRange(GetRoleClaims(roles.ToList()));

            string token = GenerateToken(user, roles.Select(x => x.Name).ToArray(), claims.ToArray());

            return CommandResult<string>.Success(token);
        }

        private List<string> GetUserClaims(User user)
        {
            return user.UserClaims.Select(x => x.ClaimValue).ToList();
        }

        private List<string> GetRoleClaims(List<Role> roles)
        {
            var claims = new List<string>();
            var roleClaims = roles.Select(x => x.RoleClaims);
            foreach (var roleClaim in roleClaims)
            {
                var claimValues = roleClaim.Select(x => x.ClaimValue).ToList();
                claims.AddRange(claimValues);
            }

            return claims;
        }

        private string GenerateToken(User user, string[] roles, string[] claims)
        {
            var clientSecret = configuration.GetAuthenticationConfig("AdminAudienceSecret");
            var audienceSecret = configuration.GetAuthenticationConfig("AdminAudienceSecret");
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceSecret));

            var myAudience = "0f8fad5b-d9cb-469f-a165-70867728950e";

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = configuration.GetAuthenticationConfig("Issuer"),
                Audience = myAudience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("roles", JsonConvert.SerializeObject(roles), JsonClaimValueTypes.JsonArray),
                    new Claim("claims", JsonConvert.SerializeObject(claims), JsonClaimValueTypes.JsonArray)
                }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
