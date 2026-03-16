using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.ExtensionMethods;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets;
using HTX.Net.Objects.Sockets.Queries;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HTX.Net
{
    internal class HTXAuthenticationProvider : AuthenticationProvider<HTXCredentials>
    {
        private readonly bool _signPublicRequests;

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.HMAC, ApiCredentialsType.Ed25519];

        public override string Key => ApiCredentials.Key;

        public HTXAuthenticationProvider(HTXCredentials credentials, bool signPublicRequests) : base(credentials)
        {
            _signPublicRequests = signPublicRequests;
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated && !_signPublicRequests)
                return;

            request.QueryParameters ??= new Dictionary<string, object>();
            request.QueryParameters.Add("AccessKeyId", ApiCredentials.Key);
            if (ApiCredentials.CredentialType == ApiCredentialsType.HMAC)
                request.QueryParameters.Add("SignatureMethod", "HmacSHA256");
            else if (ApiCredentials.CredentialType == ApiCredentialsType.Ed25519)
                request.QueryParameters.Add("SignatureMethod", "Ed25519");
            request.QueryParameters.Add("SignatureVersion", 2);
            request.QueryParameters.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            // Russian api has /api prefix which shouldn't be part of the signature
            var path = request.Path.StartsWith("/api") ? request.Path.Substring(4) : request.Path;
            
            var sortedParameters = request.QueryParameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer()).ToDictionary(x => x.Key, x => x.Value);
            var paramString = sortedParameters.CreateParamString(true, request.ArraySerialization);
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant());

            var host = request.BaseAddress.Substring(request.BaseAddress.IndexOf("/") + 2);
            var signData = $"{request.Method}\n{host}\n{path}\n{paramString}";

            var signature = ApiCredentials.CredentialType == ApiCredentialsType.HMAC
                ? SignHMACSHA256(ApiCredentials.HMAC!, signData, SignOutputType.Base64)
#if NET8_0_OR_GREATER
                : SignEd25519(ApiCredentials.Ed25519!, signData, SignOutputType.Base64)
#else
                : throw new NotSupportedException("Ed25519 signing is only supported on .NET 8.0 or greater");
#endif
                ;
            request.QueryParameters.Add("Signature", signature);
            request.SetQueryString($"{paramString}&Signature={WebUtility.UrlEncode(signature)}");
        }

        public override Query? GetAuthenticationQuery(SocketApiClient apiClient, SocketConnection connection, Dictionary<string, object?>? context = null)
        {
            object? version = null;
            context?.TryGetValue("version", out version);

            var uri = connection.ConnectionUri;
            if ((string?)version == "2")
            {
                var parameters = new ParameterCollection();
                parameters.Add("AccessKeyId", ApiCredentials.Key);
                parameters.Add("SignatureMethod", "HmacSHA256");
                parameters.Add("SignatureVersion", 2);
                parameters.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

                var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
                var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
                paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
                var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
                var signature = SignHMACSHA256(ApiCredentials.HMAC!, signData, SignOutputType.Base64);

                var request = new HTXAuthenticationRequest2(ApiCredentials.Key, (string)parameters["Timestamp"], signature);
                return new HTXOpAuthQuery(apiClient, request);
            }
            else
            {
                var parameters = new ParameterCollection();
                parameters.Add("accessKey", ApiCredentials.Key);
                parameters.Add("signatureMethod", "HmacSHA256");
                parameters.Add("signatureVersion", 2.1);
                parameters.Add("timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

                var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
                var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
                paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
                var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
                var signature = SignHMACSHA256(ApiCredentials.HMAC!,signData, SignOutputType.Base64);

                var authParams = new HTXAuthParams { AccessKey = ApiCredentials.Key, Timestamp = (string)parameters["timestamp"], Signature = signature };

                return new HTXAuthQuery(apiClient, new HTXAuthRequest<HTXAuthParams>
                {
                    Action = "req",
                    Channel = "auth",
                    Params = authParams
                });
            }
        }
    }
}
