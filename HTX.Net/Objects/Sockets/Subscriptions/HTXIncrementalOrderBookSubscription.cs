using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXIncrementalOrderBookSubscription : Subscription<HTXSocketResponse, HTXSocketResponse>
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private bool _snapshots;
        private Action<DataEvent<HTXIncrementalOrderBookUpdate>> _handler;

        public HTXIncrementalOrderBookSubscription(ILogger logger, SocketApiClient client, bool snapshots, string topic, Action<DataEvent<HTXIncrementalOrderBookUpdate>> handler) : base(logger, false)
        {
            _client = client;
            _handler = handler;
            _snapshots = snapshots;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<HTXDataEvent<HTXIncrementalOrderBookUpdate>>(topic, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXSubscribeQuery(_client, _topic, Authenticated, dataType: _snapshots ? null : "incremental");
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXUnsubscribeQuery(_topic, Authenticated, dataType: _snapshots ? null : "incremental");
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXIncrementalOrderBookUpdate> message)
        {
            _handler.Invoke(
                new DataEvent<HTXIncrementalOrderBookUpdate>(message.Data, receiveTime, originalData)
                    .WithUpdateType(message.Data.Event == "snapshot" ? SocketUpdateType.Snapshot : SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                    .WithStreamId(message.Channel)
                );

            return CallResult.SuccessResult;
        }
    }
}
