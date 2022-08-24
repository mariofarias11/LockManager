using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.UpdateUser(command, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return new UserDto(user);
        }
    }
}
