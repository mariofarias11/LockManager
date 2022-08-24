using System.Security.Cryptography;
using LockManager.Domain.Auth;

namespace LockManager.Domain.Entities
{
    public class UserAuth
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public void SetRefreshToken(RefreshToken newRefreshToken)
        {
            RefreshToken = newRefreshToken.Token;
            TokenCreated = newRefreshToken.Created;
            TokenExpires = newRefreshToken.Expires;
        }

        public bool VerifyPasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}
