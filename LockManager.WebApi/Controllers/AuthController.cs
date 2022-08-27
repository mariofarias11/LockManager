using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LockManager.WebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserQuery { Username = command.Username});
            if (user is null)
            {
                return BadRequest($"User {command.Username} not found");
            }

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserQuery { Username = request.Username }, cancellationToken);

            if (user is null)
            {
                return BadRequest("User not found.");
            }

            var command = new LoginCommand
            {
                User = user,
                Username = request.Username,
                Password = request.Password
            };

            var result = await _mediator.Send(command, cancellationToken);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Wrong password.");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token"), Authorize]
        public async Task<ActionResult<string>> RefreshToken(CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var user = await _mediator.Send(new GetUserQuery { Username = username }, cancellationToken);

            if (user is null)
            {
                return BadRequest("User not found.");
            }

            var command = new RefreshTokenCommand { User = user };
            var result = await _mediator.Send(command, cancellationToken);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized("Token expired.");
            }

            return Ok(result);
        }
    }
}
