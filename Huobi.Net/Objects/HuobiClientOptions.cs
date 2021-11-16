using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients.Socket;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Client options
    /// </summary>
    public class HuobiClientSpotOptions: RestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiClientSpotOptions Default { get; set; } = new HuobiClientSpotOptions()
        {
            BaseAddress = "https://api.huobi.pro",
            RateLimiters = new List<IRateLimiter>
            {
                 new RateLimiter()
                    .AddPartialEndpointLimit("/v1/order", 100, TimeSpan.FromSeconds(2), null, true, true)
                    .AddApiKeyLimit(10, TimeSpan.FromSeconds(1), true, true)
                    .AddTotalRateLimit(10, TimeSpan.FromSeconds(1))
            }
        };

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        /// <summary>
        /// Ctor
        /// </summary>
        public HuobiClientSpotOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : HuobiClientSpotOptions
        {
            base.Copy(input, def);

            input.SignPublicRequests = def.SignPublicRequests;
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class HuobiSocketClientSpotOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiSocketClientSpotOptions Default { get; set; } = new HuobiSocketClientSpotOptions()
        {
            BaseAddress = "wss://api.huobi.pro/ws",
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; } = "wss://api.huobi.pro/ws/v2";

        /// <summary>
        /// The base address for the market by price websocket
        /// </summary>
        public string BaseAddressInrementalOrderBook { get; set; } = "wss://api.huobi.pro/feed";

        /// <summary>
        /// Ctor
        /// </summary>
        public HuobiSocketClientSpotOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : HuobiSocketClientSpotOptions
        {
            base.Copy(input, def);

            input.BaseAddressAuthenticated = def.BaseAddressAuthenticated;
            input.BaseAddressInrementalOrderBook = def.BaseAddressInrementalOrderBook;
        }
    }

    /// <summary>
    /// Order book options
    /// </summary>
    public class HuobiOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places
        /// </summary>
        public int? MergeStep { get; set; }

        /// <summary>
        /// The amount of entries to maintain. Either 5, 20 or 150. Level 5 and 20 are currently only supported for the following symbols: btcusdt, ethusdt, xrpusdt, eosusdt, ltcusdt, etcusdt, adausdt, dashusdt, bsvusdt.
        /// </summary>
        public int? Levels { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IHuobiSocketClientSpot? SocketClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeStep">The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places</param>
        /// <param name="levels">The amount of entries to maintain. Either 5, 20 or 150</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        public HuobiOrderBookOptions(int? mergeStep = null, int? levels = null, IHuobiSocketClientSpot? socketClient = null)
        {
            SocketClient = socketClient;
            MergeStep = mergeStep;
            Levels = levels;
        }
    }
}
