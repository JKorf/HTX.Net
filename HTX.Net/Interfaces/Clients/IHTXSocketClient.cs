using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;

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
        /// <summary>
        /// Usdt futures V5 streams
        /// </summary>
        /// <see cref="IHTXSocketClientUsdtFuturesV5Api"/>
        public IHTXSocketClientUsdtFuturesV5Api UsdtFuturesV5Api { get; }
    }
}
