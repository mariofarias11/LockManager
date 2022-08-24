using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class LoginCommand : IRequest<string>
    {
        public string Username { get; set; } 
        public string Password { get; set; }
        public UserDto User { get; set; }
    }
}
