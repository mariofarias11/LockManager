using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Input;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class UpdateDoorOpennessCommandHandler : IRequestHandler<UpdateDoorOpennessCommand, DoorDto>
    {
        private readonly IDoorRepository _doorRepository;

        public UpdateDoorOpennessCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public async Task<DoorDto> Handle(UpdateDoorOpennessCommand command, CancellationToken cancellationToken)
        {
            var door = await _doorRepository.GetDoorById(command.Id);

            if (door == null)
            {
                return null;
            }

            if (door.MinimumRoleAuthorized > command.User.Role)
            {
                //save history
                return new DoorDto
                {
                    UnauthorizedMessage = $"User {command.User.Id} do not have permission to open door {command.Id}"
                };
            }

            var input = new UpdateDoorInput
            {
                Id = door.Id,
                Open = command.Open,
                MinimumRoleAuthorized = door.MinimumRoleAuthorized
            };
            var entity = await _doorRepository.UpdateDoor(input, cancellationToken);
            return new DoorDto(entity);
        }
    }
}
