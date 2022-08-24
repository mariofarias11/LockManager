using System.Reflection;
using LockManager.Application.Handlers;
using MediatR;

namespace LockManager.WebApi.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateUserCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<IMediator, Mediator>();

            return services;
        }
    }
}
