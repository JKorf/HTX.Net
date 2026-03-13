using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;

namespace HTX.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the HTX websocket API. 
    /// </summary>
    public interface IHTXSocketClient : ISocketClient<HTXCredentials>
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        /// <see cref="IHTXSocketClientSpotApi"/>
        public IHTXSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Usdt futures streams
        /// </summary>
        /// <see cref="IHTXSocketClientUsdtFuturesApi"/>
        public IHTXSocketClientUsdtFuturesApi UsdtFuturesApi { get; }
    }
}