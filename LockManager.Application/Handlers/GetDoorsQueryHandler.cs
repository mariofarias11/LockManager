using LockManager.Application.Repositories;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Query;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class GetDoorsQueryHandler : IRequestHandler<GetDoorsQuery, IEnumerable<DoorDto>>
    {
        private readonly IDoorRepository _doorRepository;

        public GetDoorsQueryHandler(IDoorRepository doorRepository)
        {
            _doorRepository = doorRepository;
        }

        public async Task<IEnumerable<DoorDto>> Handle(GetDoorsQuery request, CancellationToken cancellationToken)
        {
            var doors = await _doorRepository.GetDoors();

            var result = doors.Select(x => new DoorDto(x));
            return result;
        }
    }
}
