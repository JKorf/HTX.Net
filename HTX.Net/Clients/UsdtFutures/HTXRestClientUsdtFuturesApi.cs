using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Interfaces.Clients.SpotApi;
using HTX.Net.Interfaces.Clients.UsdtFuturesApi;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients.UsdtFutures
{
    /// <inheritdoc />
    internal partial class HTXRestClientUsdtFuturesApi : RestApiClient<HTXEnvironment, HTXAuthenticationProvider, HTXCredentials>, IHTXRestClientUsdtFuturesApi
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;

        protected override IRestMessageHandler MessageHandler => new HTXRestMessageHandler(HTXErrors.FuturesMapping);

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
            : base(log, HTXExchange.Metadata.Id, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            Account = new HTXRestClientUsdtMarginSwapApiAccount(this);
            ExchangeData = new HTXRestClientUsdtFuturesApiExchangeData(this);
            SubAccount = new HTXRestClientUsdtFuturesApiSubAccount(this);
            Trading = new HTXRestClientUsdtFuturesApiTrading(this);

        }
        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        public IHTXRestClientUsdtFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override HTXAuthenticationProvider CreateAuthenticationProvider(HTXCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXApiResponseV2<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        internal async Task<HttpResult> SendBasicAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return HttpResult.Fail(result);

            if (!string.IsNullOrEmpty(result.Data.ErrorCode))
                return HttpResult.Fail(result, new ServerError(result.Data.ErrorCode!, GetErrorInfo(result.Data.ErrorCode!, result.Data.ErrorMessage)));

            return result;

        }

        internal async Task<HttpResult<T>> SendBasicAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return HttpResult.Fail<T>(result);

            if (!string.IsNullOrEmpty(result.Data.ErrorCode))
                return HttpResult.Fail<T>(result, new ServerError(result.Data.ErrorCode!, GetErrorInfo(result.Data.ErrorCode!, result.Data.ErrorMessage)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();
    }
}
