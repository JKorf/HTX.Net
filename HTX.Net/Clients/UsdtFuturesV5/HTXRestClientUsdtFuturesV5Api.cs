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
        internal HTXRestClientUsdtFuturesV5Api(ILogger log, HttpClient? httpClient, HTXRestOptions options)
            : base(log, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtFuturesV5Options)
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

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            where T : class
        {
            var result = await base.SendAsync<HTXApiResponseV2<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => Task.FromResult(new WebCallResult<DateTime>(null, null, null, null, null, null, null, null, null, null, null, ResultDataSource.Server, DateTime.UtcNow, null));
    }
}
