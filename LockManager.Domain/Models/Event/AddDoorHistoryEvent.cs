namespace LockManager.Domain.Models.Event
{
    public class AddDoorHistoryEvent
    {
        public int UserId { get; set; }
        public bool IsSuccessfulEntry { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int DoorId { get; set; }
    }
}
