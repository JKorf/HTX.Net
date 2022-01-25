using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Objects;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientFuturesCoinApi : RestApiClient, IHuobiClientFuturesCoinApi, IFuturesClient
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

        /// <inheritdoc />
        public string ExchangeName => "Huobi";

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientFuturesCoinApiTrading Trading { get; }
        /// <inheritdoc />
        public IHuobiClientFuturesCoinApiExchangeData ExchangeData { get; }

        #endregion

        #region constructor/destructor
        internal HuobiClientFuturesCoinApi(Log log, HuobiClient baseClient, HuobiClientOptions options)
            : base(options, options.FuturesCoinApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Trading = new HuobiClientFuturesCoinApiTrading(this);
            ExchangeData = new HuobiClientFuturesCoinApiExchangeData(this);
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
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, string? version = null)
        {
            if (version == null)
                return new Uri(BaseAddress.AppendPath(endpoint));
            return new Uri(BaseAddress.AppendPath($"v{version}", endpoint));
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

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync()
        {
            var symbols = await ExchangeData.GetSymbolsAsync().ConfigureAwait(false);
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
            => new TimeSyncInfo(_log, _options.FuturesCoinApiOptions.AutoTimestamp, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;
    }
}
