﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Objects;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientFuturesUsdtApi : RestApiClient, IHuobiClientFuturesUsdtApi, IFuturesClient
    {
        private readonly HuobiClient _baseClient;
        private readonly HuobiClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState();

        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        public Task<WebCallResult<OrderId>> CancelOrderAsync(string orderId, string? symbol = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string ExchangeName => "Huobi";

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientFuturesUsdtApiAccount Account { get; }
        /// <inheritdoc />
        public IHuobiClientFuturesUsdtApiTrading Trading { get; }
        /// <inheritdoc />
        public IHuobiClientFuturesUsdtApiExchangeData ExchangeData { get; }

        #endregion

        #region constructor/destructor
        internal HuobiClientFuturesUsdtApi(Log log, HuobiClient baseClient, HuobiClientOptions options)
            : base(options, options.FuturesUsdtApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new HuobiClientFuturesUsdtApiAccount(this);
            Trading = new HuobiClientFuturesUsdtApiTrading(this);
            ExchangeData = new HuobiClientFuturesUsdtApiExchangeData(this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, _options.SignPublicRequests, false);

        #region methods

        internal Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiV2Request<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiTimestampRequest<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
            => _baseClient.SendHuobiRequest<T>(this, uri, method, cancellationToken, parameters, signed, weight);

        internal Task<WebCallResult<T>> SendHuobiFuturesRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
            => _baseClient.SendHuobiFuturesRequest<T>(this, uri, method, cancellationToken, parameters, signed, weight);


        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="api"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, string api, string? version = null)
        {
            var result = BaseAddress.AppendPath(api);

            if (!string.IsNullOrEmpty(version))
                result = result.AppendPath($"v{version}");

            return new Uri(result.AppendPath(endpoint));
        }

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
        #endregion

        #region common interface
        public string GetSymbolName(string baseAsset, string quoteAsset)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Symbol>>> GetSymbolsAsync(CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<Ticker>> GetTickerAsync(string symbol, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Ticker>>> GetTickersAsync(CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Kline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null,
            int? limit = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<OrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Trade>>> GetRecentTradesAsync(string symbol, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Balance>>> GetBalancesAsync(string? accountId = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<Order>> GetOrderAsync(string orderId, string? symbol = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<UserTrade>>> GetOrderTradesAsync(string orderId, string? symbol = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Order>>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Order>>> GetClosedOrdersAsync(string? symbol = null, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var symbols = await ExchangeData.GetSymbolsAsync(ct).ConfigureAwait(false);
            if (!symbols)
                return symbols.As<IEnumerable<Symbol>>(null);

            return symbols.As(symbols.Data.Select(d => new Symbol
            {
                SourceObject = d,
                Name = d.ContractCode,
                PriceStep = d.PriceTick,
                QuantityStep = d.ContractSize
            }));
        }

        public Task<WebCallResult<Ticker>> GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Ticker>>> GetTickersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Kline>>> GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime = null, DateTime? endTime = null,
            int? limit = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<OrderBook>> GetOrderBookAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Trade>>> GetRecentTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Balance>>> GetBalancesAsync(string? accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<Order>> GetOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<UserTrade>>> GetOrderTradesAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Order>>> GetOpenOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Order>>> GetClosedOrdersAsync(string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<OrderId>> CancelOrderAsync(string orderId, string? symbol = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<OrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null,
            int? leverage = null, string? accountId = null)
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Position>>> GetPositionsAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.FuturesUsdtApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        public Task<WebCallResult<OrderId>> PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price = null,
            int? leverage = null, string? accountId = null, string? clientOrderId = null,
            CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<WebCallResult<IEnumerable<Position>>> GetPositionsAsync(CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
