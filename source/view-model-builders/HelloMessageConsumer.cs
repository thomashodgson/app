using messaging;
using Serilog;

namespace view_model_builders
{
    class HelloMessageConsumer : IMessageConsumer
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger _log;

        public HelloMessageConsumer(IMessageBus messageBus, ILogger log)
        {
            _messageBus = messageBus;
            _log = log;
        }

        public void Start()
        {
            _messageBus.GetIncomingMessageObservable(MessageType.HelloRequested).Subscribe(new MessageObserver(ResolveRepositoryPath, _messageBus, _log));
        }

        private void ResolveRepositoryPath(Message message)
        {
            var model = message.Data;
            model.ScriptsFolderPath = _fileSystem.GetRepositoryFolderPath(message.User, model.Repository.Name);
            _messageBus.Send(MessageType.RepositoryPathResolved, message);
        }
    }
}
