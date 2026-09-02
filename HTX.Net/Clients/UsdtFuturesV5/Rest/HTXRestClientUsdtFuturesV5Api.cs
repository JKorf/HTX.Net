using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using HTX.Net.Clients.MessageHandlers;
using HTX.Net.Interfaces.Clients.UsdtFuturesV5Api;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Options;

namespace HTX.Net.Clients.UsdtFuturesV5
{
    /// <inheritdoc />
    internal partial class HTXRestClientUsdtFuturesV5Api : RestApiClient<HTXEnvironment, HTXAuthenticationProvider, HTXCredentials>, IHTXRestClientUsdtFuturesV5Api
    {
        /// <inheritdoc />
        public new HTXRestOptions ClientOptions => (HTXRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => HTXErrors.FuturesMapping;

        protected override IRestMessageHandler MessageHandler => new HTXRestMessageHandler(HTXErrors.FuturesMapping);

        /// <inheritdoc />
        public string ExchangeName => "HTX";

        #region Api clients

        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesV5ApiAccount Account { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesV5ApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IHTXRestClientUsdtFuturesV5ApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HTXRestClientUsdtFuturesV5Api(ILoggerFactory? loggerFactory, HttpClient? httpClient, HTXRestOptions options)
            : base(loggerFactory, HTXExchange.Metadata.Id, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtFuturesV5Options)
        {
            Account = new HTXRestClientUsdtFuturesV5ApiAccount(this);
            ExchangeData = new HTXRestClientUsdtFuturesV5ApiExchangeData(this);
            Trading = new HTXRestClientUsdtFuturesV5ApiTrading(this);
        }
        #endregion

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(HTXExchange._serializerContext));

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

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync() => throw new NotImplementedException();
    }
}
