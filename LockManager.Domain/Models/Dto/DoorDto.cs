using System.Text.Json.Serialization;
using LockManager.Domain.Entities;

namespace LockManager.Domain.Models.Dto
{
    public class DoorDto
    {
        public int Id { get; set; }
        public bool Open { get; set; }
        public Role MinimumRoleAuthorized { get; set; }
        [JsonIgnore]
        public string UnauthorizedMessage { get; set; } = string.Empty;

        public DoorDto()
        {
        }

        public DoorDto(Door door)
        {
            Id = door.Id;
            Open = door.Open;
            MinimumRoleAuthorized = door.MinimumRoleAuthorized;
        }
    }
}
