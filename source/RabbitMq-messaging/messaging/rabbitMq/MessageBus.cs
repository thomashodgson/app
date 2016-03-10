using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq_messaging.messaging.rabbitMq
{
    class MessageBus : IMessageBus, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;


        public MessageBus(IConnection connection, IModel model)
        {
            _connection = connection;
            _model = model;
        }

        public void Send(Event @event, Message message)
        {
            var serializedMessage = SerializeMessage(message);
           _model.BasicPublish(@event.ToString(), message.RequestUrl, null, serializedMessage);
        }

        private static byte[] SerializeMessage(Message message)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        public IObservable<Message> GetIncomingMessageObservable(Event @event, string requestUrl)
        {
            var consumer = new EventingBasicConsumer(_model);
            var noAck = false;

            var queueName = _model.QueueDeclare().QueueName;

            _model.BasicConsume(queueName, noAck, consumer);
            _model.QueueBind(queueName, @event.ToString(), requestUrl);
            var subject = new Subject<Message>();

            consumer.Received += (model, ea) =>
            {
                subject.OnNext(JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(ea.Body)));
                _model.BasicAck(ea.DeliveryTag, multiple:false);
            };

            return subject;
        }

        public IObservable<Message> GetIncomingMessageObservable(Event @event, IEnumerable<string> requestUrls)
        {
            var observables = requestUrls.Select(x => GetIncomingMessageObservable(@event, x));
            return observables.Merge();
        }

        public void Dispose()
        {
            _connection.Dispose();
            _model.Dispose();
        }
    }
}
