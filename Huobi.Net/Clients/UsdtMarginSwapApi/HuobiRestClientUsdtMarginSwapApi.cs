using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.UsdtMarginSwapApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class HuobiRestClientUsdtMarginSwapApi : RestApiClient, IHuobiRestClientUsdtMarginSwapApi
    {
        /// <inheritdoc />
        public new HuobiRestOptions ClientOptions => (HuobiRestOptions)base.ClientOptions;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Usdt Margin Swap Api");

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

        internal readonly string _brokerId;

        #region Api clients

        /// <inheritdoc />
        public IHuobiRestClientUsdtMarginSwapApiAccount Account { get; }
        /// <inheritdoc />
        public IHuobiRestClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHuobiRestClientUsdtMarginSwapApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HuobiRestClientUsdtMarginSwapApi(ILogger log, HttpClient? httpClient, HuobiRestOptions options)
            : base(log, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            Account = new HuobiRestClientUsdtMarginSwapApiAccount(this);
            ExchangeData = new HuobiRestClientUsdtMarginSwapApiExchangeData(this);
            Trading = new HuobiRestClientUsdtMarginSwapApiTrading(this);

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "AA1ef14811";
        }
        #endregion

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}";

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

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

        internal async Task<WebCallResult<DateTime>> SendTimestampRequestAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<string>>(uri, method, cancellationToken, parameters, signed, requestWeight: 0).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<DateTime>(result.Error!);

            return result.As(result.Data.Timestamp);
        }


        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: 0).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<string>(MessagePath.Get().Property("err-code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg"));

            if (code == null || msg == null)
                return new ServerError(accessor.GetOriginalString());


            return new ServerError($"{code}, {msg}");
        }

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;
    }
}
