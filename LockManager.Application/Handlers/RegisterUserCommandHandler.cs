using LockManager.Application.Repositories;
using LockManager.Application.Services;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Input;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserAuthRepository _userAuthRepository;

        public RegisterUserCommandHandler(ITokenService tokenService, IUserAuthRepository userAuthRepository)
        {
            _tokenService = tokenService;
            _userAuthRepository = userAuthRepository;
        }

        public async Task<bool> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            _tokenService.CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var input = new CreateUserAuthInput
            {
                Username = command.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            var result = await _userAuthRepository.CreateUserAuth(input, cancellationToken);
            return result;
        }
    }
}
