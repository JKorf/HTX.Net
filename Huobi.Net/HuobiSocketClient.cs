using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
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
using System.Threading.Tasks;

namespace Huobi.Net
{
    public class HuobiSocketClient : SocketClient
    {
        #region fields
        private static HuobiSocketClientOptions defaultOptions = new HuobiSocketClientOptions();
        private static HuobiSocketClientOptions DefaultOptions
        {
            get
            {
                var result = new HuobiSocketClientOptions()
                {
                    LogVerbosity = defaultOptions.LogVerbosity,
                    BaseAddress = defaultOptions.BaseAddress,
                    LogWriters = defaultOptions.LogWriters,
                    Proxy = defaultOptions.Proxy,
                    BaseAddressAuthenticated = defaultOptions.BaseAddressAuthenticated,
                    ReconnectInterval = defaultOptions.ReconnectInterval,
                    SocketResponseTimeout = defaultOptions.SocketResponseTimeout
                };

                if (defaultOptions.ApiCredentials != null)
                    result.ApiCredentials = new ApiCredentials(defaultOptions.ApiCredentials.Key.GetString(), defaultOptions.ApiCredentials.Secret.GetString());

                return result;
            }
        }

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
        
        protected override bool SocketReconnect(SocketSubscription subscription, TimeSpan disconnectedTime)
        {
            var request = (HuobiRequest)subscription.Request;
            if (request.Signed)
            {
                if (!Authenticate(subscription).Result.Success)
                    return false;
            }

            Send(subscription.Socket, request);

            var subResult = subscription.WaitForEvent("Subscription");
            if (!subResult.Success)
                return false;

            return true;
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryMarketKlinesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketResponse<List<HuobiMarketData>>> QueryMarketKlines(string symbol, HuobiPeriod period) => QueryMarketKlinesAsync(symbol, period).Result;
        /// <summary>
        /// Gets candlestick data for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketResponse<List<HuobiMarketData>>>> QueryMarketKlinesAsync(string symbol, HuobiPeriod period)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            return await Query<HuobiSocketResponse<List<HuobiMarketData>>>(request);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToMarketKlineUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketKlineUpdates(string symbol, HuobiPeriod period, Action<HuobiSocketUpdate<HuobiMarketData>> onData) => SubscribeToMarketKlineUpdatesAsync(symbol, period, onData).Result;
        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketKlineUpdatesAsync(string symbol, HuobiPeriod period, Action<HuobiSocketUpdate<HuobiMarketData>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new PeriodConverter(false))}");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryMarketDepthAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketResponse<HuobiMarketDepth>> QueryMarketDepth(string symbol, int mergeStep) => QueryMarketDepthAsync(symbol, mergeStep).Result;
        /// <summary>
        /// Gets the current orderbook for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketResponse<HuobiMarketDepth>>> QueryMarketDepthAsync(string symbol, int mergeStep)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new CallResult<HuobiSocketResponse<HuobiMarketDepth>>(null, new ArgumentError("Merge step should be between 0 and 5"));

