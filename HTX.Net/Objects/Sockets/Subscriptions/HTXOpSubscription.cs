using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpSubscription<T> : Subscription<HTXOpResponse, HTXOpResponse> where T: HTXOpMessage
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public HTXOpSubscription(ILogger logger, SocketApiClient client, string listenId, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;
            MessageMatcher = MessageMatcher.Create<T>(listenId, DoHandleMessage);
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXOpQuery(_client, _topic, "sub", Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXOpQuery(_client, _topic, "unsub", Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<T> message)
        {
            _handler.Invoke(message.As(message.Data)
                .WithUpdateType(SocketUpdateType.Update)
                .WithStreamId(message.Data.Topic));
            return CallResult.SuccessResult;
        }
    }
}
