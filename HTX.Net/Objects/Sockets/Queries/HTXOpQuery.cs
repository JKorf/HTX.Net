using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpQuery : Query<HTXOpResponse>
    {
        private readonly SocketApiClient _client;

        public HTXOpQuery(SocketApiClient client, string topic, string op, bool authenticated, int weight = 1) : base(new HTXOpMessage { RequestId = ExchangeHelpers.NextId().ToString(), Topic = topic, Operation = op }, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXOpResponse>(((HTXOpMessage)Request).RequestId!, HandleMessage);
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpResponse message)
        {
            if (message.ErrorCode == 0)
                return new CallResult<HTXOpResponse>(message, originalData, null);

            return new CallResult<HTXOpResponse>(new ServerError(message.ErrorCode!, _client.GetErrorInfo(message.ErrorCode!, message.ErrorMessage)));
        }
    }
}
