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
        public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var result = await _baseClient.QueryInternalAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(this, request, false).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(_baseClient.NextIdInternal().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, internalHandler, ct).ConfigureAwait(false);
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
