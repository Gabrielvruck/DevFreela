using DevFreela.Core.Services;
using DevFreela.Infrastructure.MessageBus;
using RabbitMQ.Client;

namespace DevFreela.API.Extensions
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, string hostName, string userName, string password)
        {
            services.AddSingleton(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password
                };
                return factory;
            });

            services.AddScoped<IMessageBusService, MessageBusService>();
            return services;
        }
    }
}
