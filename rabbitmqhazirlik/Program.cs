// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;

internal class Program //Publisher
{   
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

        var factory =new ConnectionFactory();
        factory.Uri = new Uri("amqps://kzylzeod:w54vPXXqZXhIMKETehruwfRirfxpOZc-@moose.rmq.cloudamqp.com/kzylzeod");
        using var connection = factory.CreateConnection();
        var channel =connection.CreateModel();
        channel.ExchangeDeclare("Log-FanaoutExchange", durable: true, type: ExchangeType.Fanout); //durable true diyoruz çünkü uygulama kapatılırsa exchange kaybolmasın istiyorum...


        Enumerable.Range(1, 50).ToList().ForEach(x =>
        {

            string message = $"Fanaut Exchange mesajları kuyruk ayırt etmeksizin kaçtane kuyruk varsa hepsine aynı mesajı iletir :{x}";
            var messageBody = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("Log-FanaoutExchange","", null, messageBody); //burada kuyruk declare etmiyoruz o yüzden çift tırnak ile boş bırakıyoruz çünkü fanaout kendisine bağlanan her kuyruğa ayırt etmeksizin aynı mesajı atacak...
            Console.WriteLine(message);

        });
        #endregion


    }
}