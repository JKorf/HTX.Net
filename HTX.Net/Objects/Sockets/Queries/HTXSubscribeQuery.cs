using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXSubscribeQuery : Query<HTXSocketResponse>
    {
        private readonly SocketApiClient _client;

        public HTXSubscribeQuery(SocketApiClient client, string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse>(((HTXSubscribeRequest)Request).Id, HandleMessage);
        }

        public CallResult<HTXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<HTXSocketResponse> message)
        {
            if (message.Data.Status != "ok")
                return new CallResult<HTXSocketResponse>(new ServerError(message.Data.ErrorCode!, _client.GetErrorInfo(message.Data.ErrorCode!, message.Data.ErrorMessage)));

            return message.ToCallResult();
        }
    }
}
