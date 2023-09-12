using RabbitMQ.Client;

namespace RabbitMqWatermark.Services
{
    public class RabbitMqClientsService:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ImageDirectExchange";
        public static string RoutingWatermark = "watermark-route-image";
        public static string queveName = "queve-watermark-image";

        private readonly ILogger<RabbitMqClientsService> _logger;

        public RabbitMqClientsService(ConnectionFactory connectionFactory,ILogger<RabbitMqClientsService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if(_channel is { IsOpen:true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName,type:"direct",true,false);
            _channel.QueueDeclare(queveName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: queveName, routingKey: RoutingWatermark);
            _logger.LogInformation("RabbitMq İle Bağlantı Kuruldu");
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ İle Bağlantı Koparıldı");

        }
    }
}
