using AuthenticationService.Application.CommandHandlers.Users;
using AuthenticationService.Infrastructure.CQRS.Commands;
using AuthenticationService.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using static AuthenticationService.Dtos.UserRequest;

namespace AuthenticationService.Controllers
{
    [Route("api/")]
    [ApiController]

    public class AuthenticateController : ControllerBase
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public AuthenticateController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request, CancellationToken cancellationToken) 
        {
            var command = new UserLoginCommand()
            {
                UserName = request.userName,
                Password = request.password
            };

            var result = await commandBus.SendAsync(command, cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.Message);

            return Ok(result.Response);
        }
    }
}