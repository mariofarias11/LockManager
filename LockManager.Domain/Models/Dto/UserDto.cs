namespace LockManager.Domain.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }
    }
}
