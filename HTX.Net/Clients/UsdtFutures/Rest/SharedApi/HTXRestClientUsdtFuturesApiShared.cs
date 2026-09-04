using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net;
using HTX.Net.Enums;
using HTX.Net.Objects.Models.UsdtMarginSwap;
using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net.Clients.UsdtFutures
{
    internal partial class HTXRestClientUsdtFuturesSharedApi :
        SharedApiBase,
        IHTXRestClientUsdtFuturesApiShared,
        IHTXRestClientUsdtFuturesSharedApi
    {
        private readonly HTXRestClientUsdtFuturesApi _api;

        private const string _topicId = "HTXFutures";
        private const string _exchangeName = "HTX";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(HTXExchange.Metadata, this);

        public HTXRestClientUsdtFuturesSharedApi(HTXRestClientUsdtFuturesApi api)
             : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBookTickerOptions,
                GetFuturesSymbolsOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetIndexPriceKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetFundingRateHistoryOptions,
                GetOpenInterestOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetFeeOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions
                );
        }
    }
}
