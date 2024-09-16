using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _connectionFactory;

        public MessageBusService(ConnectionFactory connectionFactory, IConfiguration configuration)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public void Publish(string queue, byte[] message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            //garantir que a fila seja criada
            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            //publicar a mensagem
            channel.BasicPublish(
                exchange: "",
                routingKey: queue,
                basicProperties: null,
                body: message
            );

        }
    }
}


