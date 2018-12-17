using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Converters;
using Huobi.Net.Interfaces;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Huobi.Net
{
    public class HuobiSocketClient : SocketClient, IHuobiSocketClient
    {
        #region fields
        private static HuobiSocketClientOptions defaultOptions = new HuobiSocketClientOptions();
        private static HuobiSocketClientOptions DefaultOptions => defaultOptions.Copy();

        private string baseAddressAuthenticated;
        private int socketResponseTimeout;
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
        public HuobiSocketClient(HuobiSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new HuobiAuthenticationProvider(options.ApiCredentials))
        {
            Configure(options);

            SetDataInterpreter(DecompressData);
        }
        #endregion

        #region methods
        private void Configure(HuobiSocketClientOptions options)
        {
            baseAddressAuthenticated = options.BaseAddressAuthenticated;
            socketResponseTimeout = (int)Math.Round(options.SocketResponseTimeout.TotalMilliseconds);
        }

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
        public CallResult<List<HuobiMarketKline>> QueryMarketKlines(string symbol, HuobiPeriod period) => QueryMarketKlinesAsync(symbol, period).Result;
        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        public async Task<CallResult<List<HuobiMarketKline>>> QueryMarketKlinesAsync(string symbol, HuobiPeriod period)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var result = await Query<HuobiSocketResponse<List<HuobiMarketKline>>>(request).ConfigureAwait(false);
            return new CallResult<List<HuobiMarketKline>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketKlineUpdates(string symbol, HuobiPeriod period, Action<HuobiMarketKline> onData) => SubscribeToMarketKlineUpdatesAsync(symbol, period, onData).Result;
        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketKlineUpdatesAsync(string symbol, HuobiPeriod period, Action<HuobiMarketKline> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiMarketKline>>(data => onData(data.Data));
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public CallResult<HuobiMarketDepth> QueryMarketDepth(string symbol, int mergeStep) => QueryMarketDepthAsync(symbol, mergeStep).Result;
        /// <summary>
        /// Gets the current order book for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiMarketDepth>> QueryMarketDepthAsync(string symbol, int mergeStep)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new CallResult<HuobiMarketDepth>(null, new ArgumentError("Merge step should be between 0 and 5"));

            var request = new HuobiSocketRequest($"market.{symbol}.depth.step{mergeStep}");
            var result = await Query<HuobiSocketResponse<HuobiMarketDepth>>(request).ConfigureAwait(false);     
            if(!result.Success)
                return new CallResult<HuobiMarketDepth>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiMarketDepth>(result.Data.Data, null);
        }

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketDepthUpdates(string symbol, int mergeStep, Action<HuobiMarketDepth> onData) => SubscribeToMarketDepthUpdatesAsync(symbol, mergeStep, onData).Result;
        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketDepthUpdatesAsync(string symbol, int mergeStep, Action<HuobiMarketDepth> onData)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new CallResult<UpdateSubscription>(null, new ArgumentError("Merge step should be between 0 and 5"));

            var internalHandler = new Action<HuobiSocketUpdate<HuobiMarketDepth>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.Data);
            });

            var request = new HuobiSubscribeRequest($"market.{symbol}.depth.step{mergeStep}");
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        public CallResult<List<HuobiMarketTradeDetails>> QueryMarketTrades(string symbol) => QueryMarketTradesAsync(symbol).Result;
        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        public async Task<CallResult<List<HuobiMarketTradeDetails>>> QueryMarketTradesAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.trade.detail");
            var result = await Query<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>(request).ConfigureAwait(false);
            return new CallResult<List<HuobiMarketTradeDetails>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketTradeUpdates(string symbol, Action<HuobiMarketTrade> onData) => SubscribeToMarketTradeUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketTradeUpdatesAsync(string symbol, Action<HuobiMarketTrade> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.trade.detail");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiMarketTrade>>(data => onData(data.Data));
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets details for a market
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        public CallResult<HuobiMarketDetails> QueryMarketDetails(string symbol) => QueryMarketDetailsAsync(symbol).Result;
        /// <summary>
        /// Gets details for a market
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiMarketDetails>> QueryMarketDetailsAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.detail");
            var result = await Query<HuobiSocketResponse<HuobiMarketDetails>>(request).ConfigureAwait(false);
            if (!result.Success)
                return new CallResult<HuobiMarketDetails>(null, result.Error);

            result.Data.Data.Timestamp = result.Data.Timestamp;
            return new CallResult<HuobiMarketDetails>(result.Data.Data, null);
        }

        /// <summary>
        /// Subscribes to market detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketDetailUpdates(string symbol, Action<HuobiMarketDetails> onData) => SubscribeToMarketDetailUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to market detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketDetailUpdatesAsync(string symbol, Action<HuobiMarketDetails> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.detail");
            var internalHandler = new Action<HuobiSocketUpdate<HuobiMarketDetails>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.Data);
            });
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribes to updates for all market tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketTickerUpdates(Action<HuobiMarketTicks> onData) => SubscribeToMarketTickerUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribes to updates for all market tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketTickerUpdatesAsync(Action<HuobiMarketTicks> onData)
        {
            var request = new HuobiSubscribeRequest("market.tickers");
            var internalHandler = new Action<HuobiSocketUpdate<List<HuobiMarketTick>>>(data =>
            {
                var result = new HuobiMarketTicks();
                result.Timestamp = data.Timestamp;
                result.Ticks = data.Data;
                onData(result);
            });
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        public CallResult<List<HuobiAccountBalances>> QueryAccounts() => QueryAccountsAsync().Result;
        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        public async Task<CallResult<List<HuobiAccountBalances>>> QueryAccountsAsync()
        {
            var request = new HuobiAuthenticatedRequest("req", "accounts.list");
            var result = await Query<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>(request).ConfigureAwait(false);
            return new CallResult<List<HuobiAccountBalances>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribe to account/wallet updates
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAccountUpdates(Action<HuobiAccountEvent> onData) => SubscribeToAccountUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribe to account/wallet updates
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<HuobiAccountEvent> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", "accounts");
            var internalHandler = new Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>>(data =>
            {
                data.Data.Timestamp = data.Timestamp;
                onData(data.Data);
            });
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="accountId">The account id to get orders for</param>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public CallResult<List<HuobiOrder>> QueryOrders(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => QueryOrdersAsync(accountId, symbol, states, types, startTime, endTime, fromId, limit).Result;
        /// <summary>
        /// Gets a list of orders
        /// </summary>
        /// <param name="accountId">The account id to get orders for</param>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with id's higher than this</param>
        /// <param name="limit">The max number of results</param>
        /// <returns></returns>
        public async Task<CallResult<List<HuobiOrder>>> QueryOrdersAsync(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
        {
            var stateConverter = new OrderStateConverter(false);
            var stateString = string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter)));
            
            var request = new HuobiOrderListRequest(accountId, symbol, stateString);
            if(types != null)
            {
                var typeConverter = new OrderTypeConverter(false);
                request.Types = string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter)));
            }
            request.StartTime = startTime?.ToString("yyyy-MM-dd");
            request.EndTime = endTime?.ToString("yyyy-MM-dd");
            request.FromId = fromId?.ToString();
            request.Limit = limit?.ToString();

            var result = await Query<HuobiSocketAuthDataResponse<List<HuobiOrder>>>(request).ConfigureAwait(false);
            return new CallResult<List<HuobiOrder>>(result.Data?.Data, result.Error);
        }

        /// <summary>
        /// Subscribe to updates when any order changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(Action<HuobiOrderUpdate> onData) => SubscribeToOrderUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribe to updates when any order changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<HuobiOrderUpdate> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", "orders.*");
            var internalHandler = new Action<HuobiSocketAuthDataResponse<HuobiOrderUpdate>>(data => onData(data.Data));
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to updates when a order for a symbol changes
        /// </summary> 
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(string symbol, Action<HuobiOrderUpdate> onData) => SubscribeToOrderUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribe to updates when a order for a symbol changes
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<HuobiOrderUpdate> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", $"orders.{symbol}");
            var internalHandler = new Action<HuobiSocketAuthDataResponse<HuobiOrderUpdate>>(data => onData(data.Data));
            return await Subscribe(request, internalHandler).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets data for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        public CallResult<HuobiOrder> QueryOrderDetails(long orderId) => QueryOrderDetailsAsync(orderId).Result;
        /// <summary>
        /// Gets data for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiOrder>> QueryOrderDetailsAsync(long orderId)
        {
            var result = await Query<HuobiSocketAuthDataResponse<HuobiOrder>>(new HuobiOrderDetailsRequest(orderId.ToString())).ConfigureAwait(false);
            return new CallResult<HuobiOrder>(result.Data?.Data, result.Error);
        }

        #region private
        private async Task<CallResult<T>> Query<T>(HuobiRequest request) where T: HuobiResponse
        {
            CallResult<T> result = null;
            var subscription = GetBackgroundSocket(request.Signed);
            if (subscription == null)
            {
                // We don't have a background socket to query, create a new one
                var connectResult = await CreateAndConnectSocket<T>(request.Signed, false, data => result = new CallResult<T>(data, null)).ConfigureAwait(false);
                if (!connectResult.Success)
                    return new CallResult<T>(null, connectResult.Error);

                subscription = connectResult.Data;
                subscription.Type = request.Signed ? SocketType.BackgroundAuthenticated : SocketType.Background;
            }
            else
            {
                // Use earlier created background socket to query without having to connect again
                subscription.Events.Single(s => s.Name == DataEvent).Reset();
                if(request.Signed)
                    subscription.MessageHandlers[DataHandlerName] = (subs, data) => DataHandlerV2<T>(subs, data, rdata => result = new CallResult<T>(rdata, null));
                else
                    subscription.MessageHandlers[DataHandlerName] = (subs, data) => DataHandlerV1<T>(subs, data, rdata => result = new CallResult<T>(rdata, null));
            }

            Send(subscription.Socket, request);
            var dataResult = await subscription.WaitForEvent(DataEvent, socketResponseTimeout).ConfigureAwait(false);

            if (!dataResult.Success)            
                return new CallResult<T>(null, dataResult.Error);

            return !result.Data.IsSuccessful ? new CallResult<T>(null, new ServerError($"{result.Data.ErrorCode}: {result.Data.ErrorMessage}")) : result;
        }
        
        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(HuobiRequest request, Action<T> onData) where T: class
        {
            var connectResult = await CreateAndConnectSocket(request.Signed, true, onData).ConfigureAwait(false);
            if (!connectResult.Success)
                return new CallResult<UpdateSubscription>(null, connectResult.Error);

            var subscription = connectResult.Data;
            Send(subscription.Socket, request);

            var subResult = await subscription.WaitForEvent(SubscriptionEvent, socketResponseTimeout).ConfigureAwait(false);
            if (!subResult.Success)
            {
                await subscription.Close().ConfigureAwait(false);
                return new CallResult<UpdateSubscription>(null, subResult.Error);
            }

            subscription.Request = request;
            subscription.Socket.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(subscription), null);
        }

        private bool DataHandlerV1<T>(SocketSubscription subscription, JToken data, Action<T> handler) where T : class
        {
            var v1Data = (data["data"] != null || data["tick"] != null) && (data["rep"] != null || data["ch"] != null);
            var v1Error = data["status"] != null && (string)data["status"] == "error";

            if (!v1Data && !v1Error)
                return false;

            if (!v1Data && subscription.GetWaitingEvent(DataEvent) == null)
                return false;

            var desResult = Deserialize<T>(data, false);
            if (!desResult.Success)
            {
                log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            handler(desResult.Data);
            subscription.SetEventByName(DataEvent, true, null);
            return true;
        }

        private bool DataHandlerV2<T>(SocketSubscription subscription, JToken data, Action<T> handler) where T : class
        {
            var v2Data = (string)data["op"] == "notify" || (string)data["op"] == "req";

            if (!v2Data)
                return false;

            var desResult = Deserialize<T>(data, false);
            if (!desResult.Success)
            {
                log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            handler(desResult.Data);
            subscription.SetEventByName(DataEvent, true, null);
            return true;
        }

        private bool PingHandlerV1(SocketSubscription subscription, JToken data)
        {
            var v1Ping = data["ping"] != null;

            if (v1Ping)
                Send(subscription.Socket, new HuobiPingResponse((long)data["ping"]));

            return v1Ping;
        }

        private bool PingHandlerV2(SocketSubscription subscription, JToken data)
        {
            var v2Ping = (string)data["op"] == "ping";

            if (v2Ping)
                Send(subscription.Socket, new HuobiPingAuthResponse((long)data["ts"]));

            return v2Ping;
        }

        private bool AuthenticationHandler(SocketSubscription subscription, JToken data)
        {
            if ((string)data["op"] != "auth")
                return false;

            var authResponse = Deserialize<HuobiSocketAuthDataResponse<object>>(data, false);
            if (!authResponse.Success)
            {
                log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error);
                subscription.SetEventByName(AuthenticationEvent, false, authResponse.Error);
                return true;
            }
            if (!authResponse.Data.IsSuccessful)
            {
                log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Data.ErrorMessage);
                subscription.SetEventByName(AuthenticationEvent, false, new ServerError(authResponse.Data.ErrorCode, authResponse.Data.ErrorMessage));
                return true;
            }

            log.Write(LogVerbosity.Debug, "Authorization completed");
            subscription.SetEventByName(AuthenticationEvent, true, null);
            return true;
        }

        private bool SubscriptionHandlerV1(SocketSubscription subscription, JToken data)
        {
            var v1Sub = data["subbed"] != null;
            var v1Error = data["status"] != null && (string)data["status"] == "error";
            if (v1Sub || v1Error)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(data, false);
                if (!subResponse.Success)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                    subscription.SetEventByName(SubscriptionEvent, false, subResponse.Error);
                    return true;
                }

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.ErrorMessage);
                    subscription.SetEventByName(SubscriptionEvent, false, new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                log.Write(LogVerbosity.Debug, "Subscription completed");
                subscription.SetEventByName(SubscriptionEvent, true, null);
                return true;
            }

            return false;
        }

        private bool SubscriptionHandlerV2(SocketSubscription subscription, JToken data)
        {
            var v2Sub = (string)data["op"] == "sub";
            if (!v2Sub)
                return false;
            
            var subResponse = Deserialize<HuobiSocketAuthResponse>(data, false);
            if (!subResponse.Success)
            {
                log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                subscription.SetEventByName(SubscriptionEvent, false, subResponse.Error);
                return true;
            }

            if (!subResponse.Data.IsSuccessful)
            {
                log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Data.ErrorMessage);
                subscription.SetEventByName(SubscriptionEvent, false, new ServerError(subResponse.Data.ErrorCode, subResponse.Data.ErrorMessage));
                return true;
            }

            log.Write(LogVerbosity.Debug, "Subscription completed");
            subscription.SetEventByName(SubscriptionEvent, true, null);
            return true;            
        }

        private async Task<CallResult<SocketSubscription>> CreateAndConnectSocket<T>(bool authenticate, bool sub, Action<T> onMessage) where T: class
        {
            var socket = CreateSocket(authenticate ? baseAddressAuthenticated: BaseAddress);
            var subscription = new SocketSubscription(socket);

            if (authenticate)
            {
                subscription.MessageHandlers.Add(DataHandlerName, (subs, data) => DataHandlerV2(subs, data, onMessage));
                subscription.MessageHandlers.Add(PingHandlerName, PingHandlerV2);
                subscription.MessageHandlers.Add(SubscriptionHandlerName, SubscriptionHandlerV2);
                subscription.MessageHandlers.Add(AuthenticationHandlerName, AuthenticationHandler);

                subscription.AddEvent(AuthenticationEvent);
            }
            else
            {
                subscription.MessageHandlers.Add(DataHandlerName, (subs, data) => DataHandlerV1(subs, data, onMessage));
                subscription.MessageHandlers.Add(PingHandlerName, PingHandlerV1);
                subscription.MessageHandlers.Add(SubscriptionHandlerName, SubscriptionHandlerV1);
            }

            subscription.AddEvent(sub ? SubscriptionEvent: DataEvent);
            
            var connectResult = await ConnectSocket(subscription).ConfigureAwait(false);
            if (!connectResult.Success)
                return new CallResult<SocketSubscription>(null, connectResult.Error);

            if(authenticate)
            {
                var authResult = await Authenticate(subscription).ConfigureAwait(false);
                if (!authResult.Success)
                    return new CallResult<SocketSubscription>(null, authResult.Error);
            }

            return new CallResult<SocketSubscription>(subscription, null);
        }

        private async Task<CallResult<bool>> Authenticate(SocketSubscription subscription)
        {            
            if(authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var authParams = authProvider.AddAuthenticationToParameters(baseAddressAuthenticated, Constants.GetMethod, new Dictionary<string, object>(), true);
            var authObjects = new HuobiAuthenticationRequest
            {
                AccessKeyId = authProvider.Credentials.Key.GetString(),
                Operation = "auth",
                SignatureMethod = (string)authParams["SignatureMethod"],
                SignatureVersion = authParams["SignatureVersion"].ToString(),
                Timestamp = (string)authParams["Timestamp"],
                Signature = (string)authParams["Signature"]
            };
            Send(subscription.Socket, authObjects);

            var authResult = await subscription.WaitForEvent(AuthenticationEvent, socketResponseTimeout).ConfigureAwait(false);
            if (!authResult.Success)
            {
                await subscription.Close().ConfigureAwait(false);
                return new CallResult<bool>(false, authResult.Error);
            }

            return new CallResult<bool>(true, null);
        }

        protected override bool SocketReconnect(SocketSubscription subscription, TimeSpan disconnectedTime)
        {
            var request = (HuobiRequest)subscription.Request;
            if (request.Signed)
            {
                if (!Authenticate(subscription).Result.Success)
                    return false;
            }

            Send(subscription.Socket, request);

            return subscription.WaitForEvent(SubscriptionEvent, socketResponseTimeout).Result.Success;
        }

        private static string DecompressData(byte[] byteData)
        {
            using (var decompressedStream = new MemoryStream())
            using (var compressedStream = new MemoryStream(byteData))
            using (var deflateStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;

                using (var streamReader = new StreamReader(decompressedStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        #endregion
        #endregion
    }
}
