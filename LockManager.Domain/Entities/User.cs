using LockManager.Domain.Models;

namespace LockManager.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
    }
}
