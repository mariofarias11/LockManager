using LockManager.Domain.Entities;

namespace LockManager.Application.Repositories
{
    public interface IDoorHistoryRepository
    {
        IEnumerable<DoorHistory> GetByDoorId(int doorId);
    }
}
