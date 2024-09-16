using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IModel _channel;
        //IConfiguration pegar os dados do appsettings user e senha 
        public MessageBusService(IConfiguration configuration, ConnectionFactory factory, IModel channel)
        {
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public void Publish(string queue, byte[] message)
        {
            //garantir que a fila seja criada
            _channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            //publicar a mensagem
            _channel.BasicPublish(
                exchange: "",
                routingKey: queue,
                basicProperties: null,
                body: message
            );

        }
    }
}


