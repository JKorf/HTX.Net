using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAccountSubscription : Subscription<HTXSocketAuthResponse, HTXSocketAuthResponse>
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<HTXAccountUpdate>> _handler;

        public HTXAccountSubscription(ILogger logger, SocketApiClient client, string topic, Action<DataEvent<HTXAccountUpdate>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<HTXDataEvent<HTXAccountUpdate>>(topic, DoHandleMessage);
            MessageRouter = MessageRouter.Create<HTXDataEvent<HTXAccountUpdate>>(topic, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "sub", _topic, Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "unsub", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXAccountUpdate> message)
        {
            _handler.Invoke(
                new DataEvent<HTXAccountUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithDataTimestamp(message.Data.ChangeTime));

            return CallResult.SuccessResult;
        }
    }
}
