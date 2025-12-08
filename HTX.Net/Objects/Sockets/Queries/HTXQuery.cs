using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Objects.Internal;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXQuery<T> : Query<HTXSocketResponse<T>>
    {
        private readonly SocketApiClient _client;

        public HTXQuery(SocketApiClient client, string topic, bool authenticated, int weight = 1) : base(new HTXSocketRequest(ExchangeHelpers.NextId().ToString(), topic), authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse<T>>(((HTXSocketRequest)Request).Id, HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXSocketResponse<T>>(((HTXSocketRequest)Request).Id, HandleMessage);
        }

        public CallResult<HTXSocketResponse<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXSocketResponse<T> message)
        {
            if (message.IsSuccessful)
                return new CallResult<HTXSocketResponse<T>>(message, originalData, null);

            return new CallResult<HTXSocketResponse<T>>(new ServerError(message.ErrorCode!, _client.GetErrorInfo(message.ErrorCode!, message.ErrorMessage)));
        }
    }
}
