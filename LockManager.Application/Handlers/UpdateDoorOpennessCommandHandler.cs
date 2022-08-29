using LockManager.Application.Repositories;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Event;
using LockManager.Domain.Models.Input;
using MassTransit;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class UpdateDoorOpennessCommandHandler : IRequestHandler<UpdateDoorOpennessCommand, DoorDto>
    {
        private readonly IDoorRepository _doorRepository;
        private readonly IBus _bus;

        public UpdateDoorOpennessCommandHandler(IDoorRepository doorRepository, IBus bus)
        {
            _doorRepository = doorRepository;
            _bus = bus;
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
                await PublishAddDoorHistoryEvent(false, door.Id, command.User.Id);
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

            if (command.Open)
            {
                await PublishAddDoorHistoryEvent(true, door.Id, command.User.Id);
            }

            return new DoorDto(entity);
        }

        private async Task PublishAddDoorHistoryEvent(bool isSuccessfulEntry, int doorId, int userId)
        {
            var doorHistoryEvent = new AddDoorHistoryEvent
            {
                DoorId = doorId,
                UserId = userId,
                EntryDateTime = DateTime.Now,
                IsSuccessfulEntry = isSuccessfulEntry
            };

            await _bus.Publish(doorHistoryEvent);
        }
    }
}
