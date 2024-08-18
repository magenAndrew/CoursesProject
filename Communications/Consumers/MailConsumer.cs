using System;
using System.Text;
using System.Text.Json;
using Notify.DTO;
using Communications.Models;
using Communications.Settings;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Communications
{
    public static class MailConsumer
    {
        public static void Register(IModel channel, string exchangeName, string queueName, string routingKey,IMailSender mailSender, MailSettings mailSettings)
        {
            channel.BasicQos(0, 10, false);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var message = JsonSerializer.Deserialize<MailRmq>(Encoding.UTF8.GetString(body.ToArray()));
                mailSender.SendEmail(message, mailSettings);
                channel.BasicAck(e.DeliveryTag, false);                
            };

            channel.BasicConsume(queueName, false, consumer);
            Console.WriteLine($"Subscribed to the queue with key {routingKey} (exchange name: {exchangeName})");
            Console.ReadLine();
        }
    }
}
