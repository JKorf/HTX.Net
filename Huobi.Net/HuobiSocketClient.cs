using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Huobi.Net.Interfaces;
using System.Globalization;
using Huobi.Net.Objects.SocketObjects.V2;
using HuobiOrderUpdate = Huobi.Net.Objects.SocketObjects.V2.HuobiOrderUpdate;
using Microsoft.Extensions.Logging;
using CryptoExchange.Net.Authentication;
using Huobi.Net.Enums;
using System.Threading;

namespace Huobi.Net
{
    /// <summary>
    /// Client for the Huobi socket API
    /// </summary>
    public class HuobiSocketClient : SocketClient, IHuobiSocketClient
    {
        #region fields
        private static HuobiSocketClientOptions defaultOptions = new HuobiSocketClientOptions();
        private static HuobiSocketClientOptions DefaultOptions => defaultOptions.Copy();

        private readonly string baseAddressAuthenticated;
        private readonly string baseAddressMbp;
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of HuobiSocketClient with default options
        /// </summary>
        public HuobiSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of HuobiSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public HuobiSocketClient(HuobiSocketClientOptions options) : base("Huobi", options, options.ApiCredentials == null ? null : new HuobiAuthenticationProvider(options.ApiCredentials, false))
        {
            baseAddressAuthenticated = options.BaseAddressAuthenticated;
            baseAddressMbp = options.BaseAddressInrementalOrderBook;

            SetDataInterpreter(DecompressData, null);
            AddGenericHandler("PingV1", PingHandlerV1);
            AddGenericHandler("PingV2", PingHandlerV2);
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the API key and secret
        /// </summary>
        /// <param name="apiKey">The api key</param>
        /// <param name="apiSecret">The api secret</param>
        public void SetApiCredentials(string apiKey, string apiSecret)
        {
            SetAuthenticationProvider(new HuobiAuthenticationProvider(new ApiCredentials(apiKey, apiSecret), false));
        }

        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(HuobiSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(request, false).ConfigureAwait(false);
            return new CallResult<IEnumerable<HuobiKline>>(result.Data?.Data, result.Error);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, symbol)));
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
            var result = await QueryAsync<HuobiSocketResponse<HuobiOrderBook>>(request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiOrderBook>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiOrderBook>(result.Data.Data, null);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
        {
            symbol = symbol.ValidateHuobiSymbol();
            levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
            var result = await QueryAsync<HuobiSocketResponse<HuobiIncementalOrderBook>>(baseAddressMbp, request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiIncementalOrderBook>(null, result.Error);

            if (result.Data.Data == null)
            {
                var info = $"No data received when requesting order book. " +
                    $"Levels 5/20 are only supported for a subset of symbols, see https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update. Use 150 level instead.";
                log.Write(LogLevel.Debug, info);
                return new CallResult<HuobiIncementalOrderBook>(null, new ServerError(info));
            }

            return new CallResult<HuobiIncementalOrderBook>(result.Data.Data, null);
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
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
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
            return await SubscribeAsync(baseAddressMbp, request, null, false, internalHandler, ct).ConfigureAwait(false);
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
            return await SubscribeAsync(baseAddressMbp, request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiSymbolTradeDetails>>>(request, false).ConfigureAwait(false);
            return new CallResult<IEnumerable<HuobiSymbolTradeDetails>>(result.Data?.Data, result.Error);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTrade>>>(data => onData(data.As(data.Data.Data, symbol)));
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
            var result = await QueryAsync<HuobiSocketResponse<HuobiSymbolDetails>>(request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiSymbolDetails>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiSymbolDetails>(result.Data.Data, null);
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
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), "market.tickers");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<IEnumerable<HuobiSymbolTicker>>>>(data =>
            {
                var result = new HuobiSymbolDatas { Timestamp = data.Timestamp, Ticks = data.Data.Data};
                onData(data.As(result));
            });
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.ticker");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiSymbolTick>>>(data =>
            {
                onData(data.As(data.Data.Data));
            });
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.bbo");
            var internalHandler = new Action<DataEvent<HuobiDataEvent<HuobiBestOffer>>>(data =>
            {
                onData(data.As(data.Data.Data, symbol));
            });
            return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
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
            var request = new HuobiAuthenticatedSubscribeRequest( $"orders#{symbol ?? "*"}");
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                if (data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
                {
                    log.Write(LogLevel.Warning, "Invalid order update data: " + data);
                    return;
                }

                var eventType = data.Data["data"]!["eventType"]?.ToString();
                var symbol = data.Data["data"]!["symbol"]?.ToString();
                if (eventType == "trigger")
                    DeserializeAndInvoke(data, onConditionalOrderTriggerFailure, symbol);

                else if (eventType == "deletion")
                    DeserializeAndInvoke(data, onConditionalOrderCanceled, symbol);

                else if (eventType == "creation")
                    DeserializeAndInvoke(data, onOrderSubmitted, symbol);

                else if (eventType == "trade")
                    DeserializeAndInvoke(data, onOrderMatched, symbol);

                else if (eventType == "cancellation")
                    DeserializeAndInvoke(data, onOrderCancelation, symbol);
                else
                {
                    log.Write(LogLevel.Warning, "Unknown order event type: " + eventType);
                }
            });
            return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, CancellationToken ct = default)
        {
            var request = new HuobiAuthenticatedSubscribeRequest("accounts.update#1");
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                DeserializeAndInvoke(data, onAccountUpdate);
            });
            return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default)
        {
            var request = new HuobiAuthenticatedSubscribeRequest($"trade.clearing#{symbol ?? "*"}#1");
            var internalHandler = new Action<DataEvent<JToken>>(data =>
            {
                if(data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
                {
                    log.Write(LogLevel.Warning, "Invalid order update data: " + data);
                    return;
                }

                var eventType = data.Data["data"]!["eventType"]?.ToString();
                var symbol = data.Data["data"]!["symbol"]?.ToString();
                if (eventType == "trade")
                    DeserializeAndInvoke(data, onOrderMatch, symbol);
                else if(eventType == "cancellation")
                    DeserializeAndInvoke(data, onOrderCancel, symbol);
                else
                {
                    log.Write(LogLevel.Warning, "Unknown order details event type: " + eventType);
                }
            });
            return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
        }

        #region private

        private void DeserializeAndInvoke<T>(DataEvent<JToken> data, Action<DataEvent<T>>? action, string? symbol = null)
        {
            var obj = Deserialize<T>(data.Data["data"]!);
            if (!obj)
            {
                log.Write(LogLevel.Error, $"Failed to deserialize {typeof(T).Name}: " + obj.Error);
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
        
        /// <inheritdoc />
        protected override SocketConnection GetSocketConnection(string address, bool authenticated)
        {
            address = authenticated ? baseAddressAuthenticated : address;
            var socketResult = sockets.Where(s => s.Value.Socket.Url.TrimEnd('/') == address.TrimEnd('/') && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < ClientOptions.SocketSubscriptionsCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= ClientOptions.SocketSubscriptionsCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketWrapper = new SocketConnection(this, socket);
            foreach (var kvp in genericHandlers)
                socketWrapper.AddSubscription(SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value));
            return socketWrapper;
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
        #endregion
        #endregion

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(default, null);
            var v1Data = (data["data"] != null || data["tick"] != null) && data["rep"] != null;
            var v1Error = data["status"] != null && data["status"]!.ToString() == "error";
            var isV1QueryResponse = v1Data || v1Error;
            if (isV1QueryResponse)
            {
                var hRequest = (HuobiSocketRequest) request;
                var id = data["id"];
                if (id == null)
                    return false;

                if (id.ToString() != hRequest.Id)
                    return false;

                if (v1Error)
                {
                    var error = new ServerError(data["err-msg"]!.ToString());
                    callResult = new CallResult<T>(default, error);
                    return true;
                }

                var desResult = Deserialize<T>(data);
                if (!desResult)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
                    callResult = new CallResult<T>(default, desResult.Error);
                    return true;
                }

                callResult = new CallResult<T>(desResult.Data, null);
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
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
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
                        log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Id;
                    if (id != hRequest.Id)
                        return false; // Not for this request

                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(null, new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                {
                    var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                    if (!subResponse)
                    {
                        log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                        callResult = new CallResult<object>(null, subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Channel;
                    if (id != haRequest.Channel)
                        return false; // Not for this request

                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.Code);
                    callResult = new CallResult<object>(null, new ServerError(subResponse.Data.Code, "Failed to subscribe"));
                    return true;
                }
            }

            var v1Sub = message["subbed"] != null;
            if (v1Sub)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(message);
                if (!subResponse)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiSubscribeRequest)request;
                if (subResponse.Data.Id != hRequest.Id)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(null, new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Subscription completed");
                callResult = new CallResult<object>(subResponse.Data, null);
                return true;
            }

            var action = message["action"]?.ToString();
            var v2Sub = action == "sub";
            if (v2Sub)
            {
                var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                if (!subResponse)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(null, subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                if (subResponse.Data.Channel != hRequest.Channel)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.Message);
                    callResult = new CallResult<object>(null, new ServerError(subResponse.Data.Code, subResponse.Data.Message));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Subscription completed");
                callResult = new CallResult<object>(subResponse.Data, null);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (request is HuobiSubscribeRequest hRequest)
                return hRequest.Topic == message["ch"]?.ToString();
            
            if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                return haRequest.Channel == message["ch"]?.ToString();
            
            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            if (identifier == "PingV1" && message["ping"] != null)
                return true;

            if (identifier == "PingV2" && message["action"]?.ToString() == "ping")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var authParams = ((HuobiAuthenticationProvider)authProvider).SignRequest(
                baseAddressAuthenticated,
                HttpMethod.Get, 
                new Dictionary<string, object>(), 
                "accessKey",
                "signatureMethod",
                "signatureVersion",
                "timestamp",
                "signature",
                2.1);
            var authObjects = new HuobiAuthenticationRequest(authProvider.Credentials.Key!.GetString(),
                (string)authParams["timestamp"],
                (string)authParams["signature"]);

            var result = new CallResult<bool>(false, new ServerError("No response from server"));
            await s.SendAndWaitAsync(authObjects, ClientOptions.SocketResponseTimeout, data =>
            {
                if (data["ch"]?.ToString() != "auth")
                    return false;

                var authResponse = Deserialize<HuobiAuthSubscribeResponse>(data);
                if (!authResponse)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Authorization failed: " + authResponse.Data.Message);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Authorization completed");
                result = new CallResult<bool>(true, null);
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

                await connection.SendAndWaitAsync(unsub, ClientOptions.SocketResponseTimeout, data =>
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

                await connection.SendAndWaitAsync(unsub, ClientOptions.SocketResponseTimeout, data =>
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

            throw new InvalidOperationException("Unknown request type");
        }
    }
}
