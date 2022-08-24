using System.Security.Claims;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LockManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || user.Role < Role.Admin || !user.Active)
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPatch, Route("{id:min(1)}"), Authorize]
        public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null || user.Role < Role.Admin || !user.Active)
            {
                return Unauthorized();
            }

            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
