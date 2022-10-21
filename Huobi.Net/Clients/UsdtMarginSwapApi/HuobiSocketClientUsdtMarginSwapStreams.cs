using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HuobiOrderUpdate = Huobi.Net.Objects.Models.Socket.HuobiOrderUpdate;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiSocketClientUsdtMarginSwapStreams : SocketApiClient
    {
        #region fields
        private readonly string _baseAddressAuthenticated;
        private readonly string _baseAddressIndex;

        private readonly HuobiSocketClient _baseClient;
        private readonly Log _log;
        #endregion

        #region ctor
        internal HuobiSocketClientUsdtMarginSwapStreams(Log log, HuobiSocketClient baseClient, HuobiSocketClientOptions options)
            : base(options, options.UsdtMarginSwapOptions)
        {
            _log = log;
            _baseClient = baseClient;
            _baseAddressAuthenticated = options.UsdtMarginSwapOptions.BaseAddressAuthenticated;
            _baseAddressIndex = options.UsdtMarginSwapOptions.BaseAddressIndex;
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, false);

        #region methods

        /// <inheritdoc />
        //public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        //{
        //    var request = new HuobiSocketRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
        //    var result = await _baseClient.QueryInternalAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(this, request, false).ConfigureAwait(false);
        //    return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
        //}

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.depth.step" + mergeStep);
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var request = new HuobiIncrementalOrderBookSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.depth.size_{limit}.high_freq",
                snapshot ? "snapshot": "incremental");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTickUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.bbo");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiBestOfferUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.trade.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapTradesUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.premium_index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.estimated_rate.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.basis.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}.{priceType}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapBasisUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.mark_price.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedOrderUpdatesAsync(Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSocketRequest2(
                "sub",
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"orders.*");
            var internalHandler = new Action<DataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedOrderUpdatesAsync(string contractCode, Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSocketRequest2(
                "sub",
                _baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture),
                $"orders.{contractCode}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiIsolatedMarginOrder>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        #region private

        private void DeserializeAndInvoke<T>(DataEvent<JToken> data, Action<DataEvent<T>>? action, string? symbol = null)
        {
            var obj = _baseClient.DeserializeInternal<T>(data.Data["data"]!);
            if (!obj)
            {
                _log.Write(LogLevel.Error, $"Failed to deserialize {typeof(T).Name}: " + obj.Error);
                return;
            }
            action?.Invoke(data.As(obj.Data, symbol));
        }

        #endregion
        #endregion

    }
}
