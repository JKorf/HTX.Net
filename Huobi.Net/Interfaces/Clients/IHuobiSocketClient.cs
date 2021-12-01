using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.SpotApi;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Interface for the Huobi socket client
    /// </summary>
    public interface IHuobiSocketClient : ISocketClient
    {
        public IHuobiSocketClientSpotStreams SpotStreams { get; }

    }
}