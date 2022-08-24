using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models.Command;
using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = Context.User.FirstOrDefault(x => x.Id == command.Id);

            if (user == null)
            {
                return null;
            }

            if (command.Role.HasValue)
            {
                user.Role = command.Role.Value;
            }

            user.Active = command.Active;

            Context.User.Update(user);
            await Context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await Context.User.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }
    }
}
