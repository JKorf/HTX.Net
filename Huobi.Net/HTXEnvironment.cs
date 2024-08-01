using HTX.Net.Objects;

namespace HTX.Net
{
    /// <summary>
    /// HTX environments
    /// </summary>
    public class HTXEnvironment : TradeEnvironment
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

        internal HTXEnvironment(string name,
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
        public static HTXEnvironment Live { get; }
            = new HTXEnvironment(TradeEnvironmentNames.Live,
                                   HTXApiAddresses.Default.RestClientAddress,
                                   HTXApiAddresses.Default.SocketClientAddress,
                                   HTXApiAddresses.Default.UsdtMarginSwapRestClientAddress,
                                   HTXApiAddresses.Default.UsdtMarginSwapSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="socketAddress"></param>
        /// <param name="usdtMarginSwapRestAddress"></param>
        /// <param name="usdtMarginSwapSocketAddress"></param>
        /// <returns></returns>
        public static HTXEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string socketAddress,
                        string usdtMarginSwapRestAddress,
                        string usdtMarginSwapSocketAddress)
            => new HTXEnvironment(name, restAddress, socketAddress, usdtMarginSwapRestAddress, usdtMarginSwapSocketAddress);
    }
}
