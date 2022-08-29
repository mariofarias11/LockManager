using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Input;
using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;

namespace LockManager.Infrastructure.DB.Repositories
{
    public class DoorRepository : Repository<Door>, IDoorRepository
    {
        public DoorRepository(LockManagerDbContext lockManagerDbContext) : base(lockManagerDbContext)
        {
        }

        public async Task<Door> GetDoorById(int id)
        {
            var door = await Context.Door.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return door;
        }

        public async Task<Door> CreateDoor(Role minimumRoleAuthorized, CancellationToken cancellationToken)
        {
            var door = new Door
            {
                Open = false,
                MinimumRoleAuthorized = minimumRoleAuthorized
            };

            await Context.Door.AddAsync(door, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return door;
        }

        public async Task<Door> UpdateDoor(UpdateDoorInput input, CancellationToken cancellationToken)
        {
            var door = new Door
            {
                Id = input.Id,
                Open = input.Open,
                MinimumRoleAuthorized = input.MinimumRoleAuthorized
            };

            Context.Door.Update(door);
            await Context.SaveChangesAsync(cancellationToken);

            return door;
        }

        public async Task<IEnumerable<Door>> GetDoors()
        {
            var doors = await Context.Door.AsNoTracking().ToListAsync();
            return doors;
        }
    }
}
