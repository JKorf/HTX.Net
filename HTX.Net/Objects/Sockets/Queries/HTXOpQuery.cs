using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpQuery : Query<HTXOpResponse>
    {
        public HTXOpQuery(string topic, string op, bool authenticated, int weight = 1) : base(new HTXOpMessage { RequestId = ExchangeHelpers.NextId().ToString(), Topic = topic, Operation = op }, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<HTXOpResponse>(((HTXOpMessage)Request).RequestId!, HandleMessage);
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DataEvent<HTXOpResponse> message)
        {
            if (message.Data.ErrorCode == 0)
                return message.ToCallResult(message.Data);

            return new CallResult<HTXOpResponse>(new ServerError(message.Data.ErrorCode!, message.Data.ErrorMessage));
        }
    }
}
