using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAccountSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<HTXAccountUpdate>> _handler;

        public HTXAccountSubscription(ILogger logger, SocketApiClient client, string topic, Action<DataEvent<HTXAccountUpdate>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXDataEvent<HTXAccountUpdate>>(topic, DoHandleMessage);
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
            if (message.Data.ChangeTime != null)
                _client.UpdateTimeOffset(message.Data.ChangeTime.Value);

            _handler.Invoke(
                new DataEvent<HTXAccountUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithDataTimestamp(message.Data.ChangeTime, _client.GetTimeOffset()));

            return CallResult.SuccessResult;
        }
    }
}
