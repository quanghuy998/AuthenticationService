using AuthenticationService.Application.CommandHandlers.Users;
using AuthenticationService.Application.QueryHandlers.Users;
using AuthenticationService.Infrastructure.CQRS.Commands;
using AuthenticationService.Infrastructure.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using static AuthenticationService.Dtos.UserRequest;

namespace AuthenticationService.Controllers
{
    [Route("api/users")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public UserController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand()
            {
                FirstName = request.firstName,
                LastName = request.lastName,
                Email = request.email,
                UserName = request.userName,
                Password = request.password,
                Address = request.address,
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetUserInformationsQuery();

            var result = await queryBus.SendAsync(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id, CancellationToken cancellationToken)
        {
            var query = new GetUserInformationByIdQuery()
            {
                Id = id
            };

            var result = await queryBus.SendAsync(query, cancellationToken);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand()
            {
                Id = id,
                Address = request.address,
                Email = request.email,
                FirstName = request.firstName,
                LastName = request.lastName,
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand()
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
