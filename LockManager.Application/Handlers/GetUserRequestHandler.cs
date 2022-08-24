using LockManager.Application.Repositories;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Request;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user == null)
            {
                return null;
            }

            return new UserDto(user);
        }
    }
}
