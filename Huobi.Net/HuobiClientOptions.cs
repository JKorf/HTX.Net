using CryptoExchange.Net.Objects;
using System;

namespace Huobi.Net
{
    public class HuobiClientOptions: ClientOptions
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
}
