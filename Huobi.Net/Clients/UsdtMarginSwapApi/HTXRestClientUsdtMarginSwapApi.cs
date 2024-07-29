using CryptoExchange.Net.Clients;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters.MessageParsing;
using HTX.Net.Clients.UsdtMarginSwapApi;
using HTX.Net.Interfaces.Clients.UsdtMarginSwapApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class HTXRestClientUsdtMarginSwapApi : RestApiClient, IHTXRestClientUsdtMarginSwapApi
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Usdt Margin Swap Api");

        /// <inheritdoc />
        public string ExchangeName => "HTX";

        internal readonly string _brokerId;

        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientUsdtMarginSwapApiAccount Account { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtMarginSwapApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HTXRestClientUsdtMarginSwapApi(ILogger log, HttpClient? httpClient, HTXRestOptions options)
            : base(log, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            Account = new HTXRestClientUsdtMarginSwapApiAccount(this);
            ExchangeData = new HTXRestClientUsdtMarginSwapApiExchangeData(this);
            Trading = new HTXRestClientUsdtMarginSwapApiTrading(this);

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "AA1ef14811";
        }
        #endregion

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => $"{baseAsset.ToUpperInvariant()}-{quoteAsset.ToUpperInvariant()}";

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

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
            var result = await SendRequestAsync<HTXBasicResponse<string>>(uri, method, cancellationToken, parameters, signed, requestWeight: 0).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<DateTime>(result.Error!);

            return result.As(result.Data.Timestamp);
        }


        internal async Task<WebCallResult<T>> SendHTXRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HTXBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: 0).ConfigureAwait(false);
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
