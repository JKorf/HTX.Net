using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXSubscribeQuery : Query<HTXSocketResponse>
    {
        public HTXSubscribeQuery(string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse>(((HTXSubscribeRequest)Request).Id, HandleMessage);
        }

        public CallResult<HTXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<HTXSocketResponse> message)
        {
            if (message.Data.Status != "ok")
                return new CallResult<HTXSocketResponse>(new ServerError(message.Data.ErrorMessage!));

            return message.ToCallResult();
        }
    }
}
