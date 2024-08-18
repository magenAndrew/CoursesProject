using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Produser.Models;
internal class Program
{
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connection = GetRabbitConnection(configuration);
        var channel = connection.CreateModel();

        var producer = new Producer.Producers.Producer("exchange.Email", "Email", channel);
 
        producer.Produce("chuchundr@yandex.ru","Проба отправки","<h1>TEST</h1>");


        channel.Close();
        connection.Close();
    }

    private static IConnection GetRabbitConnection(IConfiguration configuration)
    {
        var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = rmqSettings.Host,
            VirtualHost = rmqSettings.VHost,
            UserName = rmqSettings.Login,
            Password = rmqSettings.Password,
        };

        return factory.CreateConnection();
    }
}
