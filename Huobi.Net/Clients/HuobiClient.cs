using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Clients.FuturesApi;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Clients.SwapsApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.FuturesApi;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.SwapsApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Internal.Futures;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiClient" />
    public class HuobiClient : BaseRestClient, IHuobiClient
    {
        #region Api clients

        /// <inheritdoc />
        public IHuobiClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IHuobiClientFuturesCoinApi FuturesCoinApi { get; }
        /// <inheritdoc />
        public IHuobiClientSwapsCoinApi SwapsCoinApi { get; }
        /// <inheritdoc />
        public IHuobiClientFuturesUsdtApi FuturesUsdtApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of HuobiClient using the default options
        /// </summary>
        public HuobiClient() : this(HuobiClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of the HuobiClient with the provided options
        /// </summary>
        public HuobiClient(HuobiClientOptions options) : base("Huobi", options)
        {
            manualParseError = true;

            SpotApi = AddApiClient(new HuobiClientSpotApi(log, this, options));
            FuturesCoinApi = AddApiClient(new HuobiClientFuturesCoinApi(log, this, options));
            SwapsCoinApi = AddApiClient(new HuobiClientSwapsCoinApi(log, this, options));
            FuturesUsdtApi = AddApiClient(new HuobiClientFuturesUsdtApi(log, this, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(HuobiClientOptions options)
        {
            HuobiClientOptions.Default = options;
        }

        internal async Task<WebCallResult<T>> SendHuobiV2Request<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiApiResponseV2<T>>(apiClient, uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 200)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(apiClient, uri, method, cancellationToken, parameters, signed).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<(T, DateTime)>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<(T, DateTime)>(new ServerError($"{result.Data.ErrorCode}-{result.Data.ErrorMessage}"));

            return result.As((result.Data.Data, result.Data.Timestamp));
        }

        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(apiClient, uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<T>> SendHuobiFuturesRequest<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1)
        {
            var result = await SendRequestAsync<HuobiBasicFuturesResponse<T>>(apiClient, uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1).ConfigureAwait(false);
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
                return Task.FromResult<ServerError?>(new ServerError($"{(string)data["code"]!}, {(string)data["message"]!}"));

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
        #endregion
    }
}
