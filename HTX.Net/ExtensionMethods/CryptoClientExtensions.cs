using HTX.Net.Clients;
using HTX.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the HTX REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IHTXRestClient HTX(this ICryptoRestClient baseClient) => baseClient.TryGet<IHTXRestClient>(() => new HTXRestClient());

        /// <summary>
        /// Get the HTX Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IHTXSocketClient HTX(this ICryptoSocketClient baseClient) => baseClient.TryGet<IHTXSocketClient>(() => new HTXSocketClient());
    }
}
