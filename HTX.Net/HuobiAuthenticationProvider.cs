using CryptoExchange.Net.Clients;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HTX.Net
{
    internal class HTXAuthenticationProvider : AuthenticationProvider
    {
        private readonly bool _signPublicRequests;

        public HTXAuthenticationProvider(ApiCredentials credentials, bool signPublicRequests) : base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");

            _signPublicRequests = signPublicRequests;
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            if (!auth && !_signPublicRequests)
                return;

            // These are always in the uri
            uriParameters ??= new ParameterCollection();
            uriParameters.Add("AccessKeyId", _credentials.Key);
            uriParameters.Add("SignatureMethod", "HmacSHA256");
            uriParameters.Add("SignatureVersion", 2);
            uriParameters.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var absolutePath = uri.AbsolutePath;
            if (absolutePath.StartsWith("/api"))
                // Russian api has /api prefix which shouldn't be part of the signature
                absolutePath = absolutePath.Substring(4);

            var sortedParameters = uriParameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, arraySerialization).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant());
            var signData = $"{method}\n{uri.Host}\n{absolutePath}\n{paramString}";
            uriParameters.Add("Signature", SignHMACSHA256(signData, SignOutputType.Base64));
        }

        public HTXAuthParams GetWebsocketAuthentication(Uri uri)
        {
            var parameters = new ParameterCollection();
            parameters.Add("accessKey", _credentials.Key);
            parameters.Add("signatureMethod", "HmacSHA256");
            parameters.Add("signatureVersion", 2.1);
            parameters.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
            var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
            var signature = SignHMACSHA256(signData, SignOutputType.Base64);

            return new HTXAuthParams { AccessKey = _credentials.Key, Timestamp = (string)parameters["timestamp"], Signature = signature };
        }

        public HTXAuthenticationRequest2 GetWebsocketAuthentication2(Uri uri)
        {
            var parameters = new ParameterCollection();
            parameters.Add("AccessKeyId", _credentials.Key);
            parameters.Add("SignatureMethod", "HmacSHA256");
            parameters.Add("SignatureVersion", 2);
            parameters.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
            var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
            var signature = SignHMACSHA256(signData, SignOutputType.Base64);

            return new HTXAuthenticationRequest2(_credentials.Key, (string)parameters["Timestamp"], signature);
        }
    }
}
