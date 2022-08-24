using LockManager.Domain.Entities;
using LockManager.Domain.Models.Input;

namespace LockManager.Application.Repositories
{
    public interface IUserAuthRepository
    {
        Task<bool> CreateUserAuth(CreateUserAuthInput input, CancellationToken cancellationToken);
        Task<UserAuth> GetUserAuthByUsername(string username);
        Task<bool> UpdateUserAuth(UpdateUserAuthInput input, CancellationToken cancellationToken);
    }
}
