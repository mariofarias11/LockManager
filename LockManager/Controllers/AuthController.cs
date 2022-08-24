using System.Security.Claims;
using LockManager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using LockManager.Domain.Auth.Dto;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Request;
using MediatR;

namespace LockManager.WebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;

        public AuthController(ITokenService tokenService, IMediator mediator)
        {
            _tokenService = tokenService;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthDto>> Register(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserRequest { Username = command.Username});
            if (user is null)
            {
                return BadRequest($"User {command.Username} not found");
            }

            var result = _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var user = await _mediator.Send(new GetUserRequest { Username = username?.Value }, cancellationToken);

            if (user is null)
            {
                return BadRequest("User not found.");
            }

            command.User = user;
            var result = await _mediator.Send(command, cancellationToken);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest("Wrong password.");
            }

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(CancellationToken cancellationToken)
        {
            var username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var user = await _mediator.Send(new GetUserRequest { Username = username?.Value }, cancellationToken);

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
