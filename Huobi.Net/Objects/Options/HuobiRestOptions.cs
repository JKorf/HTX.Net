using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Options
{
    /// <summary>
    /// Options for the HuobiRestClient
    /// </summary>
    public class HuobiRestOptions : RestExchangeOptions<HuobiEnvironment>
    {
        /// <summary>
        /// Default options for the HuobiRestClient
        /// </summary>
        public static HuobiRestOptions Default { get; set; } = new HuobiRestOptions()
        {
            Environment = HuobiEnvironment.Live
        };

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions()
        {
            RateLimiters = new List<IRateLimiter>
            {
                    new RateLimiter()
                    .AddPartialEndpointLimit("/v1/order", 100, TimeSpan.FromSeconds(2), null, true, true)
                    .AddApiKeyLimit(10, TimeSpan.FromSeconds(1), true, true)
                    .AddTotalRateLimit(10, TimeSpan.FromSeconds(1))
            }
        };

        /// <summary>
        /// USDT margin swap API options
        /// </summary>
        public RestApiOptions UsdtMarginSwapOptions { get; private set; } = new RestApiOptions();

        internal HuobiRestOptions Copy()
        {
            var options = Copy<HuobiRestOptions>();
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            options.UsdtMarginSwapOptions = UsdtMarginSwapOptions.Copy<RestApiOptions>();
            options.SignPublicRequests = SignPublicRequests;
            return options;
        }
    }
}
