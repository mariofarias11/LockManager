using LockManager.Application.Repositories;
using LockManager.Application.Services;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Input;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserAuthRepository _userAuthRepository;

        public LoginCommandHandler(ITokenService tokenService, IUserAuthRepository userAuthRepository)
        {
            _tokenService = tokenService;
            _userAuthRepository = userAuthRepository;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var userAuth = await _userAuthRepository.GetUserAuthByUsername(command.Username);
            var isPasswordCorrect = userAuth.VerifyPasswordHash(command.Password);
            if (!isPasswordCorrect)
            {
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            userAuth.SetRefreshToken(refreshToken);

            var updateUserAuthInput = new UpdateUserAuthInput(userAuth);
            await _userAuthRepository.UpdateUserAuth(updateUserAuthInput, cancellationToken);

            string token = _tokenService.CreateToken(command.User);
            return token;
        }
    }
}
