using System.Reflection;
using LockManager.Application.Handlers;
using LockManager.Application.Services;
using MediatR;

namespace LockManager.WebApi.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateUserCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateUserCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RegisterUserCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(LoginCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RefreshTokenCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateDoorCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateDoorOpennessCommandHandler).GetTypeInfo().Assembly);
            services.AddScoped<IMediator, Mediator>();

            return services;
        }
        
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
