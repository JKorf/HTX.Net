using CryptoExchange.Net.RateLimiting;
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
        }

        internal IRateLimitGate EndpointLimit { get; private set; }

    }
}
