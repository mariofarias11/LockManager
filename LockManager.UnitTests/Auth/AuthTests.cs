using LockManager.Application.Handlers;
using LockManager.Application.Repositories;
using LockManager.Application.Services;
using LockManager.Domain.Entities;
using LockManager.Domain.Models;
using LockManager.Domain.Models.Command;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Input;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LockManager.UnitTests.Auth
{
    public class AuthTests
    {
        private readonly Mock<IUserAuthRepository> _userAuthRepository;
        private readonly IConfiguration _configuration;
        private readonly CancellationToken _cancellationToken;
        private readonly ITokenService _tokenService;

        public AuthTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"AppSettings:Token", "lock-manager-secret!"}
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _userAuthRepository = new Mock<IUserAuthRepository>();
            _cancellationToken = new CancellationToken();
            _tokenService = new TokenService(_configuration);
        }

        [Fact]
        public async void RegisterUserCommandTest()
        {
            //Arrange
            var command = new RegisterUserCommand
            {
                Username = "test",
                Password = "test"
            };

            var handler = new RegisterUserCommandHandler(_tokenService, _userAuthRepository.Object);

            _userAuthRepository
                .Setup(x => x.CreateUserAuth(It.IsAny<CreateUserAuthInput>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userAuthRepository.Verify(x => x.CreateUserAuth(It.IsAny<CreateUserAuthInput>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.True(result);
        }

        [Fact]
        public async void LoginCommand_Success_Test()
        {
            //Arrange
            var command = new LoginCommand
            {
                Username = "test",
                Password = "test",
                User = CreateUserDto(Role.Admin)
            };

            var handler = new LoginCommandHandler(_tokenService, _userAuthRepository.Object);

            var userAuth = CreateUserAuth(command.Password);
            _userAuthRepository
                .Setup(x => x.GetUserAuthByUsername(It.IsAny<string>()))
                .Returns(Task.FromResult(userAuth));

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userAuthRepository.Verify(x => x.UpdateUserAuth(It.IsAny<UpdateUserAuthInput>(), It.IsAny<CancellationToken>()), Times.Once());
            _userAuthRepository.Verify(x => x.GetUserAuthByUsername(It.IsAny<string>()), Times.Once());
            Assert.NotNull(result);
        }

        [Fact]
        public async void LoginCommand_WrongPassword_Test()
        {
            //Arrange
            var command = new LoginCommand
            {
                Username = "test",
                Password = "test",
                User = CreateUserDto(Role.Admin)
            };

            var handler = new LoginCommandHandler(_tokenService, _userAuthRepository.Object);

            var userAuth = CreateUserAuth("password");
            _userAuthRepository
                .Setup(x => x.GetUserAuthByUsername(It.IsAny<string>()))
                .Returns(Task.FromResult(userAuth));

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userAuthRepository.Verify(x => x.UpdateUserAuth(It.IsAny<UpdateUserAuthInput>(), It.IsAny<CancellationToken>()), Times.Never());
            _userAuthRepository.Verify(x => x.GetUserAuthByUsername(It.IsAny<string>()), Times.Once());
            Assert.Null(result);
        }

        [Fact]
        public async void RefreshTokenCommand_Success_Test()
        {
            //Arrange
            var command = new RefreshTokenCommand
            {
                User = CreateUserDto(Role.Director)
            };

            var handler = new RefreshTokenCommandHandler(_tokenService, _userAuthRepository.Object);

            var userAuth = CreateUserAuth("password");
            _userAuthRepository
                .Setup(x => x.GetUserAuthByUsername(It.IsAny<string>()))
                .Returns(Task.FromResult(userAuth));

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userAuthRepository.Verify(x => x.UpdateUserAuth(It.IsAny<UpdateUserAuthInput>(), It.IsAny<CancellationToken>()), Times.Once());
            _userAuthRepository.Verify(x => x.GetUserAuthByUsername(It.IsAny<string>()), Times.Once());
            Assert.NotNull(result);
        }

        [Fact]
        public async void RefreshTokenCommand_Expired_Test()
        {
            //Arrange
            var command = new RefreshTokenCommand
            {
                User = CreateUserDto(Role.Director)
            };

            var handler = new RefreshTokenCommandHandler(_tokenService, _userAuthRepository.Object);

            var userAuth = CreateUserAuth("password");
            userAuth.TokenExpires = DateTime.Now.AddDays(-1);
            _userAuthRepository
                .Setup(x => x.GetUserAuthByUsername(It.IsAny<string>()))
                .Returns(Task.FromResult(userAuth));

            //Act
            var result = await handler.Handle(command, _cancellationToken);

            //Assert
            _userAuthRepository.Verify(x => x.UpdateUserAuth(It.IsAny<UpdateUserAuthInput>(), It.IsAny<CancellationToken>()), Times.Never());
            _userAuthRepository.Verify(x => x.GetUserAuthByUsername(It.IsAny<string>()), Times.Once());
            Assert.Null(result);
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

        private UserAuth CreateUserAuth(string password)
        {
            _tokenService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            return new UserAuth
            {
                Id = 1,
                Username = "test",
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now.AddDays(1)
            };
        }
    }
}
