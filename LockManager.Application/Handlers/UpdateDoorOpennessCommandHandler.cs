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

        public async Task<DoorDto> Handle(UpdateDoorOpennessCommand opennessCommand, CancellationToken cancellationToken)
        {
            var door = await _doorRepository.GetDoorById(opennessCommand.Id);

            if (door == null)
            {
                return null;
            }

            if (door.MinimumRoleAuthorized > opennessCommand.User.Role)
            {
                //save history
                return new DoorDto
                {
                    UnauthorizedMessage = "This user do not have permission to open this door"
                };
            }

            var input = new UpdateDoorInput
            {
                Id = door.Id,
                Open = opennessCommand.Open,
                MinimumRoleAuthorized = door.MinimumRoleAuthorized
            };
            var entity = await _doorRepository.UpdateDoor(input, cancellationToken);
            return new DoorDto(entity);
        }
    }
}
