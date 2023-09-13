using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCreateWorkerService.Services
{
    public class RabbitMqClientsService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string queveName = "queve-excel-file";

        private readonly ILogger<RabbitMqClientsService> _logger;

        public RabbitMqClientsService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientsService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
           
            
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
