using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAccountSubscription : Subscription<HTXSocketAuthResponse, HTXSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HTXAccountUpdate>> _handler;

        public HTXAccountSubscription(ILogger logger, string topic, Action<DataEvent<HTXAccountUpdate>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<HTXDataEvent<HTXAccountUpdate>>(topic, DoHandleMessage);
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery("sub", _topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXAuthQuery("unsub", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXAccountUpdate>> message)
        {
            _handler.Invoke(message.As(message.Data.Data, message.Data.Channel, null, SocketUpdateType.Update).WithDataTimestamp(message.Data.Data.ChangeTime));
            return CallResult.SuccessResult;
        }
    }
}
