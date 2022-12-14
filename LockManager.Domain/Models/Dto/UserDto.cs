using LockManager.Domain.Entities;

namespace LockManager.Domain.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }

        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Role = user.Role;
            Active = user.Active;   
            Username = user.Username;
        }
    }
}
