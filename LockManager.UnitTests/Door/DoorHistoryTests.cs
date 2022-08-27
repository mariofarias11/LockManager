using LockManager.Application.Consumers;
using LockManager.Application.Handlers;
using LockManager.Application.Repositories;
using LockManager.Domain.Entities;
using LockManager.Domain.Models.Event;
using LockManager.Domain.Models.Input;
using LockManager.Domain.Models.Query;
using MassTransit;
using Moq;

namespace LockManager.UnitTests.Door
{
    public class DoorHistoryTests
    {
        private readonly Mock<IDoorHistoryRepository> _doorHistoryRepository;

        public DoorHistoryTests()
        {
            _doorHistoryRepository = new Mock<IDoorHistoryRepository>();
        }

        [Fact]
        public async void AddDoorHistoryEventConsumerTest()
        {
            //Arrange
            var consumer = new AddDoorHistoryEventConsumer(_doorHistoryRepository.Object);

            var addDoorHistoryEvent = new AddDoorHistoryEvent
            {
                DoorId = 1,
                UserId = 1,
                IsSuccessfulEntry = true,
                EntryDateTime = DateTime.Now
            };
            var context = Mock.Of<ConsumeContext<AddDoorHistoryEvent>>(x => x.Message == addDoorHistoryEvent);

            //Act
            await consumer.Consume(context);

            //Assert
            _doorHistoryRepository.Verify(x => x.AddDoorHistory(It.IsAny<AddDoorHistoryInput>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void GetDoorHistoryQueryHandlerTest()
        {
            //Arrange
            var doorHistory = CreateRandonDoorHistories();
            _doorHistoryRepository.Setup(x => x.GetByDoorId(It.IsAny<int>())).Returns(doorHistory);

            var handler = new GetDoorHistoryQueryHandler(_doorHistoryRepository.Object);
            var query = new GetDoorHistoryQuery
            {
                DoorId = 1
            };

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            _doorHistoryRepository.Verify(x => x.GetByDoorId(It.IsAny<int>()), Times.Once);
            Assert.Equal(doorHistory.Count(), result.Count());
        }

        private IEnumerable<DoorHistory> CreateRandonDoorHistories()
        {
            var result = new List<DoorHistory>();
            var random = new Random();
            var count = random.Next(100, 100000);

            for (int i = 0; i < count; i++)
            {
                var doorHistory = new DoorHistory
                {
                    Id = i,
                    UserId = random.Next(1, 100000),
                    DoorId = random.Next(1, 100000),
                    EntryDateTime = DateTime.Now,
                    IsSuccessfulEntry = random.Next() > random.Next()
                };
                result.Add(doorHistory);
            }

            return result;
        }
    }
}
