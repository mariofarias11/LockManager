using LockManager.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace LockManager.Infrastructure.Extensions
{
    public static class RabbitExtension
    {
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
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
