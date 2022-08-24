using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
        public Role? Role { get; set; }
        public bool Active { get; set; } = true;
    }
}
