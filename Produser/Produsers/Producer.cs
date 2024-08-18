using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Notify.DTO;
using System.Reflection;

namespace Producer.Producers
{
    public class Producer
    {
        private string _exchangeType;
        private string _exchangeName;
        private string _routingKey;
        private IModel _model;
        public Producer( string exchangeName, string routingKey, IModel model)
        {
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _model = model;
            _model.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        }
        
        public void Produce(string email, string mailSubject,string mailBody)
        {
            var message = new MailRmq()  { Receivers = [email], Body = mailBody, Title = mailSubject };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            
            _model.BasicPublish(exchange: _exchangeName,
                routingKey: _routingKey,
                basicProperties: null,
                body: body);

            Console.WriteLine($"Message is sent into Default Exchange: {_exchangeName}");
        }
    }
}