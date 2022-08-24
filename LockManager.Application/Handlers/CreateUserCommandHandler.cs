using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _userRepository.CreateUser(command, cancellationToken);
            return result;
        }
    }
}
