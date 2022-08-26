namespace LockManager.Domain.Models.Request
{
    public class UpdateUserRequest
    {
        public Role? Role { get; set; }
        public bool Active { get; set; } = true;
    }
}
