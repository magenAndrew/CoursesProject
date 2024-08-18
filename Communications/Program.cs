using Communication.Settings;
using Communications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
        var appSettings = configuration.Get<ApplicationSettings>();
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IMailSender, MailSender>()
            .BuildServiceProvider();

        var consumerNumber = appSettings.MailConsumers;

        var connection = GetRabbitConnection(configuration);
        var channel = connection.CreateModel();

        MailConsumer.Register(channel, "exchange.Email", "query.SendEmail", "Email", serviceProvider.GetService<IMailSender>(), appSettings.MailServerSettings);
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
        IConnection conn = factory.CreateConnection();
        return conn;
    }
}