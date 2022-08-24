using LockManager.Domain.Auth;
using LockManager.Domain.Entities;

namespace LockManager.Infrastructure.Token
{
    public interface ITokenManager
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        void SetRefreshToken(UserAuth user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
