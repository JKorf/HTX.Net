using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXUnsubscribeQuery : Query<HTXSocketResponse>
    {
        public HTXUnsubscribeQuery(string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            MessageRouter = MessageRouter.CreateWithoutHandler<HTXSocketResponse>(((HTXUnsubscribeRequest)Request).Id);
        }
    }
}
