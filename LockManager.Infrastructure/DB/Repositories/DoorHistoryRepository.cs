using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models.Input;
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

        public async Task<int> AddDoorHistory(AddDoorHistoryInput input, CancellationToken cancellationToken)
        {
            var doorHistory = new DoorHistory
            {
                UserId = input.UserId,
                DoorId = input.DoorId,
                EntryDateTime = input.EntryDateTime,
                IsSuccessfulEntry = input.IsSuccessfulEntry
            };

            await Context.DoorHistory.AddAsync(doorHistory, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return doorHistory.Id;
        }
    }
}
