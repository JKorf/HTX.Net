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
using CryptoExchange.Net.SharedApis.Models.Rest;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotApi : IHTXRestClientSpotApiShared
    {
        private long? _accountId;

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

            return result.As(result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice!.Value, x.HighPrice!.Value, x.LowPrice!.Value, x.OpenPrice!.Value, x.Volume!.Value)));
        }

        async Task<WebCallResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSymbolsAsync(SharedRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedSpotSymbol>>(default);

            return result.As(result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Name)
            {
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

            return result.As(result.Data.Ticks.Select(x => new SharedTicker(x.Symbol, x.LastTradePrice, x.HighPrice ?? 0, x.LowPrice ?? 0)));
        }

        async Task<WebCallResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await ExchangeData.GetTickerAsync(
                symbol,
                ct).ConfigureAwait(false);
            if (!result)
                return result.As<SharedTicker>(default);

            return result.As(new SharedTicker(symbol, result.Data.ClosePrice ?? 0, result.Data.HighPrice ?? 0, result.Data.LowPrice ?? 0));
        }

        async Task<WebCallResult<IEnumerable<SharedTrade>>> ITradeRestClient.GetTradesAsync(GetTradesRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTradeHistoryAsync(
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedTrade>>(default);

            return result.As(result.Data.SelectMany(x => x.Details.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp))));
        }

        async Task<WebCallResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(SharedRequest request, CancellationToken ct)
        {
            var accountId = await GetAccountId(request, ct).ConfigureAwait(false);
            if (accountId == null)
                return new WebCallResult<IEnumerable<SharedBalance>>(new ServerError("Failed to retrieve account id"));

            var result = await Account.GetBalancesAsync(accountId.Value, ct: ct).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<SharedBalance>>(default);

            var resp = new List<SharedBalance>();
            foreach(var balance in result.Data)
            {
                var asset = resp.SingleOrDefault(x => x.Asset == balance.Asset);
                if (asset == null)
                {
                    asset = new SharedBalance(balance.Asset.ToUpperInvariant(), 0, 0);
                    resp.Add(asset);
                }

                if (balance.Type == Enums.BalanceType.Trade)
                {
                    asset.Available = balance.Balance;
                    asset.Total += balance.Balance;
                }
                else if (balance.Type == Enums.BalanceType.Frozen)
                    asset.Total += balance.Balance;
            }

            return result.As<IEnumerable<SharedBalance>>(resp);
        }

        async Task<WebCallResult<SharedOrderId>> ISpotOrderRestClient.PlaceOrderAsync(PlaceSpotPlaceOrderRequest request, CancellationToken ct)
        {
            var accountId = await GetAccountId(request, ct).ConfigureAwait(false);
            if (accountId == null)
                return new WebCallResult<SharedOrderId>(new ServerError("Failed to retrieve account id"));

            var result = await Trading.PlaceOrderAsync(
                accountId.Value,
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                request.OrderType == SharedOrderType.Limit ? Enums.OrderType.Limit : Enums.OrderType.Market,
                request.Quantity ?? 0,
                request.Price,
                request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.As<SharedOrderId>(default);

            return result.As(new SharedOrderId(result.Data.ToString()));
        }

        public Task<WebCallResult<SharedSpotOrder>> GetOrderAsync(GetOrderRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetOpenOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetClosedOrdersAsync(GetClosedOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedUserTrade>>> GetUserTradesAsync(GetUserTradesRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<SharedOrderId>> CancelOrderAsync(CancelOrderRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedTicker>>> GetTickersAsync(CancellationToken ct = default) => throw new NotImplementedException();
    
        private async ValueTask<long?> GetAccountId(SharedRequest request, CancellationToken ct)
        {
            var accountId = request.GetAdditionalParameter<long?>(Exchange, "accountId");
            if (accountId != null)
                return accountId;

            if (_accountId != null)
                return _accountId;

            var accounts = await Account.GetAccountsAsync(ct).ConfigureAwait(false);
            if (!accounts)
                return default;

            _accountId = accounts.Data.First(f => f.Type == Enums.AccountType.Spot).Id;
            return _accountId;
        }
    }
}
