using LockManager.Domain.Auth;
using LockManager.Domain.Models.Dto;

namespace LockManager.Application.Services
{
    public interface ITokenService
    {
        string CreateToken(UserDto user);
        RefreshToken GenerateRefreshToken();
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
