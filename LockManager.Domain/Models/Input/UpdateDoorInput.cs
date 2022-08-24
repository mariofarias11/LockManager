namespace LockManager.Domain.Models.Input
{
    public class UpdateDoorInput
    {
        public int Id { get; set; }
        public bool Open { get; set; } = false;
        public Role MinimumRoleAuthorized { get; set; }
    }
}
