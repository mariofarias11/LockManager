namespace LockManager.Domain.Models.Dto
{
    public class DoorHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsSuccessfulEntry { get; set; }
        public DateTime EntryDateTime { get; set; }
        public int DoorId { get; set; }
    }
}
