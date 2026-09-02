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
    internal partial class HTXRestClientSpotApi : RestApiClient<HTXEnvironment, HTXAuthenticationProvider, HTXCredentials>, IHTXRestClientSpotApi
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

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
        internal HTXRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, HTXRestOptions options)
            : base(loggerFactory, HTXExchange.Metadata.Id, httpClient, options.Environment.RestBaseAddress, options, options.SpotOptions)
        {
            Account = new HTXRestClientSpotApiAccount(this);
            ExchangeData = new HTXRestClientSpotApiExchangeData(this);
            SubAccount = new HTXRestClientSpotApiSubAccount(this);
            Margin = new HTXRestClientSpotApiMargin(this);
            Trading = new HTXRestClientSpotApiTrading(this);

        }
        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => HTXExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override HTXAuthenticationProvider CreateAuthenticationProvider(HTXCredentials credentials)
            => new HTXAuthenticationProvider(credentials, ClientOptions.SignPublicRequests);

        #region methods
        internal async Task<HttpResult<T>> SendRawAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) 
        {
            var result = await base.SendAsync<HTXApiResponseV2<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        internal async Task<HttpResult<(T, DateTime)>> SendTimestampAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<HTXBasicResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success || result.Data == null)
                return HttpResult.Fail<(T, DateTime)>(result);

            if (result.Data.ErrorCode != null)
                return HttpResult.Fail<(T, DateTime)>(result, new ServerError(result.Data.ErrorCode, GetErrorInfo(result.Data.ErrorCode, result.Data.ErrorMessage)));

            return HttpResult.Ok(result, (result.Data.Data, result.Data.Timestamp));
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

        #endregion

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        public IHTXRestClientSpotApiShared SharedClient => this;
    }
}
