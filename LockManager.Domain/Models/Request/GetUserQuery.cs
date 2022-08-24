using LockManager.Domain.Models.Dto;
using MediatR;

namespace LockManager.Domain.Models.Request
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string Username { get; set; }
    }
}