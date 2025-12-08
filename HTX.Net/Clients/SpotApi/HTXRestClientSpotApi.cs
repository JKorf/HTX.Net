using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;
using System.Net.Http.Headers;

namespace HTX.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal partial class HTXRestClientSpotApi : RestApiClient, IHTXRestClientSpotApi
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        protected override ErrorMapping ErrorMapping => HTXErrors.SpotMapping;

        protected override IRestMessageHandler MessageHandler => new HTXRestMessageHandler(HTXErrors.SpotMapping);

        /// <inheritdoc />
        public string ExchangeName => "HTX";

        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiMargin Margin { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IHTXRestClientSpotApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HTXRestClientSpotApi(ILogger logger, HttpClient? httpClient, HTXRestOptions options)
            : base(logger, httpClient, options.Environment.RestBaseAddress, options, options.SpotOptions)
        {
            Account = new HTXRestClientSpotApiAccount(this);
            ExchangeData = new HTXRestClientSpotApiExchangeData(this);
            SubAccount = new HTXRestClientSpotApiSubAccount(this);
            Margin = new HTXRestClientSpotApiMargin(this);
            Trading = new HTXRestClientSpotApiTrading(this);

        }
        #endregion

        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        #region methods
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

        internal Task<WebCallResult<(T, DateTime)>> SendTimestampAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendTimestampToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<(T, DateTime)>> SendTimestampToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<(T, DateTime)>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<(T, DateTime)>(new ServerError(result.Data.ErrorCode, GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage)));

            return result.As((result.Data.Data, result.Data.Timestamp));
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
        protected override Error ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<string>(MessagePath.Get().Property("err-code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("err-msg"));

            if (code == null || msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            return new ServerError(code!, GetErrorInfo(code, msg), exception);
        }

        /// <inheritdoc />
        protected override Error? TryParseError(RequestDefinition request, HttpResponseHeaders responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var errCode = accessor.GetValue<string>(MessagePath.Get().Property("err-code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message")) ?? accessor.GetValue<string>(MessagePath.Get().Property("err-msg"));

            if (code > 0 && code != 200)
                return new ServerError(code.Value!, GetErrorInfo(code.Value!, msg));

            if (!string.IsNullOrEmpty(errCode))
                return new ServerError(errCode!, GetErrorInfo(errCode!, msg));

            return null;
        }

        #endregion

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        public IHTXRestClientSpotApiShared SharedClient => this;
    }
}
