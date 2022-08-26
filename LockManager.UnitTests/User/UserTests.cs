using LockManager.Application.Handlers;
using LockManager.Application.Repositories;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Command;
using Moq;

namespace LockManager.UnitTests.User
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly CancellationToken _cancellationToken;

        public UserTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async void CreateUserCommandTest()
        {
            //Arrange
            _userRepository.Setup(x => x.CreateUser(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new CreateUserCommand
            {
                Username = "test",
                Role = Role.None
            };
            var handler = new CreateUserCommandHandler(_userRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userRepository.Verify(x => x.CreateUser(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.True(result > 0);
        }
        
        [Fact]
        public async void UpdateUserCommandTest()
        {
            //Arrange
            var user = new Domain.Entities.User
            {
                Id = 1,
                Role = Role.None,
                Username = "test",
                Active = false
            };
            _userRepository.Setup(x => x.UpdateUser(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(user));

            var command = new UpdateUserCommand
            {
                Id = 1,
                Role = Role.None,
                Active = false
            };
            var handler = new UpdateUserCommandHandler(_userRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userRepository.Verify(x => x.UpdateUser(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(result.Id, user.Id);
            Assert.Equal(result.Role, user.Role);
            Assert.Equal(result.Active, user.Active);
            Assert.Equal(result.Username, user.Username);
        }
    }
}
