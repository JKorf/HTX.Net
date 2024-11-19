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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public HTXEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the HTX environment by name
        /// </summary>
        public static HTXEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

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
