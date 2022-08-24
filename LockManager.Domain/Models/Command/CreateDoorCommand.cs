using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Command
{
    public class CreateDoorCommand : IRequest<DoorDto>
    {
        public Role MinimumRoleAuthorized { get; set; }
    }
}
