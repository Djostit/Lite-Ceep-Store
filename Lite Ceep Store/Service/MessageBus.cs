namespace Lite_Ceep_Store.Service
{
    public class MessageBus
    {
        private readonly ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> _consumers;
        public MessageBus()
        {
            _consumers = new ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>>();
        }
        public async Task SendTo<TReceiver>(IMessage message)
        {
            var messageType = message.GetType();
            var receiverType = typeof(TReceiver);

            var tasks = _consumers
                .Where(s => s.Key.MessageType == messageType
                       && s.Key.ReceiverType == receiverType)
                .Select(s => s.Value(message));

            await Task.WhenAll(tasks);
        }
        public IDisposable Receive<TMessage>(object receiver, Func<TMessage, Task> handler) where TMessage : IMessage
        {
            var sub = new MessageSubscriber(s => _consumers.TryRemove(s, out var _), receiver.GetType(), typeof(TMessage));

            _consumers.TryAdd(sub, (@event) => handler((TMessage)@event));

            return sub;
        }
    }
}
