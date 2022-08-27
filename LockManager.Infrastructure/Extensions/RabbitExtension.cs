using LockManager.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LockManager.Infrastructure.Extensions
{
    public static class RabbitExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbit = configuration.GetSection("AppSettings:RabbitMq");

            services.AddMassTransit(x =>
            {
                x.AddConsumers();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(rabbit["Host"], rabbit["VirtualHost"], h =>
                    {
                        h.Username(rabbit["Username"]);
                        h.Password(rabbit["Password"]);
                    });
                });
            });

            return services;
        }

        private static IBusRegistrationConfigurator AddConsumers(
            this IBusRegistrationConfigurator busRegistrationConfigurator)
        {
            busRegistrationConfigurator.AddConsumer<AddDoorHistoryEventConsumer>();
            return busRegistrationConfigurator;
        }
    }
}
