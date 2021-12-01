using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Client options
    /// </summary>
    public class HuobiClientOptions: BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiClientOptions Default { get; set; } = new HuobiClientOptions();

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        private RestApiClientOptions _spotApiOptions = new RestApiClientOptions("https://api.huobi.pro")
        {
            RateLimiters = new List<IRateLimiter>
            {
                    new RateLimiter()
                    .AddPartialEndpointLimit("/v1/order", 100, TimeSpan.FromSeconds(2), null, true, true)
                    .AddApiKeyLimit(10, TimeSpan.FromSeconds(1), true, true)
                    .AddTotalRateLimit(10, TimeSpan.FromSeconds(1))
            }
        };
        public RestApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions.Copy(_spotApiOptions, value);
        }

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
            input.SpotApiOptions = new RestApiClientOptions(def.SpotApiOptions);
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class HuobiSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static HuobiSocketClientOptions Default { get; set; } = new HuobiSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        private HuobiSocketApiClientOptions _spotStreamsOptions = new HuobiSocketApiClientOptions("wss://api.huobi.pro/ws", "wss://api.huobi.pro/ws/v2", "wss://api.huobi.pro/feed");
        public HuobiSocketApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions.Copy(_spotStreamsOptions, value);
        }

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

            input.SpotStreamsOptions = new HuobiSocketApiClientOptions(def.SpotStreamsOptions);
        }
    }

    public class HuobiSocketApiClientOptions : ApiClientOptions
    {
        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; }

        /// <summary>
        /// The base address for the market by price websocket
        /// </summary>
        public string BaseAddressInrementalOrderBook { get; set; }

        public HuobiSocketApiClientOptions()
        {
        }

        public HuobiSocketApiClientOptions(string baseAddress, string baseAddressAuthenticated, string baseAddressIncrementalOrderBook): base(baseAddress)
        {
            BaseAddressAuthenticated = baseAddressAuthenticated;
            BaseAddressInrementalOrderBook = baseAddressIncrementalOrderBook;
        }

        public HuobiSocketApiClientOptions(HuobiSocketApiClientOptions baseOn): base(baseOn)
        {
            BaseAddressAuthenticated = baseOn.BaseAddressAuthenticated;
            BaseAddressInrementalOrderBook = baseOn.BaseAddressInrementalOrderBook;
        }

        public new void Copy<T>(T input, T def) where T : HuobiSocketApiClientOptions
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
