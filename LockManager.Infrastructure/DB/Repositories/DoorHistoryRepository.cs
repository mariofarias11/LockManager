using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace LockManager.Infrastructure.DB.Repositories
{
    public class DoorHistoryRepository : Repository<DoorHistory>, IDoorHistoryRepository
    {
        public DoorHistoryRepository(LockManagerDbContext lockManagerDbContext) : base(lockManagerDbContext)
        {
        }

        public IEnumerable<DoorHistory> GetByDoorId(int doorId)
        {
            var doorHistory = Context.DoorHistory.AsNoTracking().Where(x => x.DoorId == doorId);
            return doorHistory.AsEnumerable();
        }
    }
}
