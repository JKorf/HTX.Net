using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpQuery : Query<HTXOpResponse>
    {
        private readonly SocketApiClient _client;

        public HTXOpQuery(SocketApiClient client, string topic, string op, bool authenticated, string? contractCode = null, int weight = 1) : base(CreateRequest(topic, op, contractCode), authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXOpResponse>(((HTXOpMessage)Request).RequestId!, HandleMessage);
        }

        private static HTXOpMessage CreateRequest(string topic, string op, string? contractCode)
        {
            if (contractCode == null)
                return new HTXOpMessage { RequestId = ExchangeHelpers.NextId().ToString(), Topic = topic, Operation = op };

            return new HTXOpMessageV5 { RequestId = ExchangeHelpers.NextId().ToString(), Topic = topic, Operation = op, ContractCode = contractCode };
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpResponse message)
        {
            if (message.ErrorCode == 0)
                return new CallResult<HTXOpResponse>(message, originalData, null);

            return new CallResult<HTXOpResponse>(new ServerError(message.ErrorCode!, _client.GetErrorInfo(message.ErrorCode!, message.ErrorMessage)));
        }
    }
}
