using CryptoExchange.Net.Objects;
using System;
using Huobi.Net.Interfaces;

namespace Huobi.Net
{
    public class HuobiClientOptions: RestClientOptions
    {
        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        public HuobiClientOptions()
        {
            BaseAddress = "https://api.huobi.pro";
        }

        public HuobiClientOptions Copy()
        {
            var copy = Copy<HuobiClientOptions>();
            copy.SignPublicRequests = SignPublicRequests;
            return copy;
        }
    }

    public class HuobiSocketClientOptions : SocketClientOptions
    {
        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; } = "wss://api.huobi.pro/ws/v1";

        public HuobiSocketClientOptions()
        {
            BaseAddress = "wss://api.huobi.pro/ws";
            SocketSubscriptionsCombineTarget = 10;
        }

        public HuobiSocketClientOptions Copy()
        {
            var copy = Copy<HuobiSocketClientOptions>();
            copy.BaseAddressAuthenticated = BaseAddressAuthenticated;
            return copy;
        }
    }

    public class HuobiOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places
        /// </summary>
        public int? MergeStep { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IHuobiSocketClient SocketClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeStep">The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places</param>
        /// <param name="socketClient">The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.</param>
        public HuobiOrderBookOptions(int? mergeStep = null, IHuobiSocketClient socketClient = null) : base("Huobi", false)
        {
            SocketClient = socketClient;
            MergeStep = mergeStep;
        }
    }
}
