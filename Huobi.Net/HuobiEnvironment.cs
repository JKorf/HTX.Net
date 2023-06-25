using CryptoExchange.Net.Objects;
using Huobi.Net.Objects;

namespace Huobi.Net
{
    /// <summary>
    /// Huobi environments
    /// </summary>
    public class HuobiEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest client address
        /// </summary>
        public string RestBaseAddress { get; }

        /// <summary>
        /// Spot Rest client address for USDT margin swap API
        /// </summary>
        public string UsdtMarginSwapRestBaseAddress { get; }

        /// <summary>
        /// Base address for socket API
        /// </summary>
        public string SocketBaseAddress { get; }

        /// <summary>
        /// Socket base address for the USDT margin swap API
        /// </summary>
        public string UsdtMarginSwapSocketBaseAddress { get; }

        internal HuobiEnvironment(string name,
            string restBaseAddress,
            string socketBaseAddress,
            string marginSwapRestBaseAddress,
            string usdtMarginSwapSocketBaseAddress) : base(name)
        {
            RestBaseAddress = restBaseAddress;
            SocketBaseAddress = socketBaseAddress;
            UsdtMarginSwapRestBaseAddress = marginSwapRestBaseAddress;
            UsdtMarginSwapSocketBaseAddress = usdtMarginSwapSocketBaseAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static HuobiEnvironment Live { get; }
            = new HuobiEnvironment(TradeEnvironmentNames.Live,
                                   HuobiApiAddresses.Default.RestClientAddress,
                                   HuobiApiAddresses.Default.SocketClientAddress,
                                   HuobiApiAddresses.Default.UsdtMarginSwapRestClientAddress,
                                   HuobiApiAddresses.Default.UsdtMarginSwapSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="socketAddress"></param>
        /// <param name="usdtMarginSwapRestAddress"></param>
        /// <param name="usdtMarginSwapSocketAddress"></param>
        /// <returns></returns>
        public static HuobiEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string socketAddress,
                        string usdtMarginSwapRestAddress,
                        string usdtMarginSwapSocketAddress)
            => new HuobiEnvironment(name, restAddress, socketAddress, usdtMarginSwapRestAddress, usdtMarginSwapSocketAddress);
    }
}
