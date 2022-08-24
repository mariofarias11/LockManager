using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class CreateDoorCommandHandler : IRequestHandler<CreateDoorCommand, DoorDto>
    {
        private readonly IDoorRepository _doorRepository;

        public CreateDoorCommandHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public async Task<DoorDto> Handle(CreateDoorCommand command, CancellationToken cancellationToken)
        {
            var door = await _doorRepository.CreateDoor(command.MinimumRoleAuthorized, cancellationToken);
            return new DoorDto(door);
        }
    }
}
