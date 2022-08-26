namespace LockManager.Domain.Models.Input
{
    public class AddDoorHistoryInput
    {
        public int UserId { get; set; }
        public bool IsSuccessfulEntry { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int DoorId { get; set; }
    }
}
