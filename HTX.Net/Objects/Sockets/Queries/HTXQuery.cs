using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXQuery<T> : Query<HTXSocketResponse<T>>
    {
        public HTXQuery(string topic, bool authenticated, int weight = 1) : base(new HTXSocketRequest(ExchangeHelpers.NextId().ToString(), topic), authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse<T>>(((HTXSocketRequest)Request).Id, HandleMessage);
        }

        public CallResult<HTXSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<HTXSocketResponse<T>> message)
        {
            if (message.Data.IsSuccessful)
                return message.ToCallResult();

            return new CallResult<HTXSocketResponse<T>>(new ServerError($"{message.Data.ErrorCode}, {message.Data.ErrorMessage}"));
        }
    }
}
