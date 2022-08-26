using LockManager.Application.Repositories;
using LockManager.Domain.Models.Event;
using LockManager.Domain.Models.Input;
using MassTransit;

namespace LockManager.Application.Consumers
{
    public class AddDoorHistoryEventConsumer : IConsumer<AddDoorHistoryEvent>
    {
        private readonly IDoorHistoryRepository _doorHistoryRepository;

        public AddDoorHistoryEventConsumer(IDoorHistoryRepository doorHistoryRepository)
        {
            _doorHistoryRepository = doorHistoryRepository;
        }

        public async Task Consume(ConsumeContext<AddDoorHistoryEvent> context)
        {
            var input = new AddDoorHistoryInput
            {
                UserId = context.Message.UserId,
                DoorId = context.Message.DoorId,
                EntryDateTime = context.Message.EntryDateTime,
                IsSuccessfulEntry = context.Message.IsSuccessfulEntry
            };

            await _doorHistoryRepository.AddDoorHistory(input, context.CancellationToken);
        }
    }
}
