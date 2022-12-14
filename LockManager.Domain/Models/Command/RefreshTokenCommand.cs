using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public UserDto User { get; set; }
    }
}
