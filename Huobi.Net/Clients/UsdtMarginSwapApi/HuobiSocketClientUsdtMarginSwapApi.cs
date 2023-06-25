using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiSocketClientUsdtMarginSwapApi : SocketApiClient, IHuobiSocketClientUsdtMarginSwapApi
    {

        #region ctor
        internal HuobiSocketClientUsdtMarginSwapApi(ILogger logger, HuobiSocketOptions options)
            : base(logger, options.Environment.UsdtMarginSwapSocketBaseAddress, options, options.UsdtMarginSwapOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            SetDataInterpreter(DecompressData, null);
            AddGenericHandler("PingV1", PingHandlerV1);
            AddGenericHandler("PingV2", PingHandlerV2);
            AddGenericHandler("PingV3", PingHandlerV3);
        }

        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, false);

        #region methods

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.depth.step" + mergeStep);
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            var request = new HuobiIncrementalOrderBookSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.depth.size_{limit}.high_freq",
                snapshot ? "snapshot" : "incremental");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTickUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.bbo");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiBestOfferUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.trade.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapTradesUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("linear-swap-ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.premium_index.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.estimated_rate.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.basis.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}.{priceType}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiUsdtMarginSwapBasisUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(
                NextId().ToString(CultureInfo.InvariantCulture),
                $"market.{contractCode}.mark_price.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws_index"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        // WIP

        ///// <inheritdoc />
        //public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        //{
        //    var request = new HuobiSocketRequest2(
        //        "sub",
        //        NextId().ToString(CultureInfo.InvariantCulture),
        //        $"orders.*");
        //    var internalHandler = new Action<DataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        //    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        //}

        ///// <inheritdoc />
        //public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
        //{
        //    var request = new HuobiSocketRequest2(
        //        "sub",
        //        NextId().ToString(CultureInfo.InvariantCulture),
        //        $"orders.{contractCode}");
        //    var internalHandler = new Action<DataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        //    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        //}

        ///// <inheritdoc />
        //public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(Action<DataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
        //{
        //    var request = new HuobiSocketRequest2(
        //        "sub",
        //        NextId().ToString(CultureInfo.InvariantCulture),
        //        $"orders_cross.*");
        //    var internalHandler = new Action<DataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
        //    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        //}

        ///// <inheritdoc />
        //public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(string contractCode, Action<DataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
        //{
        //    var request = new HuobiSocketRequest2(
        //        "sub",
        //        NextId().ToString(CultureInfo.InvariantCulture),
        //        $"orders_cross.{contractCode}");
        //    var internalHandler = new Action<DataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
        //    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
        //}

        #region private

        private void PingHandlerV1(MessageEvent messageEvent)
        {
            var v1Ping = messageEvent.JsonData["ping"] != null;

            if (v1Ping)
                messageEvent.Connection.Send(new HuobiPingResponse(messageEvent.JsonData["ping"]!.Value<long>()));
        }

        private void PingHandlerV2(MessageEvent messageEvent)
        {
            var v2Ping = messageEvent.JsonData["action"]?.ToString() == "ping";

            if (v2Ping)
                messageEvent.Connection.Send(new HuobiPingAuthResponse(messageEvent.JsonData["data"]!["ts"]!.Value<long>()));
        }

        private void PingHandlerV3(MessageEvent messageEvent)
        {
            var v3Ping = messageEvent.JsonData["op"]?.ToString() == "ping";

            if (v3Ping)
            {
                messageEvent.Connection.Send(new
                {
                    op = "pong",
                    ts = messageEvent.JsonData["ts"]?.ToString()
                });
            }
        }

        private static string DecompressData(byte[] byteData)
        {
            using var decompressedStream = new MemoryStream();
            using var compressedStream = new MemoryStream(byteData);
            using var deflateStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            deflateStream.CopyTo(decompressedStream);
            decompressedStream.Position = 0;

            using var streamReader = new StreamReader(decompressedStream);
            return streamReader.ReadToEnd();
        }
        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default(T)!);
            var v1Data = (data["data"] != null || data["tick"] != null) && data["rep"] != null;
            var v1Error = data["status"] != null && data["status"]!.ToString() == "error";
            var isV1QueryResponse = v1Data || v1Error;
            if (isV1QueryResponse)
            {
                var hRequest = (HuobiSocketRequest)request;
                var id = data["id"];
                if (id == null)
                    return false;

                if (id.ToString() != hRequest.Id)
                    return false;

                if (v1Error)
                {
                    var error = new ServerError(data["err-msg"]!.ToString());
                    callResult = new CallResult<T>(error);
                    return true;
                }

                var desResult = Deserialize<T>(data);
                if (!desResult)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Failed to deserialize data: {desResult.Error}. Data: {data}");
                    callResult = new CallResult<T>(desResult.Error!);
                    return true;
                }

                callResult = new CallResult<T>(desResult.Data);
                return true;
            }

            var action = data["action"]?.ToString();
            var isV2Response = action == "req";
            if (isV2Response)
            {
                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                var channel = data["ch"]?.ToString();
                if (channel != hRequest.Channel)
                    return false;

                var desResult = Deserialize<T>(data);
                if (!desResult)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            var status = message["status"]?.ToString();
            var isError = status == "error";
            if (isError)
            {
                if (request is HuobiSubscribeRequest hRequest)
                {
                    var subResponse = Deserialize<HuobiSubscribeResponse>(message);
                    if (!subResponse)
                    {
                        _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Id;
                    if (id != hRequest.Id)
                        return false; // Not for this request

                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                {
                    var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                    if (!subResponse)
                    {
                        _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Error);
                        callResult = new CallResult<object>(subResponse.Error!);
                        return false;
                    }

                    var id = subResponse.Data.Channel;
                    if (id != haRequest.Channel)
                        return false; // Not for this request

                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Data.Code);
                    callResult = new CallResult<object>(new ServerError(subResponse.Data.Code, "Failed to subscribe"));
                    return true;
                }
            }

            var v1Sub = message["subbed"] != null;
            if (v1Sub)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(message);
                if (!subResponse)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiSubscribeRequest)request;
                if (subResponse.Data.Id != hRequest.Id)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                _logger.Log(LogLevel.Debug, $"Socket {s.SocketId} Subscription completed");
                callResult = new CallResult<object>(subResponse.Data);
                return true;
            }

            var action = message["action"]?.ToString();
            var v2Sub = action == "sub";
            if (v2Sub)
            {
                var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                if (!subResponse)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(subResponse.Error!);
                    return false;
                }

                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                if (subResponse.Data.Channel != hRequest.Channel)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Data.Message);
                    callResult = new CallResult<object>(new ServerError(subResponse.Data.Code, subResponse.Data.Message));
                    return true;
                }

                _logger.Log(LogLevel.Debug, $"Socket {s.SocketId} Subscription completed");
                callResult = new CallResult<object>(subResponse.Data);
                return true;
            }

            var operation = message["op"]?.ToString();
            var usdtMarginSub = operation == "sub";
            if (usdtMarginSub)
            {
                var subResponse = Deserialize<HuobiSocketResponse2>(message);
                if (!subResponse)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(subResponse.Error!);
                    return false;
                }

                var hRequest = (HuobiSocketRequest2)request;
                if (subResponse.Data.Topic != hRequest.Topic)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(new ServerError(subResponse.Data.ErrorCode + " - " + subResponse.Data.ErrorMessage));
                    return true;
                }

                _logger.Log(LogLevel.Debug, $"Socket {s.SocketId} Subscription completed");
                callResult = new CallResult<object>(subResponse.Data);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            if (request is HuobiSubscribeRequest hRequest)
                return hRequest.Topic == message["ch"]?.ToString();

            if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                return haRequest.Channel == message["ch"]?.ToString();

            if (request is HuobiSocketRequest2 hRequest2)
            {
                if (hRequest2.Topic == message["topic"]?.ToString())
                    return true;

                if (hRequest2.Topic.Contains("*") && hRequest2.Topic.Split('.')[0] == message["topic"]?.ToString().Split('.')[0])
                    return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            if (identifier == "PingV1" && message["ping"] != null)
                return true;

            if (identifier == "PingV2" && message["action"]?.ToString() == "ping")
                return true;

            if (identifier == "PingV3" && message["op"]?.ToString() == "ping")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            if (s.ApiClient.AuthenticationProvider == null)
                return new CallResult<bool>(new NoApiCredentialsError());

            var result = new CallResult<bool>(new ServerError("No response from server"));
            await s.SendAndWaitAsync(((HuobiAuthenticationProvider)s.ApiClient.AuthenticationProvider).GetWebsocketAuthentication2(s.ConnectionUri), ClientOptions.RequestTimeout, null, data =>
            {
                if (data["op"]?.ToString() != "auth")
                    return false;

                var authResponse = Deserialize<HuobiAuthResponse>(data);
                if (!authResponse)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(authResponse.Error!);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    _logger.Log(LogLevel.Warning, $"Socket {s.SocketId} Authorization failed: " + authResponse.Data.Message);
                    result = new CallResult<bool>(new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                    return true;
                }

                _logger.Log(LogLevel.Debug, $"Socket {s.SocketId} Authorization completed");
                result = new CallResult<bool>(true);
                return true;
            }).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            var result = false;
            if (s.Request is HuobiSubscribeRequest hRequest)
            {
                var unsubId = NextId().ToString();
                var unsub = new HuobiUnsubscribeRequest(unsubId, hRequest.Topic);

                await connection.SendAndWaitAsync(unsub, ClientOptions.RequestTimeout, null, data =>
                {
                    if (data.Type != JTokenType.Object)
                        return false;

                    var id = data["id"]?.ToString();
                    if (id == unsubId)
                    {
                        result = data["status"]?.ToString() == "ok";
                        return true;
                    }

                    return false;
                }).ConfigureAwait(false);
                return result;
            }

            if (s.Request is HuobiAuthenticatedSubscribeRequest haRequest)
            {
                var unsub = new Dictionary<string, object>()
                {
                    { "action", "unsub" },
                    { "ch", haRequest.Channel },
                };

                await connection.SendAndWaitAsync(unsub, ClientOptions.RequestTimeout, null, data =>
                {
                    if (data.Type != JTokenType.Object)
                        return false;

                    if (data["action"]?.ToString() == "unsub" && data["ch"]?.ToString() == haRequest.Channel)
                    {
                        result = data["code"]?.Value<int>() == 200;
                        return true;
                    }

                    return false;
                }).ConfigureAwait(false);
                return result;
            }

            if (s.Request is HuobiSocketRequest2 hRequest2)
            {
                var unsub = new
                {
                    op = "unsub",
                    topic = hRequest2.Topic,
                    cid = NextId().ToString()
                };
                await connection.SendAndWaitAsync(unsub, ClientOptions.RequestTimeout, null, data =>
                {
                    if (data.Type != JTokenType.Object)
                        return false;

                    if (data["op"]?.ToString() == "unsub" && data["cid"]?.ToString() == unsub.cid)
                    {
                        result = data["err-code"]?.Value<int>() == 0;
                        return true;
                    }

                    return false;
                }).ConfigureAwait(false);
                return result;
            }

            throw new InvalidOperationException("Unknown request type");
        }
        #endregion
        #endregion

    }
}
