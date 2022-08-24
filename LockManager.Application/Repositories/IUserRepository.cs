using LockManager.Domain.Models.Command;

namespace LockManager.Application.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(CreateUserCommand command, CancellationToken cancellationToken);
    }
}
