using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
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
using System.Text;
using System.Threading;
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

        protected override void SocketOpened(IWebsocket socket)
        {
        }

        protected override void SocketClosed(IWebsocket socket)
        {
        }

        protected override void SocketError(IWebsocket socket, Exception ex)
        {
        }

        protected override bool SocketReconnect(SocketSubscription socket, TimeSpan disconnectedTime)
        {
            var request = (HuobiRequest)socket.Request;
            var id = NextId().ToString();
            if (request is HuobiAuthenticatedRequest ar)
                ar.Id = id;
            else if (request is HuobiSubscribeRequest sr)
                sr.Id = id;

            if (request.Signed)
            {
                if (!Authenticate(socket.Socket).Result.Success)
                {
                    log.Write(LogVerbosity.Warning, $"Socket {socket.Socket.Id} authentication failed while trying to reconnect");
                    return false;
                }
            }

            var resubResult = SendAndWait(socket.Socket, request, data => 
            {
                return data["id"] != null && (string)data["id"] == id || data["cid"] != null && (string)data["cid"] == id;
            }, socketResponseTimeout).Result;

            if (!resubResult.Success)
            {
                log.Write(LogVerbosity.Warning, $"Socket {socket.Socket.Id} no subscription response while trying to reconnect");
                return false;
            }
            var subData = Deserialize<HuobiSubscribeResponse>(resubResult.Data, false);
            if (!subData.Success)
            {
                log.Write(LogVerbosity.Warning, $"Socket {socket.Socket.Id} failed sub response deserialization while trying to reconnect");
                return false;
            }

            return true;
        }
        
        public CallResult<HuobiSocketResponse<List<HuobiMarketData>>> QueryMarketKlines(string symbol, string period) => QueryMarketKlinesAsync(symbol, period).Result;
        public async Task<CallResult<HuobiSocketResponse<List<HuobiMarketData>>>> QueryMarketKlinesAsync(string symbol, string period)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.kline.{period}");
            return await Query<HuobiSocketResponse<List<HuobiMarketData>>>(request);
        }

        public CallResult<SocketSubscription> SubscribeToMarketKlineUpdates(string symbol, string period, Action<HuobiSocketUpdate<HuobiMarketData>> onData) => SubscribeToMarketKlineUpdatesAsync(symbol, period, onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToMarketKlineUpdatesAsync(string symbol, string period, Action<HuobiSocketUpdate<HuobiMarketData>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.kline.{period}");
            return await Subscribe<HuobiSubscribeResponse, HuobiSocketUpdate<HuobiMarketData>>(request, onData);
        }

        public CallResult<HuobiSocketResponse<HuobiMarketDepth>> QueryMarketDepth(string symbol, string type) => QueryMarketDepthAsync(symbol, type).Result;
        public async Task<CallResult<HuobiSocketResponse<HuobiMarketDepth>>> QueryMarketDepthAsync(string symbol, string type)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.depth.{type}");
            return await Query<HuobiSocketResponse<HuobiMarketDepth>>(request);            
        }

        public CallResult<SocketSubscription> SubscribeToMarketDepthUpdates(string symbol, string type, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData) => SubscribeToDepthUpdatesAsync(symbol, type, onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToDepthUpdatesAsync(string symbol, string type, Action<HuobiSocketUpdate<HuobiMarketDepth>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.depth.{type}");
            return await Subscribe<HuobiSubscribeResponse, HuobiSocketUpdate<HuobiMarketDepth>>(request, onData);
        }

        public CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>> QueryMarketTrades(string symbol) => QueryMarketTradesAsync(symbol).Result;
        public async Task<CallResult<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>> QueryMarketTradesAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.trade.detail");
            return await Query<HuobiSocketResponse<List<HuobiMarketTradeDetails>>>(request);
        }

        public CallResult<SocketSubscription> SubscribeToMarketTradeUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData) => SubscribeToMarketTradeUpdatesAsync(symbol, onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToMarketTradeUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketTrade>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.trade.detail");
            return await Subscribe<HuobiSubscribeResponse, HuobiSocketUpdate<HuobiMarketTrade>>(request, onData);
        }


        public CallResult<HuobiSocketResponse<HuobiMarketData>> QueryMarketDetails(string symbol) => QueryMarketDetailsAsync(symbol).Result;
        public async Task<CallResult<HuobiSocketResponse<HuobiMarketData>>> QueryMarketDetailsAsync(string symbol)
        {
            var request = new HuobiSocketRequest($"market.{symbol}.detail");
            return await Query<HuobiSocketResponse<HuobiMarketData>>(request);
        }

        public CallResult<SocketSubscription> SubscribeToMarketDetailUpdates(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData) => SubscribeToMarketDetailUpdatesAsync(symbol, onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToMarketDetailUpdatesAsync(string symbol, Action<HuobiSocketUpdate<HuobiMarketData>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.{symbol}.detail");
            return await Subscribe<HuobiSubscribeResponse, HuobiSocketUpdate<HuobiMarketData>>(request, onData);
        }

        public CallResult<SocketSubscription> SubscribeToMarketTickerUpdates(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData) => SubscribeToMarketTickerUpdatesAsync(onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToMarketTickerUpdatesAsync(Action<HuobiSocketUpdate<List<HuobiMarketTick>>> onData)
        {
            var request = new HuobiSubscribeRequest($"market.tickers");
            return await Subscribe<HuobiSubscribeResponse, HuobiSocketUpdate<List<HuobiMarketTick>>>(request, onData);
        }

        public CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>> QueryAccounts() => QueryAccountsAsync().Result;
        public async Task<CallResult<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>> QueryAccountsAsync()
        {
            var request = new HuobiAuthenticatedRequest("req", $"accounts.list");
            return await Query<HuobiSocketAuthDataResponse<List<HuobiAccountBalances>>>(request);
        }

        public CallResult<SocketSubscription> SubscribeToAccountUpdates(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData) => SubscribeToAccountUpdatesAsync(onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToAccountUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiAccountEvent>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", "accounts");
            return await Subscribe<HuobiSocketAuthResponse, HuobiSocketAuthDataResponse<HuobiAccountEvent>>(request, onData);
        }

        public CallResult<HuobiSocketAuthDataResponse<List<HuobiOrder>>> QueryOrders(long accountId, string symbol, HuobiOrderState[] states, HuobiOrderType[] types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, int? limit = null) => QueryOrdersAsync(accountId, symbol, states, types, startTime, endTime, fromId, limit).Result;
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

        public CallResult<SocketSubscription> SubscribeToOrderUpdates(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData) => SubscribeToOrderUpdatesAsync(onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToOrderUpdatesAsync(Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", $"orders.*");
            return await Subscribe<HuobiSocketAuthResponse, HuobiSocketAuthDataResponse<HuobiOrder>>(request, onData);
        }

        public CallResult<SocketSubscription> SubscribeToOrderUpdates(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData) => SubscribeToOrderUpdatesAsync(symbol, onData).Result;
        public async Task<CallResult<SocketSubscription>> SubscribeToOrderUpdatesAsync(string symbol, Action<HuobiSocketAuthDataResponse<HuobiOrder>> onData)
        {
            var request = new HuobiAuthenticatedRequest("sub", $"orders.{symbol}");
            return await Subscribe<HuobiSocketAuthResponse, HuobiSocketAuthDataResponse<HuobiOrder>>(request, onData);
        }

        public CallResult<HuobiSocketAuthDataResponse<HuobiOrder>> QueryOrderDetails(long orderId) => QueryOrderDetailsAsync(orderId).Result;
        public async Task<CallResult<HuobiSocketAuthDataResponse<HuobiOrder>>> QueryOrderDetailsAsync(long orderId)
        {
            return await Query<HuobiSocketAuthDataResponse<HuobiOrder>>(new HuobiOrderDetailsRequest(orderId.ToString()));
        }


        public async Task<CallResult<bool>> Authenticate(IWebsocket socket)
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
            var result = await SendAndWait(socket, authObjects, data => { return data["op"] != null && (string)data["op"] == "auth"; }, socketResponseTimeout);
            if (!result.Success)
                return new CallResult<bool>(false, result.Error);

            var desResult = Deserialize<HuobiSocketAuthDataResponse<object>>(result.Data, false);
            if (!desResult.Success)
                return new CallResult<bool>(false, desResult.Error);

            if (desResult.Data.ErrorCode != 0)
                return new CallResult<bool>(false, new ServerError(desResult.Data.ErrorCode, desResult.Data.ErrorMessage));

            return new CallResult<bool>(true, null);
        }
        
        private async Task<CallResult<T>> Query<T>(HuobiRequest request) where T: HuobiResponse
        {
            var id = NextId().ToString();
            var result = new CallResult<T>(null, new ServerError("No response from server"));
            var subWait = new ManualResetEvent(false);
            Action<string> handler = new Action<string>(data =>
            {
                var token = JToken.Parse(data);
                var operation = token["op"];
                if (operation != null && ((string)operation == "auth" || (string)operation == "ping"))
                    return;

                if (token["ping"] != null)
                    return;

                log.Write(LogVerbosity.Debug, $"Socket received data: " + data);
                if (!result.Success)
                {
                    var desResult = Deserialize<T>(data, false);
                    if (!desResult.Success)
                    {
                        result = new CallResult<T>(null, desResult.Error);
                        return;
                    }

                    if (desResult.Data.IsSuccessfull)
                        result = new CallResult<T>(desResult.Data, null);
                    else
                        result = new CallResult<T>(null, new ServerError($"{desResult.Data.ErrorCode}: {desResult.Data.ErrorMessage}"));

                    subWait.Set();
                }
            });

            var connectResult = await CreateAndConnectSocket(request.Signed, handler);
            if (!connectResult.Success)
                return new CallResult<T>(null, connectResult.Error);

            request.Id = id;
            Send(connectResult.Data.Socket, request);
            subWait.WaitOne(socketResponseTimeout);
           
            connectResult.Data.Socket.ShouldReconnect = false;
            await connectResult.Data.Socket.Close();
            connectResult.Data.Socket.Dispose();
            
            return result;
        }
        

        private async Task<CallResult<SocketSubscription>> Subscribe<T, U>(HuobiRequest request, Action<U> onData) where T: HuobiResponse where U: class
        {
            var id = NextId().ToString();
            CallResult<bool> subConfirmation = new CallResult<bool>(false, new ServerError("No response from server"));
            var subWait = new ManualResetEvent(false);
            Action<string> handler = new Action<string>(data =>
            {
                var token = JToken.Parse(data);
                var operation = token["op"];
                if (operation != null && ((string)operation == "auth" || (string)operation == "ping"))
                    return;

                if (token["ping"] != null)
                    return;

                log.Write(LogVerbosity.Debug, $"Socket received data: " + data);
                if (!subConfirmation.Success)
                {
                    var desResult = Deserialize<T>(data, false);
                    if (!desResult.Success)
                    {
                        subConfirmation = new CallResult<bool>(false, desResult.Error);
                        return;
                    }

                    if (desResult.Data.IsSuccessfull)                    
                        subConfirmation = new CallResult<bool>(true, null);                    
                    else                    
                        subConfirmation = new CallResult<bool>(false, new ServerError($"{desResult.Data.ErrorCode}: {desResult.Data.ErrorMessage}"));
                    
                    subWait.Set();
                }
                else
                {
                    if ((operation != null && (string)operation == "sub") || token["subbed"] != null)
                        return;

                    var desResult = Deserialize<U>(data, false);
                    if (!desResult.Success)
                        return;

                    onData(desResult.Data);
                }
            });

            var connectResult = await CreateAndConnectSocket(request.Signed, handler);
            if (!connectResult.Success)
                return new CallResult<SocketSubscription>(null, connectResult.Error);

            request.Id = id;
            Send(connectResult.Data.Socket, request);
            subWait.WaitOne(socketResponseTimeout);

            if (!subConfirmation.Success)
            {
                connectResult.Data.Socket.ShouldReconnect = false;
                await connectResult.Data.Socket.Close();
                connectResult.Data.Socket.Dispose();
                return new CallResult<SocketSubscription>(null, subConfirmation.Error);
            }

            connectResult.Data.Request = request;
            return new CallResult<SocketSubscription>(connectResult.Data, null);
        }

        private async Task<CallResult<SocketSubscription>> CreateAndConnectSocket(bool authenticate, Action<string> onMessage)
        {
            var socket = CreateSocket(authenticate ? baseAddressAuthenticated: baseAddress);
            socket.OnMessage += onMessage;

            var connectResult = ConnectSocket(socket);
            if (!connectResult.Success)
                return new CallResult<SocketSubscription>(null, connectResult.Error);

            if (authenticate)
            {
                var authResult = await Authenticate(socket);
                if (!authResult.Success)
                {
                    connectResult.Data.Socket.ShouldReconnect = false;
                    await connectResult.Data.Socket.Close();
                    connectResult.Data.Socket.Dispose();
                    return new CallResult<SocketSubscription>(null, authResult.Error);
                }
            }

            return new CallResult<SocketSubscription>(connectResult.Data, null);
        }

        protected override IWebsocket CreateSocket(string address)
        {
            var result = base.CreateSocket(address);

            result.OnMessage += data =>
            {
                var token = JToken.Parse(data);
                if (token["ping"] != null)
                {
                    log.Write(LogVerbosity.Debug, $"Socket {result.Id} received ping request: " + data);
                    Send(result, new HuobiPingResponse((long)token["ping"]));
                }
                if(token["op"] != null && (string)token["op"] == "ping")
                {
                    log.Write(LogVerbosity.Debug, $"Socket {result.Id} received ping request: " + data);
                    Send(result, new HuobiPingAuthResponse((long)token["ts"]));
                }
            };

            return result;
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
