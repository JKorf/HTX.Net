using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXUnsubscribeQuery : Query<HTXSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXUnsubscribeQuery(string topic, bool authenticated, int weight = 1, string? dataType = null) : base(new HTXUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic, DataType = dataType }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HTXUnsubscribeRequest)Request).Id };
        }

    }
}
