using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
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
        public HuobiSocketClient(HuobiSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new HuobiAuthenticationProvider(options.ApiCredentials, false))
        {
            baseAddressAuthenticated = options.BaseAddressAuthenticated;

            SetDataInterpreter(DecompressData, null);
            AddGenericHandler("PingV1", PingHandlerV1);
            AddGenericHandler("PingV2", PingHandlerV2);
        }
        #endregion

        #region methods

        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(HuobiSocketClientOptions options)
        {
            defaultOptions = options;
        }

        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        public CallResult<IEnumerable<HuobiKline>> GetKlines(string symbol, HuobiPeriod period) => GetKlinesAsync(symbol, period).Result;
        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, HuobiPeriod period)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var result = await Query<HuobiSocketResponse<IEnumerable<HuobiKline>>>(request, false).ConfigureAwait(false);
            return new CallResult<IEnumerable<HuobiKline>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToKlineUpdates(string symbol, HuobiPeriod period, Action<HuobiKline> onData) => SubscribeToKlineUpdatesAsync(symbol, period, onData).Result;
        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, HuobiPeriod period, Action<HuobiKline> onData)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiKline>>(data => onData(data.Data));
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public CallResult<HuobiOrderBook> GetOrderBook(string symbol, int mergeStep) => GetOrderBookAsync(symbol, mergeStep).Result;
        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int mergeStep)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
            var result = await Query<HuobiSocketResponse<HuobiOrderBook>>(request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiOrderBook>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiOrderBook>(result.Data.Data, null);
        }

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(string symbol, int mergeStep, Action<HuobiOrderBook> onData) => SubscribeToOrderBookUpdatesAsync(symbol, mergeStep, onData).Result;
        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int mergeStep, Action<HuobiOrderBook> onData)
        {
            symbol = symbol.ValidateHuobiSymbol();
            mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

            var internalHandler = new Action<HuobiSocketUpdate<HuobiOrderBook>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.Data);
            });

            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        public CallResult<IEnumerable<HuobiSymbolTradeDetails>> GetTrades(string symbol) => GetTradesAsync(symbol).Result;
        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradesAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var result = await Query<HuobiSocketResponse<IEnumerable<HuobiSymbolTradeDetails>>>(request, false).ConfigureAwait(false);
            return new CallResult<IEnumerable<HuobiSymbolTradeDetails>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToTradeUpdates(string symbol, Action<HuobiSymbolTrade> onData) => SubscribeToTradeUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<HuobiSymbolTrade> onData)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiSymbolTrade>>(data => onData(data.Data));
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets details for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        public CallResult<HuobiSymbolDetails> GetSymbolDetails(string symbol) => GetSymbolDetailsAsync(symbol).Result;
        /// <summary>
        /// Gets details for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
            var result = await Query<HuobiSocketResponse<HuobiSymbolDetails>>(request, false).ConfigureAwait(false);
            if (!result)
                return new CallResult<HuobiSymbolDetails>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiSymbolDetails>(result.Data.Data, null);
        }

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToSymbolDetailUpdates(string symbol, Action<HuobiSymbolDetails> onData) => SubscribeToSymbolDetailUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<HuobiSymbolDetails> onData)
        {
            symbol = symbol.ValidateHuobiSymbol();
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiSymbolDetails>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.Data);
            });
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to updates for all tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToTickerUpdates(Action<HuobiSymbolTicks> onData) => SubscribeToSymbolTickerUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribes to updates for all tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(Action<HuobiSymbolTicks> onData)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), "market.tickers");
            var internalHandler = new Action<HuobiSocketUpdate<IEnumerable<HuobiSymbolTick>>>(data =>
            {
                var result = new HuobiSymbolTicks {Timestamp = data.Timestamp, Ticks = data.Data};
                onData(result);
            });
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onData">Data handler</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription>
            SubscribeToBestOfferUpdates(string symbol, Action<HuobiBestOffer> onData) =>
            SubscribeToBestOfferUpdatesAsync(symbol, onData).Result;

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onData">Data handler</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<HuobiBestOffer> onData)
        {
            var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.bbo");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiBestOffer>>(data =>
            {
                onData(data.Data);
            });
            return await Subscribe(request, null, false, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates of orders
        /// </summary>
        /// <param name="symbol">Subscribe on a specific symbol</param>
        /// <param name="onOrderSubmitted">Event handler for the order submitted event</param>
        /// <param name="onOrderMatched">Event handler for the order matched event</param>
        /// <param name="onOrderCancellation">Event handler for the order cancelled event</param>
        /// <param name="onConditionalOrderTriggerFailure">Event handler for the conditional order trigger failed event</param>
        /// <param name="onConditionalOrderCancelled">Event handler for the condition order cancelled event</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(
            string? symbol = null,
            Action<HuobiSubmittedOrderUpdate>? onOrderSubmitted = null,
            Action<HuobiMatchedOrderUpdate>? onOrderMatched = null,
            Action<HuobiCancelledOrderUpdate>? onOrderCancellation = null,
            Action<HuobiTriggerFailureOrderUpdate>? onConditionalOrderTriggerFailure = null,
            Action<HuobiOrderUpdate>? onConditionalOrderCancelled = null) => SubscribeToOrderUpdatesAsync(symbol, onOrderSubmitted, onOrderMatched, onOrderCancellation, onConditionalOrderTriggerFailure, onConditionalOrderCancelled).Result;

        /// <summary>
        /// Subscribe to updates of orders
        /// </summary>
        /// <param name="symbol">Subscribe on a specific symbol</param>
        /// <param name="onOrderSubmitted">Event handler for the order submitted event</param>
        /// <param name="onOrderMatched">Event handler for the order matched event</param>
        /// <param name="onOrderCancellation">Event handler for the order cancelled event</param>
        /// <param name="onConditionalOrderTriggerFailure">Event handler for the conditional order trigger failed event</param>
        /// <param name="onConditionalOrderCancelled">Event handler for the condition order cancelled event</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<HuobiSubmittedOrderUpdate>? onOrderSubmitted = null,
            Action<HuobiMatchedOrderUpdate>? onOrderMatched = null,
            Action<HuobiCancelledOrderUpdate>? onOrderCancellation = null,
            Action<HuobiTriggerFailureOrderUpdate>? onConditionalOrderTriggerFailure = null,
            Action<HuobiOrderUpdate>? onConditionalOrderCancelled = null)
        {
            symbol = symbol?.ValidateHuobiSymbol();
            var request = new HuobiAuthenticatedSubscribeRequest( $"orders#{symbol ?? "*"}");
            var internalHandler = new Action<JToken>(data =>
            {
                var eventType = (string) data["data"]["eventType"];
                if (eventType == "trigger")
                    DeserializeAndInvoke(data, onConditionalOrderTriggerFailure);

                else if (eventType == "deletion")
                    DeserializeAndInvoke(data, onConditionalOrderCancelled);

                else if (eventType == "creation")
                    DeserializeAndInvoke(data, onOrderSubmitted);

                else if (eventType == "trade")
                    DeserializeAndInvoke(data, onOrderMatched);

                else if (eventType == "cancellation")
                    DeserializeAndInvoke(data, onOrderCancellation);
                else
                {
                    log.Write(LogVerbosity.Warning, "Unknown order event type: " + eventType);
                }
            });
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }


        /// <summary>
        /// Subscribe to updates of account balances
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAccountUpdates(Action<HuobiAccountUpdate> onAccountUpdate) => SubscribeToAccountUpdatesAsync(onAccountUpdate).Result;

        /// <summary>
        /// Subscribe to updates of account balances
        /// </summary>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<HuobiAccountUpdate> onAccountUpdate)
        {
            var request = new HuobiAuthenticatedSubscribeRequest("accounts.update#1");
            var internalHandler = new Action<JToken>(data =>
            {
                DeserializeAndInvoke(data, onAccountUpdate);
            });
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to detailed order matched/cancelled updates
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order cancelled event</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderDetailsUpdates(string? symbol = null, Action<HuobiTradeUpdate>? onOrderMatch = null, Action<HuobiOrderCancellationUpdate>? onOrderCancel = null) => SubscribeToOrderDetailsUpdatesAsync(symbol, onOrderMatch, onOrderCancel).Result;

        /// <summary>
        /// Subscribe to detailed order matched/cancelled updates
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order cancelled event</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null, Action<HuobiTradeUpdate>? onOrderMatch = null, Action<HuobiOrderCancellationUpdate>? onOrderCancel = null)
        {
            var request = new HuobiAuthenticatedSubscribeRequest($"trade.clearing#{symbol ?? "*"}#1");
            var internalHandler = new Action<JToken>(data =>
            {
                var eventType = (string)data["data"]["eventType"];
                if (eventType == "trade")
                    DeserializeAndInvoke(data, onOrderMatch);
                else if(eventType == "cancellation")
                    DeserializeAndInvoke(data, onOrderCancel);
                else
                {
                    log.Write(LogVerbosity.Warning, "Unknown order details event type: " + eventType);
                }
            });
            return await Subscribe(request, null, true, internalHandler).ConfigureAwait(false);
        }

        #region private

        private void DeserializeAndInvoke<T>(JToken data, Action<T>? action)
        {
            var obj = Deserialize<T>(data["data"], true);
            if (!obj)
            {
                log.Write(LogVerbosity.Error, $"Failed to deserialize {typeof(T).Name}: " + obj.Error);
                return;
            }
            action?.Invoke(obj.Data);
        }

        private void PingHandlerV1(SocketConnection connection, JToken data)
        {
            var v1Ping = data["ping"] != null;

            if (v1Ping)
                connection.Send(new HuobiPingResponse((long)data["ping"]));
        }

        private void PingHandlerV2(SocketConnection connection, JToken data)
        {
            var v2Ping = (string)data["action"] == "ping";

            if (v2Ping)
                connection.Send(new HuobiPingAuthResponse((long)data["data"]["ts"]));
        }
        
        /// <inheritdoc />
        protected override SocketConnection GetWebsocket(string address, bool authenticated)
        {
            address = authenticated ? baseAddressAuthenticated : BaseAddress;
            var socketResult = sockets.Where(s => s.Value.Socket.Url.TrimEnd('/') == address.TrimEnd('/') && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.HandlerCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.HandlerCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.HandlerCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketWrapper = new SocketConnection(this, socket);
            foreach (var kvp in genericHandlers)
                socketWrapper.AddHandler(SocketSubscription.CreateForIdentifier(kvp.Key, false, kvp.Value));
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
            var v1Error = data["status"] != null && (string)data["status"] == "error";
            var isV1QueryResponse = v1Data || v1Error;
            if (isV1QueryResponse)
            {
                var hRequest = (HuobiSocketRequest) request;
                if ((string) data["id"] != hRequest.Id)
                    return false;

                var desResult = Deserialize<T>(data, false);
                if (!desResult)
                {
                    log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    callResult = new CallResult<T>(default, desResult.Error);
                    return true;
                }

                callResult = new CallResult<T>(desResult.Data, null);
            }
            
            var isV2Response = (string)data["action"] == "req";
            if (isV2Response)
            {
                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                if ((string)data["ch"] != hRequest.Channel)
                    return false;

                var desResult = Deserialize<T>(data, false);
                if (!desResult)
                {
                    log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            return true;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            callResult = null;
            var isError = message["status"] != null && (string)message["status"] == "error";
            if (isError)
            {
                if (request is HuobiSubscribeRequest hRequest)
                {
                    var subResponse = Deserialize<HuobiSubscribeResponse>(message, false);
                    if (!subResponse)
                    {
                        log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Id;
                    if (id != hRequest.Id)
                        return false; // Not for this request

                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(null, new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                {
                    var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message, false);
                    if (!subResponse)
                    {
                        log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                        callResult = new CallResult<object>(null, subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Channel;
                    if (id != haRequest.Channel)
                        return false; // Not for this request

                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.Code);
                    callResult = new CallResult<object>(null, new ServerError(subResponse.Data.Code, "Failed to subscribe"));
                    return true;
                }
            }

            var v1Sub = message["subbed"] != null;
            if (v1Sub)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(message, false);
                if (!subResponse)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiSubscribeRequest)request;
                if (subResponse.Data.Id != hRequest.Id)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(null, new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Subscription completed");
                callResult = new CallResult<object>(subResponse.Data, null);
                return true;
            }

            var v2Sub = (string)message["action"] == "sub";
            if (v2Sub)
            {
                var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message, false);
                if (!subResponse)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(null, subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                if (subResponse.Data.Channel != hRequest.Channel)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.Message);
                    callResult = new CallResult<object>(null, new ServerError(subResponse.Data.Code, subResponse.Data.Message));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Subscription completed");
                callResult = new CallResult<object>(subResponse.Data, null);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            if (request is HuobiSubscribeRequest hRequest)
                return hRequest.Topic == (string) message["ch"];
            
            if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                return haRequest.Channel == (string) message["ch"];
            
            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            if (identifier == "PingV1" && message["ping"] != null)
                return true;

            if (identifier == "PingV2" && (string)message["action"] == "ping")
                return true;

            return false;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
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
            await s.SendAndWait(authObjects, ResponseTimeout, data =>
            {
                if ((string)data["ch"] != "auth")
                    return false;

                var authResponse = Deserialize<HuobiAuthSubscribeResponse>(data, false);
                if (!authResponse)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Data.Message);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                return true;
            });

            return result;
        }

        /// <inheritdoc />
        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            var result = false;
            if (s.Request is HuobiSubscribeRequest hRequest)
            {
                var unsubId = NextId().ToString();
                var unsub = new HuobiUnsubscribeRequest(unsubId, hRequest.Topic);

                await connection.SendAndWait(unsub, ResponseTimeout, data =>
                {
                    if (data.Type != JTokenType.Object)
                        return false;

                    var id = (string)data["id"];
                    if (id == unsubId)
                    {
                        result = (string)data["status"] == "ok";
                        return true;
                    }

                    return false;
                });
                return result;
            }

            if (s.Request is HuobiAuthenticatedSubscribeRequest haRequest)
            {
                var unsub = new Dictionary<string, object>()
                {
                    { "action", "unsub" },
                    { "ch", haRequest.Channel },
                };

                await connection.SendAndWait(unsub, ResponseTimeout, data =>
                {
                    if (data.Type != JTokenType.Object)
                        return false;

                    if ((string)data["action"] == "unsub" && (string)data["ch"] == haRequest.Channel)
                    {
                        result = (int)data["code"] == 200;
                        return true;
                    }

                    return false;
                });
                return result;
            }

            throw new InvalidOperationException("Unknown request type");
        }
    }
}
