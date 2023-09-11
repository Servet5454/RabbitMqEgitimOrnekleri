// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

internal class Program //Publisher
{
    public enum LogNames
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
    private static void Main(string[] args)
    {
        #region 1. RabbitMq Ön Hazırlık Consumer

        //var factory = new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");

        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();
        //channel.QueueDeclare("HelloQueve", true, false, false);

        //Enumerable.Range(1, 50).ToList().ForEach(x =>
        //{

        //    string message = $"Selamın Aleyküm :{x}";
        //    var messageBody = Encoding.UTF8.GetBytes(message);
        //    channel.BasicPublish(string.Empty, "HelloQueve", null, messageBody);
        //    Console.WriteLine(message);

        //});

        //Console.ReadLine();
        #endregion

        #region 2. Fanaout Exchange Davranışı
        // burada senoryada biz exchange oluşturuyoruz kuyruk oluşturmuyoruz, böylelikle consumne edecek taraf kendi kuyruğunu oluşturup buradan direk bilgi alabilecekler....

        //var factory =new ConnectionFactory();
        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");
        //using var connection = factory.CreateConnection();
        //var channel =connection.CreateModel();
        //channel.ExchangeDeclare("Log-FanaoutExchange", durable: true, type: ExchangeType.Fanout); //durable true diyoruz çünkü uygulama kapatılırsa exchange kaybolmasın istiyorum...


        //Enumerable.Range(1, 50).ToList().ForEach(x =>
        //{

        //    string message = $"Fanaut Exchange mesajları kuyruk ayırt etmeksizin kaçtane kuyruk varsa hepsine aynı mesajı iletir :{x}";
        //    var messageBody = Encoding.UTF8.GetBytes(message);
        //    channel.BasicPublish("Log-FanaoutExchange","", null, messageBody); //burada kuyruk declare etmiyoruz o yüzden çift tırnak ile boş bırakıyoruz çünkü fanaout kendisine bağlanan her kuyruğa ayırt etmeksizin aynı mesajı atacak...
        //    Console.WriteLine(message);

        //});
        #endregion

        #region 3. direct Exchange


        //var factory = new ConnectionFactory();

        //factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");
        //using var connection = factory.CreateConnection();
        //var channel = connection.CreateModel();

        //channel.ExchangeDeclare("Direct-Exchange", durable: true, type: ExchangeType.Direct);


        //Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
        //{
        //    var routeKey = $"route-{x}";

        //    var queveName =$"Direct-queve{x}";
        //    channel.QueueDeclare(queveName,durable:true,exclusive:false,autoDelete:false);
        //    channel.QueueBind(queveName, "Direct-Exchange",routeKey,null);
        //});

        //Enumerable.Range(1, 50).ToList().ForEach(x =>
        //{
        //    LogNames log =(LogNames)new Random().Next(1,5);
        //    string message = $"Direct Exchange mesajı Tipi :{log}";
        //    var messagebody = Encoding.UTF8.GetBytes(message);
        //    var routeKey =$"route-{log}";
        //    channel.BasicPublish("Direct-Exchange", routeKey, false);
        //    Console.WriteLine($"Messajlar Gönderiliyor  :{log}");
        //});

        #endregion

        #region 4. Topic Exchange


        var factory = new ConnectionFactory();

        factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");
        using var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare("Topic-Exchange1", durable: true, type: ExchangeType.Topic);



        Random rnd = new Random();
        Enumerable.Range(1, 50).ToList().ForEach(x =>
        {
            LogNames log1 = (LogNames)rnd.Next(1, 5);
            LogNames log2 = (LogNames)rnd.Next(1, 5);
            LogNames log3 = (LogNames)rnd.Next(1, 5);

            var routeKey = $"{log1}.{log2}.{log3}";

            string message = $"log-type: {log1}-{log2}-{log3}";

            var messagebody = Encoding.UTF8.GetBytes(message);


            channel.BasicPublish("Topic-Exchange1", routeKey, false,body:messagebody);
            Console.WriteLine($"Messajlar Gönderiliyor  :{message}");
        });

        Console.ReadLine();

        #endregion
    }
}