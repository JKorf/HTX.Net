using CryptoExchange.Net.RateLimiting;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.SharedApis;

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
        /// Format a base and quote asset to an HTX recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            if (tradingMode == TradingMode.Spot)
                return $"{baseAsset.ToLowerInvariant()}{quoteAsset.ToLowerInvariant()}";

            return $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}" + (!deliverTime.HasValue ? string.Empty : ("-" + deliverTime.Value.ToString("yyMMdd")));
        }

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
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new ExactPathFilter("/ws/v2"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 50, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)) // 50 requests per second per connection
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { new ExactPathFilter("/ws/v2"), new LimitItemTypeFilter(RateLimitItemType.Request) }, 100, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 100 requests per second per IP

            UsdtTrade = new RateLimitGate("USDT-M Trade")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new IGuardFilter[] { }, 72, TimeSpan.FromSeconds(3), RateLimitWindowType.Fixed));
            UsdtRead = new RateLimitGate("USDT-M Read")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerApiKeyPerEndpoint, new IGuardFilter[] { }, 72, TimeSpan.FromSeconds(3), RateLimitWindowType.Fixed));
            PublicMarket = new RateLimitGate("Public Market")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { }, 800, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed));
            UsdtPublicReference = new RateLimitGate("USDT-M Public Reference")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new IGuardFilter[] { }, 240, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed));
            UsdtConnection = new RateLimitGate("USDT-M Connection Messages")
                                            .AddGuard(new RateLimitGuard(RateLimitGuard.PerConnection, new IGuardFilter[] { new LimitItemTypeFilter(RateLimitItemType.Request) }, 40, TimeSpan.FromSeconds(1), RateLimitWindowType.Fixed)); // 40 requests per second per connection

            EndpointLimit.RateLimitTriggered += RateLimitTriggered;
            SpotMarketLimit.RateLimitTriggered += RateLimitTriggered;
            SpotConnection.RateLimitTriggered += RateLimitTriggered;
            UsdtTrade.RateLimitTriggered += RateLimitTriggered;
            UsdtRead.RateLimitTriggered += RateLimitTriggered;
            PublicMarket.RateLimitTriggered += RateLimitTriggered;
            UsdtPublicReference.RateLimitTriggered += RateLimitTriggered;
            UsdtConnection.RateLimitTriggered += RateLimitTriggered;
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
