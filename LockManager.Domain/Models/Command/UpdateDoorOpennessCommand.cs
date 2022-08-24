using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class UpdateDoorOpennessCommand : IRequest<DoorDto>
    {
        public int Id { get; set; }
        public bool Open { get; set; } = false;
        public UserDto User { get; set; }
    }
}
