using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Query
{
    public class GetDoorsQuery : IRequest<IEnumerable<DoorDto>>
    {
    }
}
