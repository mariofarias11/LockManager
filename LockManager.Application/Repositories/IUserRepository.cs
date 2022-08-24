using LockManager.Domain.Entities;
using LockManager.Domain.Models.Command;

namespace LockManager.Application.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(CreateUserCommand command, CancellationToken cancellationToken);
        Task<User> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken);
        Task<User> GetUserByUsername(string username);
    }
}
