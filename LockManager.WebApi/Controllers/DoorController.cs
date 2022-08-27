using System.Security.Claims;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Query;
using LockManager.Domain.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LockManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateDoor([FromBody] CreateDoorCommand command, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || user.Role < Role.Admin || !user.Active)
            {
                return Unauthorized("This user can not create doors");
            }

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet, Route("{id}:min(1)/entry-history"), Authorize]
        public async Task<IActionResult> GetDoorHistoryEvents([FromRoute] int id, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || user.Role < Role.Admin || !user.Active)
            {
                return Unauthorized("This user can not access door's entry history");
            }

            var query = new GetDoorHistoryQuery { DoorId = id };
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPatch, Route("{id}:min(1)/openness"), Authorize]
        public async Task<ActionResult<DoorDto>> UpdateDoorOpenness([FromRoute] int id, [FromBody] UpdateDoorOpennessRequest request, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || !user.Active)
            {
                return Unauthorized("This user can not open this door");
            }

            var command = new UpdateDoorOpennessCommand
            {
                Id = id,
                Open = request.Open,
                User = user
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
            {
                return BadRequest($"Door {id} not found");
            }

            if (!string.IsNullOrEmpty(result.UnauthorizedMessage))
            {
                return Unauthorized(result.UnauthorizedMessage);
            }

            return Ok(result);
        }

        [HttpPatch, Route("{id}:min(1)/minimumRoleAuthorized"), Authorize]
        public async Task<ActionResult<DoorDto>> UpdateMinimumRoleAuthorizedToOpenDoor([FromRoute] int id, [FromBody] UpdateMinimumRoleAuthorizedToOpenDoorRequest request, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || user.Role < Role.Admin || !user.Active)
            {
                return Unauthorized("This user can not modify this door");
            }

            var command = new UpdateMinimumRoleAuthorizedToOpenDoorCommand
            {
                Id = id,
                MinimumRoleAuthorized = request.MinimumRoleAuthorized
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
            {
                return BadRequest($"Door {id} not found");
            }

            return Ok(result);
        }
    }
}
