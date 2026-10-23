using CryptoExchange.Net.SharedApis;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXSocketClientUsdtFuturesV5SharedApi :
        SharedApiBase,
        IHTXSocketClientUsdtFuturesV5SharedApi
    {
        private readonly HTXSocketClientUsdtFuturesV5Api _api;

        private const string _topicId = "HTXFutures";
        private const string _exchangeName = "HTX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public HTXSocketClientUsdtFuturesV5SharedApi(HTXSocketClientUsdtFuturesV5Api api)
             : base(
                  SharedTransport.Socket,
                  api,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeBalanceOptions,
                SubscribeFuturesOrderOptions,
                SubscribeUserTradeOptions,
                SubscribePositionOptions,
                PlaceFuturesOrderOptions,
                CancelFuturesOrderOptions,
                CancelAllFuturesOrdersOptions,
                CancelAllFuturesSymbolOrdersOptions,
                CancelFuturesOrderByClientOrderIdOptions
                );
        }

        private MarginMode? GetProvidedMarginMode(SharedRequest request)
        {
            var val = request.GetParamValue<SharedMarginMode?>(Exchange, "MarginMode");
            if (val == null)
                return null;

            return val == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated;
        }

        /// <inheritdoc />
        public Task UnsubscribeAllAsync() => _api.UnsubscribeAllAsync();
    }
}
