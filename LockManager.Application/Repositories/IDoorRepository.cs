using LockManager.Domain.Entities;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Input;

namespace LockManager.Application.Repositories
{
    public interface IDoorRepository
    {
        Task<IEnumerable<Door>> GetDoors();
        Task<Door> GetDoorById(int id);
        Task<Door> CreateDoor(Role minimumRoleAuthorized, CancellationToken cancellationToken);
        Task<Door> UpdateDoor(UpdateDoorInput input, CancellationToken cancellationToken);
    }
}
