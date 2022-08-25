using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Query
{
    public class GetDoorHistoryQuery : IRequest<IEnumerable<DoorHistoryDto>>
    {
        public int DoorId { get; set; }
    }
}
