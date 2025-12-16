using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXSubscribeQuery : Query<HTXSocketResponse>
    {
        private readonly SocketApiClient _client;

        public HTXSubscribeQuery(SocketApiClient client, string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse>(((HTXSubscribeRequest)Request).Id, HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXSocketResponse>(((HTXSubscribeRequest)Request).Id, HandleMessage);
        }

        public CallResult<HTXSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXSocketResponse message)
        {
            if (message.Status != "ok")
                return new CallResult<HTXSocketResponse>(new ServerError(message.ErrorCode!, _client.GetErrorInfo(message.ErrorCode!, message.ErrorMessage)));

            return new CallResult<HTXSocketResponse>(message, originalData, null);
        }
    }
}
