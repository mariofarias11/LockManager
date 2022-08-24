using LockManager.Application.Repositories;
using LockManager.Application.Services;
using LockManager.Domain.Models.Command;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserAuthRepository _userAuthRepository;

        public RefreshTokenCommandHandler(ITokenService tokenService, IUserAuthRepository userAuthRepository)
        {
            _tokenService = tokenService;
            _userAuthRepository = userAuthRepository;
        }

        public async Task<string> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var userAuth = await _userAuthRepository.GetUserAuthByUsername(command.User.Username);

            if (userAuth.TokenExpires < DateTime.Now)
            {
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            userAuth.SetRefreshToken(refreshToken);

            string token = _tokenService.CreateToken(command.User);
            return token;
        }
    }
}
