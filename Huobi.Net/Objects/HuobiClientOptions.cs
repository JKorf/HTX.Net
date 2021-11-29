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
    public class HuobiClientOptions: RestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiClientOptions Default { get; set; } = new HuobiClientOptions()
        {
            OptionsSpot = new RestSubClientOptions
            {
                BaseAddress = "https://api.huobi.pro",
                RateLimiters = new List<IRateLimiter>
                {
                     new RateLimiter()
                        .AddPartialEndpointLimit("/v1/order", 100, TimeSpan.FromSeconds(2), null, true, true)
                        .AddApiKeyLimit(10, TimeSpan.FromSeconds(1), true, true)
                        .AddTotalRateLimit(10, TimeSpan.FromSeconds(1))
                }
            }
        };

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        public RestSubClientOptions OptionsSpot { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public HuobiClientOptions()
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
        public new void Copy<T>(T input, T def) where T : HuobiClientOptions
        {
            base.Copy(input, def);

            input.SignPublicRequests = def.SignPublicRequests;
            input.OptionsSpot = new RestSubClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);
        }
    }

    public class HuobiSubSocketClientOptions: SocketSubClientOptions
    {
        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; }

        /// <summary>
        /// The base address for the market by price websocket
        /// </summary>
        public string BaseAddressInrementalOrderBook { get; set; }

        public new void Copy<T>(T input, T def) where T : HuobiSubSocketClientOptions
        {
            base.Copy(input, def);

            input.BaseAddressAuthenticated = def.BaseAddressAuthenticated;
            input.BaseAddressInrementalOrderBook = def.BaseAddressInrementalOrderBook;
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class HuobiSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiSocketClientOptions Default { get; set; } = new HuobiSocketClientOptions()
        {
            OptionsSpot = new HuobiSubSocketClientOptions
            {
                BaseAddress = "wss://api.huobi.pro/ws",
                BaseAddressAuthenticated = "wss://api.huobi.pro/ws/v2",
                BaseAddressInrementalOrderBook = "wss://api.huobi.pro/feed"
            },
            SocketSubscriptionsCombineTarget = 10
        };

        public HuobiSubSocketClientOptions OptionsSpot { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public HuobiSocketClientOptions()
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
        public new void Copy<T>(T input, T def) where T : HuobiSocketClientOptions
        {
            base.Copy(input, def);

            input.OptionsSpot = new HuobiSubSocketClientOptions();
            def.OptionsSpot.Copy(input.OptionsSpot, def.OptionsSpot);
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
        public IHuobiSocketClient? SocketClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeStep">The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places</param>
        /// <param name="levels">The amount of entries to maintain. Either 5, 20 or 150</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        public HuobiOrderBookOptions(int? mergeStep = null, int? levels = null, IHuobiSocketClient? socketClient = null)
        {
            SocketClient = socketClient;
            MergeStep = mergeStep;
            Levels = levels;
        }
    }
}
