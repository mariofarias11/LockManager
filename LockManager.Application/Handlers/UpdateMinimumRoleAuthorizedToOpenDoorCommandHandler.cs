using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Input;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class UpdateMinimumRoleAuthorizedToOpenDoorCommandHandler : IRequestHandler<UpdateMinimumRoleAuthorizedToOpenDoorCommand, DoorDto>
    {
        private readonly IDoorRepository _doorRepository;

        public UpdateMinimumRoleAuthorizedToOpenDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public async Task<DoorDto> Handle(UpdateMinimumRoleAuthorizedToOpenDoorCommand opennessCommand, CancellationToken cancellationToken)
        {
            var door = await _doorRepository.GetDoorById(opennessCommand.Id);

            if (door == null)
            {
                return null;
            }

            var input = new UpdateDoorInput
            {
                Id = door.Id,
                Open = door.Open,
                MinimumRoleAuthorized = opennessCommand.MinimumRoleAuthorized
            };
            var entity = await _doorRepository.UpdateDoor(input, cancellationToken);
            return new DoorDto(entity);
        }
    }
}
