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
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HuobiOrderUpdate = Huobi.Net.Objects.Models.Socket.HuobiOrderUpdate;

namespace Huobi.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class HuobiSocketClientSpotApi : SocketApiClient, IHuobiSocketClientSpotApi
    {
        #region fields
        #endregion

        #region ctor
        internal HuobiSocketClientSpotApi(ILogger logger, HuobiSocketOptions options)
            : base(logger, options.Environment.SocketBaseAddress, options, options.SpotOptions)
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
        public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(BaseAddress.AppendPath("ws"), request, false).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, symbol)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
            var result = await QueryAsync<HuobiSocketResponse<HuobiOrderBook>>(BaseAddress.AppendPath("ws"), request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiOrderBook>(result.Error!);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiOrderBook>(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
            var result = await QueryAsync<HuobiSocketResponse<HuobiIncementalOrderBook>>(BaseAddress.AppendPath("feed"), request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiIncementalOrderBook>(result.Error!);

            if (result.Data.Data == null)
            {
                var info = "No data received when requesting order book. " +
                    "Levels 5/20 are only supported for a subset of symbols, see https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update. Use 150 level instead.";
                _logger.Log(LogLevel.Debug, info);
                return new CallResult<HuobiIncementalOrderBook>(new ServerError(info));
            }

            return new CallResult<HuobiIncementalOrderBook>(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.As(data.Data.Data, symbol));
            });

            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);

            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.As(data.Data.Data, symbol));
            });

            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.refresh.{levels}");
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiIncementalOrderBook>>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.As(data.Data.Data, symbol));
            });

            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
            return await SubscribeAsync(BaseAddress.AppendPath("feed"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiSymbolTradeDetails>>>(BaseAddress.AppendPath("ws"), request, false).ConfigureAwait(false);
            return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiSymbolTradeDetails>>(result.Error!);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTrade>>>(data => onData(data.As(data.Data.Data, symbol)));
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
            var result = await QueryAsync<HuobiSocketResponse<HuobiSymbolDetails>>(BaseAddress.AppendPath("ws"), request, false).ConfigureAwait(false);
            if (!result)
                return result.AsError<HuobiSymbolDetails>(result.Error!);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolDetails>>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.As(data.Data.Data, symbol));
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), "market.tickers");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<IEnumerable<HuobiSymbolTicker>>>>(data =>
            {
                var result = new HuobiSymbolDatas { Timestamp = data.Timestamp, Ticks = data.Data.Data };
                onData(data.As(result));
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.ticker");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTick>>>(data =>
            {
                data.Data.Data.Symbol = symbol;
                onData(data.As(data.Data.Data));
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.bbo");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiBestOffer>>>(data =>
            {
                onData(data.As(data.Data.Data, symbol));
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws"), request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default)
        {
            symbol = symbol?.ValidateHuobiSymbol();
            var request = new HuobiAuthenticatedSubscribeRequest($"orders#{symbol ?? "*"}");
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                if (data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
                {
                    _logger.Log(LogLevel.Warning, "Invalid order update data: " + data);
                    return;
                }

                var eventType = data.Data["data"]!["eventType"]?.ToString();
                var symbol = data.Data["data"]!["symbol"]?.ToString();
                if (eventType == "trigger")
                {
                    DeserializeAndInvoke(data, onConditionalOrderTriggerFailure, symbol);
                }
                else if (eventType == "deletion")
                {
                    DeserializeAndInvoke(data, onConditionalOrderCanceled, symbol);
                }
                else if (eventType == "creation")
                {
                    DeserializeAndInvoke(data, onOrderSubmitted, symbol);
                }
                else if (eventType == "trade")
                {
                    DeserializeAndInvoke(data, onOrderMatched, symbol);
                }
                else if (eventType == "cancellation")
                {
                    DeserializeAndInvoke(data, onOrderCancelation, symbol);
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "Unknown order event type: " + eventType);
                }
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
        {
            if (updateMode != null && (updateMode > 2 || updateMode < 0))
                throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

            var request = new HuobiAuthenticatedSubscribeRequest("accounts.update#" + (updateMode ?? 1));
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                DeserializeAndInvoke(data, onAccountUpdate);
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default)
        {
            var request = new HuobiAuthenticatedSubscribeRequest($"trade.clearing#{symbol ?? "*"}#1");
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                if (data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
                {
                    _logger.Log(LogLevel.Warning, "Invalid order update data: " + data);
                    return;
                }

                var eventType = data.Data["data"]!["eventType"]?.ToString();
                var symbol = data.Data["data"]!["symbol"]?.ToString();
                if (eventType == "trade")
                {
                    DeserializeAndInvoke(data, onOrderMatch, symbol);
                }
                else if (eventType == "cancellation")
                {
                    DeserializeAndInvoke(data, onOrderCancel, symbol);
                }
                else
                {
                    _logger.Log(LogLevel.Warning, "Unknown order details event type: " + eventType);
                }
            });
            return await SubscribeAsync(BaseAddress.AppendPath("ws/v2"), request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        #region private

        private void DeserializeAndInvoke<T>(DataEvent<JToken> data, Action<DataEvent<T>>? action, string? symbol = null)
        {
            var obj = Deserialize<T>(data.Data["data"]!);
            if (!obj)
            {
                _logger.Log(LogLevel.Error, $"Failed to deserialize {typeof(T).Name}: " + obj.Error);
                return;
            }
            action?.Invoke(data.As(obj.Data, symbol));
        }


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
            if (s.ApiClient is HuobiSocketClientUsdtMarginSwapApi)
            {
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
            }
            else
            {
                await s.SendAndWaitAsync(((HuobiAuthenticationProvider)s.ApiClient.AuthenticationProvider).GetWebsocketAuthentication(s.ConnectionUri), ClientOptions.RequestTimeout, null, data =>
                {
                    if (data["ch"]?.ToString() != "auth")
                        return false;

                    var authResponse = Deserialize<HuobiAuthSubscribeResponse>(data);
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
            }

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
