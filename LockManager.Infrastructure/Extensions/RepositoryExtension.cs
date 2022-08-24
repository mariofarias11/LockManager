using LockManager.Application.Repositories;
using LockManager.Infrastructure.DB.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LockManager.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
