namespace LockManager.Domain.Models.Dto
{
    public class UpdateDoorDto
    {
        public bool? Open { get; set; } = false;
        public Role? MinimumRoleAuthorized { get; set; }
    }
}