            var request = new HuobiSocketRequest($"market.{symbol}.depth.step{mergeStep}");
            return await Query<HuobiSocketResponse<HuobiMarketDepth>>(request);            
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToDepthUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketDepthUpdates(string symbol, int mergeStep, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData) => SubscribeToDepthUpdatesAsync(symbol, mergeStep, onData).Result;
        /// <summary>
        /// Subscribes to orderbook updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToDepthUpdatesAsync(string symbol, int mergeStep, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData)
        {
            if (mergeStep < 0 || mergeStep > 5)
                return new CallResult<UpdateSubscription>(null, new ArgumentError("Merge step should be between 0 and 5"));

            var request = new HuobiSubscribeRequest($"market.{symbol}.depth.step{mergeStep}");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryMarketTradesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>> QueryMarketTrades(string symbol) => QueryMarketTradesAsync(symbol).Result;
        /// <summary>
        /// Gets a list of trades for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>> QueryMarketTradesAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.trade.detail");
            return await Query<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>(request);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToMarketTradeUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketTradeUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData) => SubscribeToMarketTradeUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketTradeUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.trade.detail");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryMarketDetailsAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketResponse<HuobiMarketData>> QueryMarketDetails(string symbol) => QueryMarketDetailsAsync(symbol).Result;
        /// <summary>
        /// Gets details for a market
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketResponse<HuobiMarketData>>> QueryMarketDetailsAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.detail");
            return await Query<HuobiSocketResponse<HuobiMarketData>>(request);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToMarketDetailUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketDetailUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData) => SubscribeToMarketDetailUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribes to market detail updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketDetailUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.detail");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToMarketTickerUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToMarketTickerUpdates(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData) => SubscribeToMarketTickerUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribes to updates for all market tickers
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketTickerUpdatesAsync(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.tickers");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryAccountsAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>> QueryAccounts() => QueryAccountsAsync().Result;
        /// <summary>
        /// Gets a list of accounts associated with the apikey/secret
        /// </summary>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>> QueryAccountsAsync()
        {
            var request = new HuobiAuthenticatedRequest("req", $"accounts.list");
            return await Query<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>(request);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToAccountUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAccountUpdates(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData) => SubscribeToAccountUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribe to account/wallet updates
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", "accounts");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryAccountsAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketAuthDataResponse<List<HuobiOrder>>> QueryOrders(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => QueryOrdersAsync(accountId, symbol, states, types, startTime, endTime, fromId, limit).Result;
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
        public async Task<CallResult<HuobiSocketAuthDataResponse<List<HuobiOrder>>>> QueryOrdersAsync(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null)
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

            return await Query<HuobiSocketAuthDataResponse<List<HuobiOrder>>>(request);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToOrderUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData) => SubscribeToOrderUpdatesAsync(onData).Result;
        /// <summary>
        /// Subscribe to updates when any order changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", $"orders.*");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="SubscribeToOrderUpdatesAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderUpdates(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData) => SubscribeToOrderUpdatesAsync(symbol, onData).Result;
        /// <summary>
        /// Subscribe to updates when a order for a symbol changes
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", $"orders.{symbol}");
            return await Subscribe(request, onData);
        }

        // <summary>
        /// Synchronized version of the <see cref="QueryOrderDetailsAsync"/> method
        /// </summary>
        /// <returns></returns>
        public CallResult<HuobiSocketAuthDataResponse<HuobiOrder>> QueryOrderDetails(long orderId) => QueryOrderDetailsAsync(orderId).Result;
        /// <summary>
        /// Gets data for a specific order
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <returns></returns>
        public async Task<CallResult<HuobiSocketAuthDataResponse<HuobiOrder>>> QueryOrderDetailsAsync(long orderId)
        {
            return await Query<HuobiSocketAuthDataResponse<HuobiOrder>>(new HuobiOrderDetailsRequest(orderId.ToString()));
        }

        private async Task<CallResult<T>> Query<T>(HuobiRequest request) where T: HuobiResponse
        {
            CallResult<T> result = null;
            var connectResult = await CreateAndConnectSocket<T>(request.Signed, false, data => result = new CallResult<T>(data, null));
            if (!connectResult.Success)
                return new CallResult<T>(null, connectResult.Error);

            var subscription = connectResult.Data;
            Send(subscription.Socket, request);

            var dataResult = subscription.WaitForEvent("Data");
            var closeTask = subscription.Close();

            if (!dataResult.Success)            
                return new CallResult<T>(null, dataResult.Error);

            if (!result.Data.IsSuccessfull)
                return new CallResult<T>(null, new ServerError($"{result.Data.ErrorCode}: {result.Data.ErrorMessage}"));

            return result;
        }
        
        private async Task<CallResult<UpdateSubscription>> Subscribe<U>(HuobiRequest request, Action<U> onData) where U: class
        {
            var connectResult = await CreateAndConnectSocket(request.Signed, true, onData);
            if (!connectResult.Success)
                return new CallResult<UpdateSubscription>(null, connectResult.Error);

            var subscription = connectResult.Data;
            Send(subscription.Socket, request);

            var subResult = subscription.WaitForEvent("Subscription");
            if (!subResult.Success)
            {
                await subscription.Close();
                return new CallResult<UpdateSubscription>(null, subResult.Error);
            }

            subscription.Request = request;
            subscription.Socket.ShouldReconnect = true;
            return new CallResult<UpdateSubscription>(new UpdateSubscription(subscription), null);
        }

        private void DataHandler<T>(SocketSubscription subscription, JToken data, Action<T> handler) where T : class
        {
            var v1Data = (data["data"] != null || data["tick"] != null) && (data["rep"] != null || data["ch"] != null);
            var v2Data = (string)data["op"] == "notify" || (string)data["op"] == "req";

            if (!v1Data && !v2Data)
                return;

            var desResult = Deserialize<T>(data, false);
            if (!desResult.Success)
            {
                log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return;
            }

            handler(desResult.Data);
            subscription.SetEvent("Data", true, null);
        }

