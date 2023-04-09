using IdentityService.Application.CommandHandlers.Roles;
using IdentityService.Application.QueryHandlers.Roles;
using IdentityService.Infrastructure.CQRS.Commands;
using IdentityService.Infrastructure.CQRS.Queries;
using IdentityService.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/roles")]
    [ApiController]

    public class RoleController : ControllerBase
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public RoleController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        [HttpPost]
        [Authorize(Policy.CanCreateRole)]
        public async Task<IActionResult> Create([FromBody] string name, CancellationToken cancellationToken)
        {
            var command = new CreateRoleCommand()
            {
                Name = name
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpGet]
        [Authorize(Policy.CanGetRole)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetRolesQuery();

            var result = await queryBus.SendAsync(query, cancellationToken);

            return Ok(result.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy.CanGetRole)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetRoleByIdQuery()
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
        [Authorize(Policy.CanUpdateRole)]
        public async Task<IActionResult> Update([FromRoute] int id, string name, CancellationToken cancellationToken)
        {
            var command = new UpdateRoleCommand()
            {
                Id = id,
                Name = name,
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy.CanDeleteRole)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand()
            {
                Id = id
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpGet]
        [Route("{roleId}/claims")]
        public async Task<IActionResult> GetRoleClaims([FromRoute] int roleId, CancellationToken cancellationToken)
        {
            var query = new GetClaimsByRoleIdQuery()
            {
                RoleId = roleId,
            };

            var result = await queryBus.SendAsync(query, cancellationToken);
            if (result.Any())
                return BadRequest();

            return Ok(result.ToList());
        }

        [HttpPost]
        [Route("{roleId}/claims")]
        public async Task<IActionResult> AddClaim([FromRoute] int roleId, [FromBody] string claimName, CancellationToken cancellationToken)
        {
            var command = new AddRoleClaimCommand()
            {
                RoleId = roleId,
                ClaimName = claimName,
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }

        [HttpDelete]
        [Route("{roleId}/claims/{claimId}")]
        public async Task<IActionResult> RemoveClaim([FromRoute] int roleId, [FromRoute] int claimId, CancellationToken cancellationToken)
        {
            var command = new RemoveRoleClaimCommand()
            {
                RoleId = roleId,
                ClaimId = claimId
            };

            var result = await commandBus.SendAsync(command, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Response);
        }
    }
}
