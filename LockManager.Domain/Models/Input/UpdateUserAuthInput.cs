using LockManager.Domain.Entities;

namespace LockManager.Domain.Models.Input
{
    public class UpdateUserAuthInput
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public UpdateUserAuthInput(UserAuth userAuth)
        {
            Id = userAuth.Id;
            Username = userAuth.Username;
            PasswordHash = userAuth.PasswordHash;
            PasswordSalt = userAuth.PasswordSalt;
            TokenCreated = userAuth.TokenCreated;
            TokenExpires = userAuth.TokenExpires;
        }
    }
}
