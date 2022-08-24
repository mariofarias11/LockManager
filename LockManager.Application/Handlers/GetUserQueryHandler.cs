using LockManager.Application.Repositories;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Request;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(query.Username);

            if (user == null)
            {
                return null;
            }

            return new UserDto(user);
        }
    }
}
