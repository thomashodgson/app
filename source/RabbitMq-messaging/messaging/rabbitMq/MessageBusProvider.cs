using System;
using app_config;
using messaging;
using RabbitMQ.Client;

namespace RabbitMq_messaging.messaging.rabbitMq
{
    class MessageBusProvider : IMessageBusProvider
    {
        private readonly IConfigProvider _configProvider;

        public MessageBusProvider(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public IMessageBus GetMessageBus()
        {
            var factory = new ConnectionFactory() { HostName = _configProvider.GetConfig().RabbitMqHostname };
            var connection = factory.CreateConnection();
            var model = connection.CreateModel();
            
            foreach (var exchangeName in Enum.GetValues(typeof(Event)))
            {
                model.ExchangeDeclare(exchange: exchangeName.ToString(),
                                        durable: false,
                                        type: "direct",
                                        autoDelete: false,
                                        arguments: null);
            }


            return new MessageBus(connection, model);
        }
    }
}
