using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal partial class HTXRestClientUsdtFuturesApi : RestApiClient, IHTXRestClientUsdtFuturesApi
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Usdt Margin Swap Api");

        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;


        /// <inheritdoc />
        public string ExchangeName => "HTX";

        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HTXRestClientUsdtFuturesApi(ILogger log, HttpClient? httpClient, HTXRestOptions options)
            : base(log, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            Account = new HTXRestClientUsdtMarginSwapApiAccount(this);
            ExchangeData = new HTXRestClientUsdtFuturesApiExchangeData(this);
            SubAccount = new HTXRestClientUsdtFuturesApiSubAccount(this);
            Trading = new HTXRestClientUsdtFuturesApiTrading(this);

        }
        #endregion

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        public IHTXRestClientUsdtFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        internal async Task<WebCallResult<T>> SendToAddressRawAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXApiResponseV2<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.As(result.Data.Data);
        }

        internal Task<WebCallResult> SendBasicAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendBasicToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendBasicToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsDatalessError(result.Error!);

            if (!string.IsNullOrEmpty(result.Data.ErrorCode))
                return result.AsDatalessError(new ServerError(result.Data.ErrorCode!, GetErrorInfo(result.Data.ErrorCode!, result.Data.ErrorMessage)));

            return result.AsDataless();

        }

        internal Task<WebCallResult<T>> SendBasicAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendBasicToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendBasicToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (!string.IsNullOrEmpty(result.Data.ErrorCode))
                return result.AsError<T>(new ServerError(result.Data.ErrorCode!, GetErrorInfo(result.Data.ErrorCode!, result.Data.ErrorMessage)));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<string?>(MessagePath.Get().Property("err-code")) ?? accessor.GetValue<string>(MessagePath.Get().Property("err_code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg")) ?? accessor.GetValue<string>(MessagePath.Get().Property("err_msg"));

            if (code == null || msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);


            return new ServerError(code, GetErrorInfo(code, msg), exception);
        }

        /// <inheritdoc />
        protected override Error? TryParseError(KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown);

            var errCode = accessor.GetValue<string>(MessagePath.Get().Property("err-code")) ?? accessor.GetValue<string>(MessagePath.Get().Property("err_code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg")) ?? accessor.GetValue<string>(MessagePath.Get().Property("err_msg"));

            if (!string.IsNullOrEmpty(errCode))
                return new ServerError(errCode!, GetErrorInfo(errCode!, msg));

            return null;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;
    }
}
