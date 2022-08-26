using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class UpdateMinimumRoleAuthorizedToOpenDoorCommand : IRequest<DoorDto>
    {
        public int Id { get; set; }
        public Role MinimumRoleAuthorized { get; set; }
    }
}
