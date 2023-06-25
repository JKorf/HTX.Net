using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.UsdtMarginSwapApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApi : RestApiClient, IHuobiClientUsdtMarginSwapApi
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

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApiAccount Account { get; }
        /// <inheritdoc />
        public HuobiClientUsdtMarginSwapApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public HuobiClientUsdtMarginSwapApiTrading Trading { get; }

        #endregion

        #region constructor/destructor
        internal HuobiClientUsdtMarginSwapApi(ILogger log, HttpClient? httpClient, HuobiRestOptions options)
            : base(log, httpClient, options.Environment.UsdtMarginSwapRestBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            Account = new HuobiClientUsdtMarginSwapApiAccount(this);
            ExchangeData = new HuobiClientUsdtMarginSwapApiExchangeData(this);
            Trading = new HuobiClientUsdtMarginSwapApiTrading(this);

            manualParseError = true;
        }
        #endregion

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

        internal async Task<WebCallResult<DateTime>> SendTimestampRequestAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<string>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<DateTime>(result.Error!);

            return result.As(result.Data.Timestamp);
        }


        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<ServerError?> TryParseErrorAsync(JToken data)
        {
            if (data["code"] != null && data["code"]?.Value<int>() != 200)
            {
                if (data["err-code"] != null)
                    return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));

                return Task.FromResult<ServerError?>(new ServerError($"{(string)data["code"]!}, {(string)data["message"]!}"));
            }

            if (data["err-code"] == null && data["err-msg"] == null)
                return Task.FromResult<ServerError?>(null);

            return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["err-code"] == null || error["err-msg"] == null)
                return new ServerError(error.ToString());

            return new ServerError($"{(string)error["err-code"]!}, {(string)error["err-msg"]!}");
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
