using IdentityService.Application.Dtos;
using IdentityService.Application.Extensions;
using IdentityService.Application.Helpers;
using IdentityService.Domain.Aggregates.Roles;
using IdentityService.Domain.Aggregates.UserRoles;
using IdentityService.Domain.Aggregates.Users;
using IdentityService.Domain.SeedWork;
using IdentityService.Infrastructure.CQRS.Commands;
using IdentityService.Infrastructure.CQRS.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class GetTokenCommand : IQuery<GetTokenResponse>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
    }

    public class GetTokenCommandHandler : IQueryHandler<GetTokenCommand, GetTokenResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IConfiguration configuration;

        public GetTokenCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.configuration = configuration;
        }

        public async Task<GetTokenResponse> Handle(GetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindOneAsync(x => x.UserName == request.UserName, cancellationToken);
            if (user is null)
                return null;

            var claims = await userRepository.GetUserClaims(x => x.UserName == request.UserName, cancellationToken);
            var userRoles = await userRoleRepository.FindAllAsync(x => x.UserId == user.Id);

            var response = new GetTokenResponse()
            {
                AccessToken = generateToken(user, userRoles.Select(_ => _.Role.Name).ToList(), claims)
            };

            return response;
        }

        private string generateToken(User user, List<string> roles, List<string> claims)
        {
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
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim("roles", JsonConvert.SerializeObject(roles), JsonClaimValueTypes.JsonArray),
                    new Claim("claims", JsonConvert.SerializeObject(claims), JsonClaimValueTypes.JsonArray)
                }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
