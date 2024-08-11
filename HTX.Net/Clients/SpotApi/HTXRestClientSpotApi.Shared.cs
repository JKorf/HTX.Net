using HTX.Net;
using HTX.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Enums;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotApi : IHTXRestClientSpotApiShared
    {
        public string Exchange => HTXExchange.ExchangeName;

        public IEnumerable<SharedOrderType> SupportedOrderType => throw new NotImplementedException();

        public IEnumerable<SharedTimeInForce> SupportedTimeInForce { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<SharedOrderType> QuoteQuantitySupport => throw new NotImplementedException();

        async Task<WebCallResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval.TotalSeconds;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new WebCallResult<IEnumerable<SharedKline>>(new ArgumentError("Interval not supported"));

            var result = await ExchangeData.GetKlinesAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                interval,
                request.Limit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedKline>>(default);

            return result.As(result.Data.Select(x => new SharedKline
            {
                BaseVolume = x.Volume!.Value,
                ClosePrice = x.ClosePrice!.Value,
                HighPrice = x.HighPrice!.Value,
                LowPrice = x.LowPrice!.Value,
                OpenPrice = x.OpenPrice!.Value,
                OpenTime = x.OpenTime
            }));
        }

        async Task<WebCallResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedSpotSymbol>>(default);

            return result.As(result.Data.Select(s => new SharedSpotSymbol
            {
                BaseAsset = s.BaseAsset,
                QuoteAsset = s.QuoteAsset,
                Name = s.Name,
                QuantityDecimals = (int)s.QuantityPrecision,
                PriceDecimals = (int)s.PricePrecision
            }));
        }

        async Task<WebCallResult<IEnumerable<SharedTicker>>> ITickerRestClient.GetTickersAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(
                ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTicker>>(default);

            return result.As(result.Data.Ticks.Select(x => new SharedTicker
            {
                HighPrice = x.HighPrice ?? 0,
                LastPrice = x.LastTradePrice,
                LowPrice = x.LowPrice ?? 0,
            }));
        }

        async Task<WebCallResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.GetTickerAsync(
                symbol,
                ct).ConfigureAwait(false);
            if (!result)
                return result.As<SharedTicker>(default);

            return result.As(new SharedTicker
            {
                Symbol = symbol,
                HighPrice = result.Data.HighPrice ?? 0,
                LastPrice = result.Data.ClosePrice ?? 0,
                LowPrice = result.Data.LowPrice ?? 0,
            });
        }

        async Task<WebCallResult<IEnumerable<SharedTrade>>> ITradeRestClient.GetTradesAsync(GetTradesRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTradeHistoryAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTrade>>(default);

            return result.As(result.Data.SelectMany(x => x.Details.Select(x => new SharedTrade
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Timestamp = x.Timestamp
            })));
        }

         async Task<WebCallResult<SharedOrderId>> ISpotOrderRestClient.PlaceOrderAsync(PlaceSpotPlaceOrderRequest request, CancellationToken ct)
        {
            var accountId = request.GetAdditionalParameter<long>(Exchange, "accountId");
            var result = await Trading.PlaceOrderAsync(
                accountId,
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Limit ? Enums.OrderType.Limit : Enums.OrderType.Market,
                request.Quantity ?? 0,
                request.Price,
                request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.As<SharedOrderId>(default);

            return result.As(new SharedOrderId
            {
                OrderId = result.Data.ToString()
            });
        }

        public Task<WebCallResult<SharedSpotOrder>> GetOrderAsync(GetOrderRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetOpenOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetClosedOrdersAsync(GetClosedOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedUserTrade>>> GetUserTradesAsync(GetUserTradesRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<SharedOrderId>> CancelOrderAsync(SpotCancelOrderRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedTicker>>> GetTickersAsync(CancellationToken ct = default) => throw new NotImplementedException();
    }
}
