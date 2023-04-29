using IdentityService.Application.Extensions;
using IdentityService.Application.QueryHandlers.Users;
using IdentityService.Infrastructure.CQRS.Commands;
using IdentityService.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static IdentityService.Dtos.UserRequest;

namespace IdentityService.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public OAuthController(IQueryBus queryBus, ICommandBus commandBus, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("oauth/token")]
        public async Task<IActionResult> GetToken([FromBody] UserLoginRequest request, CancellationToken cancellationToken)
        {
            var query = new GetTokenCommand()
            {
                UserName = request.userName,
                Password = request.password
            };

            var result = await queryBus.SendAsync(query, cancellationToken);

            if (result == null)
                return Unauthorized();

            setLargeCookie("orange_access_token", result.AccessToken, null, true);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("oauth/token/validate")]
        public async Task<IActionResult> ValidateUserToken(CancellationToken cancellationToken)
        {

            if (!User.Identity.IsAuthenticated || configuration.IsEnableFeatureFlag("DisableAuthenticationInDevelopment"))
                return Unauthorized();

            var query = new ValidateUserQuery()
            {
                User = User,
            };

            var result = await queryBus.SendAsync(query, cancellationToken);


            return result ? Ok() : Unauthorized();
        }

        private void setCookie(string key, string value, DateTime? expires, bool isHttpOnly)
        {
            var httpResponse = httpContextAccessor.HttpContext.Response;
            httpResponse.Cookies.Append(key, value, new CookieOptions
            {
                Secure = true,
                HttpOnly = isHttpOnly,
                SameSite = SameSiteMode.None,
                Expires = expires,
                Domain = "localhost"
            });
        }

        private void setLargeCookie(string key, string value, DateTime? expires, bool isHttpOnly)
        {
            var maxPageSize = 4000 / sizeof(char);
            var pages = (int)Math.Ceiling(value.Length / (decimal)maxPageSize);

            if (pages == 1)
                setCookie(key, value, expires, isHttpOnly);
            else
            {
                for (var i = 0; i < pages; i++)
                {
                    var cookiePage = string.Join("", value.Skip(maxPageSize * i).Take(maxPageSize));
                    setCookie(key + "_" + i, cookiePage, expires, isHttpOnly);
                }
            }
        }
    }
}
