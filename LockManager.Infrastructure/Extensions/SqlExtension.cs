using LockManager.Infrastructure.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LockManager.Infrastructure.Extensions
{
    public static class SqlExtension
    {
        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LockManagerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")));

            return services;
        }
    }
}
