using System.ComponentModel.DataAnnotations.Schema;

namespace LockManager.Domain.Entities
{
    public class DoorHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsSuccessfulEntry { get; set; }
        public DateTime EntryDateTime { get; set; }
        [ForeignKey("DoorId")]
        public int DoorId { get; set; }
        public virtual Door Door { get; set; }
    }
}
