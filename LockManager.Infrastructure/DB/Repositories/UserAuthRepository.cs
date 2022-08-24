using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models.Input;
using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace LockManager.Infrastructure.DB.Repositories
{
    public class UserAuthRepository : Repository<UserAuth>, IUserAuthRepository
    {
        public UserAuthRepository(LockManagerDbContext lockManagerDbContext) : base(lockManagerDbContext)
        {
        }

        public async Task<bool> CreateUserAuth(CreateUserAuthInput input, CancellationToken cancellationToken)
        {
            var userAuth = new UserAuth
            {
                Username = input.Username,
                PasswordHash = input.PasswordHash,
                PasswordSalt = input.PasswordSalt,
            };

            await Context.UserAuth.AddAsync(userAuth, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateUserAuth(UpdateUserAuthInput input, CancellationToken cancellationToken)
        {
            var userAuth = new UserAuth
            {
                Id = input.Id,
                Username = input.Username,
                PasswordHash = input.PasswordHash,
                PasswordSalt = input.PasswordSalt,
                TokenCreated = input.TokenCreated,
                TokenExpires = input.TokenExpires
            };

            Context.UserAuth.Update(userAuth);
            await Context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<UserAuth> GetUserAuthByUsername(string username)
        {
            var userAuth = await Context.UserAuth.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);

            return userAuth;
        }
    }
}
