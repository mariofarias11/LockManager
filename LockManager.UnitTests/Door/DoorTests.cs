using LockManager.Application.Handlers;
using LockManager.Application.Repositories;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Input;
using Moq;

namespace LockManager.UnitTests.Door
{
    public class DoorTests
    {
        private readonly Mock<IDoorRepository> _doorRepository;
        private readonly CancellationToken _cancellationToken;

        public DoorTests()
        {
            _doorRepository = new Mock<IDoorRepository>();
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        public async void CreateDoorCommandTest()
        {
            //Arrange
            var door = new Domain.Entities.Door
            {
                Id = 1,
                MinimumRoleAuthorized = Role.Employee,
                Open = false
            };
            _doorRepository.Setup(x => x.CreateDoor(It.IsAny<Role>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(door));

            var command = new CreateDoorCommand
            {
                MinimumRoleAuthorized = Role.Employee
            };
            var handler = new CreateDoorCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _doorRepository.Verify(x => x.CreateDoor(It.IsAny<Role>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(result.Id, door.Id);
            Assert.Equal(result.MinimumRoleAuthorized, door.MinimumRoleAuthorized);
            Assert.Equal(result.Open, door.Open);
        }

        [Fact]
        public async void UpdateDoorOpenness_Success_Test()
        {
            //Arrange
            var door = CreateDoor(Role.Employee, false);
            _doorRepository.Setup(x => x.GetDoorById(It.IsAny<int>()))
                .Returns(Task.FromResult(door));

            var updatedDoor = CreateDoor(Role.Employee, true);
            _doorRepository.Setup(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(updatedDoor));

            var command = new UpdateDoorOpennessCommand
            {
                Open = true,
                User = CreateUserDto(Role.Manager)
            };
            var handler = new UpdateDoorOpennessCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _doorRepository.Verify(x => x.GetDoorById(It.IsAny<int>()), Times.Once());
            _doorRepository.Verify(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(result.Id, door.Id);
            Assert.Equal(result.MinimumRoleAuthorized, door.MinimumRoleAuthorized);
            Assert.Equal(result.Open, updatedDoor.Open);
        }

        [Fact]
        public async void UpdateDoorOpenness_UnauthorizedUser_Test()
        {
            //Arrange
            var door = CreateDoor(Role.Director, false);
            _doorRepository.Setup(x => x.GetDoorById(It.IsAny<int>()))
                .Returns(Task.FromResult(door));

            var command = new UpdateDoorOpennessCommand
            {
                Open = true,
                User = CreateUserDto(Role.Employee)
            };
            var handler = new UpdateDoorOpennessCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            Assert.Equal($"User {command.User.Id} do not have permission to open door {command.Id}", result.UnauthorizedMessage);
            Assert.False(result.Open);
            Assert.Equal(Role.None, result.MinimumRoleAuthorized);
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public async void UpdateDoorOpenness_DoorNotFound_Test()
        {
            //Arrange
            Domain.Entities.Door door = null;
            _doorRepository.Setup(x => x.GetDoorById(It.IsAny<int>()))
                .Returns(Task.FromResult(door));

            var command = new UpdateDoorOpennessCommand
            {
                Open = true,
                User = CreateUserDto(Role.Manager)
            };
            var handler = new UpdateDoorOpennessCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _doorRepository.Verify(x => x.GetDoorById(It.IsAny<int>()), Times.Once());
            _doorRepository.Verify(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()), Times.Never());
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateMinimumRoleAuthorizedToOpenDoor_Success_Test()
        {
            //Arrange
            var door = CreateDoor(Role.Employee, false);
            _doorRepository.Setup(x => x.GetDoorById(It.IsAny<int>()))
                .Returns(Task.FromResult(door));

            var updatedDoor = CreateDoor(Role.Manager, door.Open);
            _doorRepository.Setup(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(updatedDoor));

            var command = new UpdateMinimumRoleAuthorizedToOpenDoorCommand
            {
                MinimumRoleAuthorized = Role.Manager
            };
            var handler = new UpdateMinimumRoleAuthorizedToOpenDoorCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _doorRepository.Verify(x => x.GetDoorById(It.IsAny<int>()), Times.Once());
            _doorRepository.Verify(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(result.Id, door.Id);
            Assert.Equal(result.MinimumRoleAuthorized, updatedDoor.MinimumRoleAuthorized);
            Assert.Equal(result.Open, door.Open);
        }

        [Fact]
        public async void UpdateMinimumRoleAuthorizedToOpenDoor_DoorNotFound_Test()
        {
            //Arrange
            Domain.Entities.Door door = null;
            _doorRepository.Setup(x => x.GetDoorById(It.IsAny<int>()))
                .Returns(Task.FromResult(door));

            var command = new UpdateMinimumRoleAuthorizedToOpenDoorCommand
            {
                MinimumRoleAuthorized = Role.Director
            };
            var handler = new UpdateMinimumRoleAuthorizedToOpenDoorCommandHandler(_doorRepository.Object);

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _doorRepository.Verify(x => x.GetDoorById(It.IsAny<int>()), Times.Once());
            _doorRepository.Verify(x => x.UpdateDoor(It.IsAny<UpdateDoorInput>(), It.IsAny<CancellationToken>()), Times.Never());
            Assert.Null(result);
        }

        private Domain.Entities.Door CreateDoor(Role role, bool open)
        {
            return new Domain.Entities.Door
            {
                Id = 1,
                MinimumRoleAuthorized = role,
                Open = open
            };
        }

        private UserDto CreateUserDto(Role role)
        {
            return new UserDto
            {
                Active = true,
                Role = role,
                Id = 1,
                Username = "test"
            };
        }
    }
}
