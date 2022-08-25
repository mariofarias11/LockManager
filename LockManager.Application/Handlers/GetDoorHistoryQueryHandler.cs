using LockManager.Application.Repositories;
using LockManager.Domain.Models.Dto;
using LockManager.Domain.Models.Query;
using MediatR;

namespace LockManager.Application.Handlers
{
    public class GetDoorHistoryQueryHandler : IRequestHandler<GetDoorHistoryQuery, IEnumerable<DoorHistoryDto>>
    {
        private readonly IDoorHistoryRepository _doorHistoryRepository;

        public GetDoorHistoryQueryHandler(IDoorHistoryRepository doorHistoryRepository)
        {
            _doorHistoryRepository = doorHistoryRepository;
        }

        public Task<IEnumerable<DoorHistoryDto>> Handle(GetDoorHistoryQuery query, CancellationToken cancellationToken)
        {
            var doorHistory = _doorHistoryRepository.GetByDoorId(query.DoorId);

            var result = doorHistory.Select(x => new DoorHistoryDto
            {
                Id = x.Id,
                DoorId = x.DoorId,
                UserId = x.UserId,
                IsSuccessfulEntry = x.IsSuccessfulEntry,
                EntryDateTime = x.EntryDateTime
            });

            return Task.FromResult(result);
        }
    }
}
