using LockManager.Application.Repositories;
using LockManager.Infrastructure.DB.Context;

namespace LockManager.Infrastructure.DB.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly LockManagerDbContext _lockManagerDbContext;

        public Repository(LockManagerDbContext lockManagerDbContext)
        {
            _lockManagerDbContext = lockManagerDbContext;
        }

        public LockManagerDbContext Context => _lockManagerDbContext;
    }
}
