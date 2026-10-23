using CryptoExchange.Net.SharedApis;
using HTX.Net.Clients.UsdtFutures;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    internal partial class HTXRestClientUsdtFuturesV5SharedApi:
        SharedApiBase,
        IHTXRestClientUsdtFuturesV5SharedApi
    {
        private readonly HTXRestClientUsdtFuturesV5Api _api;

        private const string _topicId = "HTXFutures";
        private const string _exchangeName = "HTX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public HTXRestClientUsdtFuturesV5SharedApi(HTXRestClientUsdtFuturesV5Api api)
             : base(
                  SharedTransport.Rest,
                  api,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetFundingInfoOptions,
                GetFundingRateHistoryOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetFuturesOrderByClientOrderIdOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetFuturesOrderTradesOptions
                );
        }

        private MarginMode? GetProvidedMarginMode(SharedRequest request)
        {
            var val = request.GetParamValue<SharedMarginMode?>(Exchange, "MarginMode");
            if (val == null)
                return null;

            return val == SharedMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated;
        }
    }
}
