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
using HTX.Net.Enums;

namespace HTX.Net.Clients.SpotApi
{
    internal partial class HTXRestClientSpotApi : IHTXRestClientSpotApiShared
    {
        private long? _accountId;

        public string Exchange => HTXExchange.ExchangeName;

        public IEnumerable<SharedOrderType> SupportedOrderType { get; } = new[]
        {
            SharedOrderType.Limit,
            SharedOrderType.Market,
            SharedOrderType.LimitMaker
        };

        public IEnumerable<SharedTimeInForce> SupportedTimeInForce { get; } = new[]
        {
            SharedTimeInForce.GoodTillCanceled,
            SharedTimeInForce.ImmediateOrCancel,
            SharedTimeInForce.FillOrKill
        };

        public SharedQuoteQuantitySupport QuoteQuantitySupport => SharedQuoteQuantitySupport.MarketBuyForced;

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

            var quantity = request.Quantity ?? 0;
            if (request.OrderType == SharedOrderType.Market && request.Side == SharedOrderSide.Buy)
                quantity = request.QuoteQuantity ?? 0;

            var result = await Trading.PlaceOrderAsync(
                accountId.Value,
                FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType),
                request.Side == SharedOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                GetPlaceOrderType(request.OrderType, request.TimeInForce),
                quantity,
                request.Price,
                request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.As<SharedOrderId>(default);

            return result.As(new SharedOrderId(result.Data.ToString()));
        }

        public async Task<WebCallResult<SharedSpotOrder>> GetOrderAsync(GetOrderRequest request, CancellationToken ct = default)
        {
            if (!long.TryParse(request.OrderId, out var orderId))
                return new WebCallResult<SharedSpotOrder>(new ArgumentError("Invalid order id"));

            var order = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
            if (!order)
                return order.As<SharedSpotOrder>(default);

            return order.As(new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.Id.ToString(),
                ParseOrderType(order.Data.Type),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Fee = order.Data.Fee,
                Price = order.Data.Price,
                Quantity = order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? null : order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.Type == OrderType.Market && order.Data.Side == OrderSide.Buy ? order.Data.Quantity : null,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                TimeInForce = ParseTimeInForce(order.Data.Type)
            });
        }

        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetOpenOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedSpotOrder>>> GetClosedOrdersAsync(GetClosedOrdersRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedUserTrade>>> GetUserTradesAsync(GetUserTradesRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<SharedOrderId>> CancelOrderAsync(CancelOrderRequest request, CancellationToken ct = default) => throw new NotImplementedException();
        public Task<WebCallResult<IEnumerable<SharedTicker>>> GetTickersAsync(CancellationToken ct = default) => throw new NotImplementedException();

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Submitted || status == OrderStatus.PreSubmitted || status == OrderStatus.Created || status == OrderStatus.PartiallyFilled) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled || status == OrderStatus.Rejected) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit || type == OrderType.FillOrKillLimit || type == OrderType.IOC) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType tif)
        {
            if (tif == OrderType.IOC) return SharedTimeInForce.ImmediateOrCancel;
            if (tif == OrderType.FillOrKillLimit) return SharedTimeInForce.FillOrKill;
            if (tif == OrderType.Limit) return SharedTimeInForce.GoodTillCanceled;
            if (tif == OrderType.LimitMaker) return SharedTimeInForce.GoodTillCanceled;

            return null;
        }

        private OrderType GetPlaceOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.Limit && (tif == null || tif == SharedTimeInForce.GoodTillCanceled)) return OrderType.Limit;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.IOC;
            if (type == SharedOrderType.Limit && tif == SharedTimeInForce.FillOrKill) return OrderType.FillOrKillLimit;
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (type == SharedOrderType.Market) return OrderType.Market;

            throw new ArgumentException($"The combination of order type `{type}` and time in force `{tif}` in invalid");
        }

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
