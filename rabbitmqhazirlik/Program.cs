// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare("HelloQueve", true, false, false);

        Enumerable.Range(1, 50).ToList().ForEach(x =>
        {
            
            string message = $"Selamın Aleyküm :{x}";
            var messageBody = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(string.Empty, "HelloQueve", null, messageBody);
            Console.WriteLine(message);
          
        });
       
        Console.ReadLine();

    }
}