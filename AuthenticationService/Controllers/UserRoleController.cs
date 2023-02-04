using AuthenticationService.Application.CommandHandlers.UserRoles;
using AuthenticationService.Application.QueryHandlers.UserRoles;
using AuthenticationService.Infrastructure.CQRS.Commands;
using AuthenticationService.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/userroles")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public UserRoleController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int userId, int roleId, CancellationToken cancellationToken)
        {
            var command = new CreateUserRoleCommand()
            {
                UserId = userId,
                RoleId = roleId
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoleByUserIdOrRoleId([FromQuery] int userId, [FromQuery] int roleId, CancellationToken cancellationToken)
        {
            if (userId == 0 && roleId == 0)
                return BadRequest();

            if (userId != 0)
            {
                var query = new GetUserRolesByUserIdQuery() { UserId = userId };
                var result = await queryBus.SendAsync(query, cancellationToken);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }    

            if (roleId != 0)
            {
                var query = new GetUserRolesByRoleIdQuery() { RoleId = roleId };
                var result = await queryBus.SendAsync(query, cancellationToken);
                if (result is null)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserRoleCommand()
            {
                Id = id
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }
    }
}
