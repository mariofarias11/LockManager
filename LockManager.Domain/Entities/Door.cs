using LockManager.Domain.Models;

namespace LockManager.Domain.Entities
{
    public class Door
    {
        public int Id { get; set; }
        public bool Open { get; set; }
        public Role MinimumRoleAuthorized { get; set; }
        public virtual ICollection<DoorHistory> DoorHistoryList { get; set; }
    }
}
