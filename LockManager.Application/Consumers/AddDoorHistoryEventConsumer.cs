using LockManager.Domain.Models.Event;
using MassTransit;

namespace LockManager.Application.Consumers
{
    public class AddDoorHistoryEventConsumer : IConsumer<AddDoorHistoryEvent>
    {
        public Task Consume(ConsumeContext<AddDoorHistoryEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
