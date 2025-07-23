using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXUnsubscribeQuery : Query<HTXSocketResponse>
    {
        public HTXUnsubscribeQuery(string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<HTXSocketResponse>(((HTXUnsubscribeRequest)Request).Id);
        }
    }
}