        private void PingHandler(SocketSubscription subscription, JToken data)
        {
            bool v1Ping = data["ping"] != null;
            bool v2Ping = (string)data["op"] == "ping";

            if (v1Ping)
                Send(subscription.Socket, new HuobiPingResponse((long)data["ping"]));
            else if (v2Ping)
                Send(subscription.Socket, new HuobiPingAuthResponse((long)data["ts"]));
        }

        private void AuthenticationHandler(SocketSubscription subscription, JToken data)
        {
            if ((string)data["op"] != "auth")
                return;

            var authResponse = Deserialize<HuobiSocketAuthDataResponse<object>>(data, false);
            if (!authResponse.Success)
            {
                log.Write(LogVerbosity.Warning, $"Authorization failed: " + authResponse.Error);
                subscription.SetEvent("Authentication", false, authResponse.Error);
                return;
            }

            log.Write(LogVerbosity.Debug, $"Authorization completed");
            subscription.SetEvent("Authentication", true, null);
        }

        private void SubscriptionHandler(SocketSubscription subscription, JToken data)
        {
            var v1Sub = data["subbed"] != null;
            var v2Sub = (string)data["op"] == "sub";

            if (!v1Sub && !v2Sub)
                return;

            if (v1Sub)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(data, false);
                if (!subResponse.Success)
                {
                    log.Write(LogVerbosity.Warning, $"Subscription failed: " + subResponse.Error);
                    subscription.SetEvent("Subscription", false, subResponse.Error);
                    return;
                }
            }
            else if (v2Sub)
            {
                var subResponse = Deserialize<HuobiSocketAuthResponse>(data, false);
                if (!subResponse.Success)
                {
                    log.Write(LogVerbosity.Warning, $"Subscription failed: " + subResponse.Error);
                    subscription.SetEvent("Subscription", false, subResponse.Error);
                    return;
                }
            }

            log.Write(LogVerbosity.Debug, $"Subscription completed");
            subscription.SetEvent("Subscription", true, null);
        }

        private async Task<CallResult<SocketSubscription>> CreateAndConnectSocket<T>(bool authenticate, bool sub, Action<T> onMessage) where T: class
        {
            var socket = CreateSocket(authenticate ? baseAddressAuthenticated: baseAddress);
            var subscription = new SocketSubscription(socket);
            subscription.DataHandlers.Add(PingHandler);
            subscription.DataHandlers.Add(AuthenticationHandler);
            subscription.DataHandlers.Add(SubscriptionHandler);
            subscription.DataHandlers.Add((subs, data) => DataHandler(subs, data, onMessage));

            if (authenticate)
                subscription.AddEvent("Authentication");

            if (sub)
                subscription.AddEvent("Subscription");
            else
                subscription.AddEvent("Data");
            
            var connectResult = await ConnectSocket(subscription);
            if (!connectResult.Success)
                return new CallResult<SocketSubscription>(null, connectResult.Error);

            if(authenticate)
            {
                var authResult = await Authenticate(subscription);
                if (!authResult.Success)
                    return new CallResult<SocketSubscription>(null, authResult.Error);
            }

            return new CallResult<SocketSubscription>(subscription, null);
        }

        private async Task<CallResult<bool>> Authenticate(SocketSubscription subscription)
        {            
            var authParams = authProvider.AddAuthenticationToParameters(baseAddressAuthenticated, Constants.GetMethod, new Dictionary<string, object>(), true);
            var authObjects = new HuobiAuthenticationRequest()
            {
                AccessKeyId = authProvider.Credentials.Key.GetString(),
                Operation = "auth",
                SignatureMethod = (string)authParams["SignatureMethod"],
                SignatureVersion = authParams["SignatureVersion"].ToString(),
                Timestamp = (string)authParams["Timestamp"],
                Signature = (string)authParams["Signature"],
            };
            Send(subscription.Socket, authObjects);

            var authResult = subscription.WaitForEvent("Authentication");
            if (!authResult.Success)
            {
                await subscription.Close();
                return new CallResult<bool>(false, authResult.Error);
            }

            return new CallResult<bool>(true, null);
        }
        
        private string DecompressData(byte[] byteData)
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
    }
}
