using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiSocketClient" />
    public class HuobiSocketClient : BaseSocketClient, IHuobiSocketClient
    {
        #region fields
        /// <inheritdoc />
        public IHuobiSocketClientSpotStreams SpotStreams { get; }
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of HuobiSocketClient with default options
        /// </summary>
        public HuobiSocketClient() : this(HuobiSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of HuobiSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public HuobiSocketClient(HuobiSocketClientOptions options) : base("Huobi", options)
        {
            SpotStreams = AddApiClient(new HuobiSocketClientSpotStreams(log, this, options));

            SetDataInterpreter(DecompressData, null);
            AddGenericHandler("PingV1", PingHandlerV1);
            AddGenericHandler("PingV2", PingHandlerV2);
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(HuobiSocketClientOptions options)
        {
            HuobiSocketClientOptions.Default = options;
        }
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

        /// <inheritdoc />
        protected override SocketConnection GetSocketConnection(SocketApiClient apiClient, string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => s.Value.Socket.Url.TrimEnd('/') == address.TrimEnd('/')
                                && s.Value.ApiClient.GetType() == apiClient.GetType()
                                && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < ClientOptions.SocketSubscriptionsCombineTarget || sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= ClientOptions.SocketSubscriptionsCombineTarget))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketWrapper = new SocketConnection(this, apiClient, socket);
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

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
        {
            return SubscribeAsync(apiClient, request, identifier, authenticated, dataHandler, ct);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
        {
            return SubscribeAsync(apiClient, url, request, identifier, authenticated, dataHandler, ct);
        }

        internal Task<CallResult<T>> QueryInternalAsync<T>(SocketApiClient apiClient, string url, object request, bool authenticated)
            => QueryAsync<T>(apiClient, url, request, authenticated);

        internal int NextIdInternal() => NextId();

        internal Task<CallResult<T>> QueryInternalAsync<T>(SocketApiClient apiClient, object request, bool authenticated)
            => QueryAsync<T>(apiClient, request, authenticated);

        internal CallResult<T> DeserializeInternal<T>(JToken obj, JsonSerializer? serializer = null, int? requestId = null)
            => Deserialize<T>(obj, serializer, requestId);
        #endregion
        #endregion

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
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
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
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
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
                        log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                        return false;
                    }

                    var id = subResponse.Data.Id;
                    if (id != hRequest.Id)
                        return false; // Not for this request

                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                if (request is HuobiAuthenticatedSubscribeRequest haRequest)
                {
                    var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                    if (!subResponse)
                    {
                        log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                        callResult = new CallResult<object>(subResponse.Error!);
                        return false;
                    }

                    var id = subResponse.Data.Channel;
                    if (id != haRequest.Channel)
                        return false; // Not for this request

                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.Code);
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
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                    return false;
                }

                var hRequest = (HuobiSubscribeRequest)request;
                if (subResponse.Data.Id != hRequest.Id)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                    callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Subscription completed");
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
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(subResponse.Error!);
                    return false;
                }

                var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
                if (subResponse.Data.Channel != hRequest.Channel)
                    return false;

                if (!subResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Subscription failed: " + subResponse.Data.Message);
                    callResult = new CallResult<object>(new ServerError(subResponse.Data.Code, subResponse.Data.Message));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Subscription completed");
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

            return false;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            if (s.ApiClient.AuthenticationProvider == null)
                return new CallResult<bool>(new NoApiCredentialsError());

            var result = new CallResult<bool>(new ServerError("No response from server"));
            await s.SendAndWaitAsync(((HuobiAuthenticationProvider)s.ApiClient.AuthenticationProvider).GetWebsocketAuthentication(new Uri(s.Socket.Url)), ClientOptions.SocketResponseTimeout, data =>
            {
                if (data["ch"]?.ToString() != "auth")
                    return false;

                var authResponse = Deserialize<HuobiAuthSubscribeResponse>(data);
                if (!authResponse)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(authResponse.Error!);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    log.Write(LogLevel.Warning, $"Socket {s.Socket.Id} Authorization failed: " + authResponse.Data.Message);
                    result = new CallResult<bool>(new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                    return true;
                }

                log.Write(LogLevel.Debug, $"Socket {s.Socket.Id} Authorization completed");
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
