using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;

namespace HTX.Net
{
    /// <summary>
    /// HTX exchange information and configuration
    /// </summary>
    public static class HTXExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "HTX";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.htx.com/";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://www.htx.com/en-us/opend/newApiPages/"
            };

        /// <summary>
        /// Rate limiter configuration for the HTX API
        /// </summary>
        public static HTXRateLimiters RateLimiter { get; } = new HTXRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the HTX API
    /// </summary>
    public class HTXRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal HTXRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            EndpointLimit = new RateLimitGate("Endpoint Limit");
            SpotMarketLimit = new RateLimitGate("Spot Market Limit")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new IGuardFilter[] { }, 10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            SpotConnection = new RateLimitGate("Spot Connection Messages")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new IGuardFilter[] { }, 50, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 50 requests per second per connection
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { }, 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 100 requests per second per IP

            UsdtTrade = new RateLimitGate("USDT-M Trade")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new IGuardFilter[] { }, 72, TimeSpan.FromSeconds(3), RateLimitWindowType.Fixed));
            UsdtRead = new RateLimitGate("USDT-M Read")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new IGuardFilter[] { }, 72, TimeSpan.FromSeconds(3), RateLimitWindowType.Fixed));
            PublicMarket = new RateLimitGate("Public Market")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { }, 800, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed));
            UsdtPublicReference = new RateLimitGate("USDT-M Public Reference")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { }, 240, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed));
            UsdtConnection = new RateLimitGate("USDT-M Connection Messages")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerEndpoint, new IGuardFilter[] { }, 40, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 40 requests per second per connection

        }

        internal IRateLimitGate EndpointLimit { get; private set; }
        internal IRateLimitGate SpotMarketLimit { get; private set; }
        internal IRateLimitGate SpotConnection { get; private set; }
        internal IRateLimitGate UsdtTrade { get; private set; }
        internal IRateLimitGate UsdtRead { get; private set; }
        internal IRateLimitGate PublicMarket { get; private set; }
        internal IRateLimitGate UsdtPublicReference { get; private set; }
        internal IRateLimitGate UsdtConnection { get; private set; }

    }
}
