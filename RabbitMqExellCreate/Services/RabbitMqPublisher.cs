using RabbitMQ.Client;
using Shared;
using System.Text;
using System.Text.Json;

namespace RabbitMqExellCreate.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientsService _rabbitMqClientsService;

        public RabbitMqPublisher(RabbitMqClientsService rabbitMqClientsService)
        {
            _rabbitMqClientsService = rabbitMqClientsService;
        }

        public void Publish(CreateExcelMessage createExcelMessage)
        {
            var channel = _rabbitMqClientsService.Connect();
            var bodyString = JsonSerializer.Serialize(createExcelMessage);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: RabbitMqClientsService.ExchangeName,
                routingKey: RabbitMqClientsService.RoutingExell,
                basicProperties: properties,
                body: bodyByte);

        }
    }
}
