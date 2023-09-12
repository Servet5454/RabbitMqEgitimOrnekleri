using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqWatermark.Services;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace RabbitMqWatermark.Backgroundservices
{
    public class ImageWatermarkProcessBackgroundService : BackgroundService
    {
        private readonly RabbitMqClientsService _rabbitMqClientsService;
        private readonly ILogger<ImageWatermarkProcessBackgroundService> _logger;
        private IModel _channel;

        public ImageWatermarkProcessBackgroundService(RabbitMqClientsService rabbitMqClientsService, ILogger<ImageWatermarkProcessBackgroundService> logger)
        {
            _rabbitMqClientsService = rabbitMqClientsService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {


            _channel =_rabbitMqClientsService.Connect();

            _channel.BasicQos(0, 1, false);

            
            return base.StartAsync(cancellationToken);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer =new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMqClientsService.queveName,
                false,
                consumer);

            consumer.Received += Consumer_Received;



            return Task.CompletedTask;

        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var imageCreatedEvent = JsonSerializer.Deserialize<productImageCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageCreatedEvent.ImageName);
                var siteName = "www.Servet.Zabun.com";

                using var img = Image.FromFile(path);

                using var graphic = Graphics.FromImage(img);

                var font = new Font(FontFamily.GenericMonospace, 32, FontStyle.Bold, GraphicsUnit.Pixel);

                var textSize = graphic.MeasureString(siteName, font);

                var color = Color.FromArgb(255, 255, 255, 255);

                var brush = new SolidBrush(color);

                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

                graphic.DrawString(siteName, font, brush, position);


                img.Save("wwwroot/Images/watermarks/" + imageCreatedEvent.ImageName);
                img.Dispose();
                graphic.Dispose();
                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception)
            {

                throw;
            }

            return Task.CompletedTask;
            



        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
