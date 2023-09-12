// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System.Text;
using System.Text.Json;

internal class Program //Consumer
{
    private static void Main(string[] args)
    {
        #region 1. RabbitMq Ön Hazırlık
        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();
        //// channel.QueueDeclare("HelloQueve", true, false, false);


        //channel.BasicQos(0, 1, false); //burada consumer lara  1 er 1 er dağıttık...
        //var consumer = new EventingBasicConsumer(channel);
        //channel.BasicConsume("HelloQueve", false, consumer);
        //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
        //{
        //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
        //    Thread.Sleep(1000);
        //    Console.WriteLine(message);
        //    channel.BasicAck(e.DeliveryTag, false);
        //};

        //Console.ReadLine();
        #endregion

        #region 2. Fanaout Exchange Davranışı

        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();
        ////var randomquevename = channel.QueueDeclare().QueueName;
        //var randomquevename = "channel.QueueDeclare().QueueName";
        //channel.QueueDeclare(randomquevename, true, false, false);
        //channel.QueueBind(randomquevename, "Log-FanaoutExchange", "", null);

        //channel.BasicQos(0, 1, false);
        //var consumer =new EventingBasicConsumer(channel);
        //channel.BasicConsume(randomquevename,false, consumer);
        //Console.WriteLine("Loglanıyor");
        //consumer.Received += (object sender, BasicDeliverEventArgs e) =>
        //{
        //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
        //    Thread.Sleep(1000);
        //    Console.WriteLine(message);
        //    channel.BasicAck(e.DeliveryTag, false);
        //};

        //Console.ReadLine();
        #endregion

        #region 3. direct Exchange

        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();


        ////var randomquevename = channel.QueueDeclare().QueueName;



        //channel.BasicQos(0, 1, false);
        //var consumer = new EventingBasicConsumer(channel);
        //var queveName = "Direct-queveCritical";
        ////var queveInfo = "Direct-queveInfo";
        //channel.BasicConsume(queveName, false, consumer);
        //Console.WriteLine("Loglanıyor dinleniyor...");
        //consumer.Received += (object sender, BasicDeliverEventArgs e) =>
        //{
        //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Gelen Mesaj :" + message +e.Body.ToString());
        //    File.AppendAllText("Log-critical.txt", message +"\n");
        //    channel.BasicAck(e.DeliveryTag, false);
        //};

        //Console.ReadLine();

        #endregion

        #region 4. Topic Exchange

        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();


        ////var randomquevename = channel.QueueDeclare().QueueName;



        //channel.BasicQos(0, 1, false);
        //var consumer = new EventingBasicConsumer(channel);
        //var queveName =channel.QueueDeclare().QueueName;

        //var routeKey = "Info.#";

        //channel.QueueBind(queveName, "Topic-Exchange1", routeKey);

        //channel.BasicConsume(queveName, false, consumer);
        //Console.WriteLine("Loglanıyor dinleniyor...");
        //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
        //{
        //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
        //    Thread.Sleep(1000);
        //    Console.WriteLine($"Gelen Mesaj :{message}");

        //    channel.BasicAck(e.DeliveryTag, false);
        //};

        //Console.ReadLine();

        #endregion

        #region 5. Header Exchange

        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();


        ////var randomquevename = channel.QueueDeclare().QueueName;



        //channel.BasicQos(0, 1, false);
        //var consumer = new EventingBasicConsumer(channel);

        //var queveName = channel.QueueDeclare().QueueName;

        //Dictionary<string,object> headers = new Dictionary<string, object>();
        //headers.Add("format", "pdf");
        //headers.Add("shape", "a4");
        //headers.Add("x-match", "all"); // burası all olduğu için hepsi uyması gerekmektedir... all yerine any yaparsak herhangi biri uyarsa işlemi yapmaktadır

        //channel.QueueBind(queveName, "header-Exchange",string.Empty,headers);

        //channel.BasicConsume(queveName, false, consumer);
        //Console.WriteLine("Loglanıyor dinleniyor...");
        //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
        //{
        //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
        //    Thread.Sleep(1000);
        //    Console.WriteLine($"Gelen Mesaj :{message}");

        //    channel.BasicAck(e.DeliveryTag, false);
        //};

        //Console.ReadLine();

        #endregion

        #region 6. Complex Typler İle Veri Gönderme

        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();


        //var randomquevename = channel.QueueDeclare().QueueName;



        channel.BasicQos(0, 1, false);
        var consumer = new EventingBasicConsumer(channel);

        var queveName = channel.QueueDeclare().QueueName;

        Dictionary<string, object> headers = new Dictionary<string, object>();
        headers.Add("format", "pdf");
        headers.Add("shape", "a4");
        headers.Add("x-match", "all"); // burası all olduğu için hepsi uyması gerekmektedir... all yerine any yaparsak herhangi biri uyarsa işlemi yapmaktadır

        channel.QueueBind(queveName, "header-Exchange", string.Empty, headers);

        channel.BasicConsume(queveName, false, consumer);
        Console.WriteLine("Loglanıyor dinleniyor...");
        consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            Product product =JsonSerializer.Deserialize<Product>(message);
            Thread.Sleep(1000);
            Console.WriteLine($"Gelen Mesaj :{product.Id}-{product.Name}-{product.Price}-{product.Description}");

            channel.BasicAck(e.DeliveryTag, false);
        };

        Console.ReadLine();

        #endregion

    }


}