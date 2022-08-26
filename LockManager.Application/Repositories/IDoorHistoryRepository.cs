using LockManager.Domain.Entities;
using LockManager.Domain.Models.Input;

namespace LockManager.Application.Repositories
{
    public interface IDoorHistoryRepository
    {
        IEnumerable<DoorHistory> GetByDoorId(int doorId);
        Task<int> AddDoorHistory(AddDoorHistoryInput input, CancellationToken cancellationToken);
    }
}
