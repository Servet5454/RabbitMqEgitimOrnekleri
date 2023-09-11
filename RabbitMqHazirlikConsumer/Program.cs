// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        // channel.QueueDeclare("HelloQueve", true, false, false);


        channel.BasicQos(0, 1, false); //burada consumer lara  1 er 1 er dağıttık...
        var consumer =new EventingBasicConsumer(channel);
        channel.BasicConsume("HelloQueve", false, consumer);
        consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
        {
            var message =Encoding.UTF8.GetString(e.Body.ToArray());
            Thread.Sleep(1000);
            Console.WriteLine(message);
            channel.BasicAck(e.DeliveryTag, false);
        };

        Console.ReadLine();

    }

  
}