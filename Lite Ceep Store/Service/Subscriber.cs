namespace Lite_Ceep_Store.Service
{
    internal class MessageSubscriber : IDisposable
    {
        private readonly Action<MessageSubscriber> _action;

        public Type ReceiverType { get; }
        public Type MessageType { get; }
        public MessageSubscriber(Action<MessageSubscriber> action, Type receiverType, Type messageType)
        {
            _action = action;
            ReceiverType = receiverType;
            MessageType = messageType;
        }

        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }
}
