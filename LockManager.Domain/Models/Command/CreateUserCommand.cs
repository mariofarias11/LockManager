using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
