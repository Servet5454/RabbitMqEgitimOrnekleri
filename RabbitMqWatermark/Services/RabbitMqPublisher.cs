

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMqWatermark.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientsService _rabbitMqClientsService;

        public RabbitMqPublisher(RabbitMqClientsService rabbitMqClientsService)
        {
            _rabbitMqClientsService = rabbitMqClientsService;
        }

        public void Publish(productImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitMqClientsService.Connect();
            var bodyString =JsonSerializer.Serialize(productImageCreatedEvent);
            var bodyByte =Encoding.UTF8.GetBytes(bodyString);
            var properties =channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange:RabbitMqClientsService.ExchangeName,
                routingKey:RabbitMqClientsService.RoutingWatermark, 
                basicProperties: properties, 
                body: bodyByte);

        }
    }
}
