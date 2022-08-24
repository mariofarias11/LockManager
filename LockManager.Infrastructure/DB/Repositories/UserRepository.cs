using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models.Command;
using LockManager.Infrastructure.DB.Context;

namespace LockManager.Infrastructure.DB.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LockManagerDbContext lockManagerDbContext) : base(lockManagerDbContext)
        {
        }

        public async Task<int> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Active = true,
                Role = command.Role,
                Username = command.Username
            };

            await Context.User.AddAsync(user, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
