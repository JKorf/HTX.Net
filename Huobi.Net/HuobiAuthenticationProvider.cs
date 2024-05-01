using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Huobi.Net
{
    internal class HuobiAuthenticationProvider : AuthenticationProvider
    {
        private readonly bool _signPublicRequests;

        public HuobiAuthenticationProvider(ApiCredentials credentials, bool signPublicRequests) : base(credentials)
        {
            if (credentials.CredentialType != ApiCredentialsType.Hmac)
                throw new Exception("Only Hmac authentication is supported");

            _signPublicRequests = signPublicRequests;
        }

        public override void AuthenticateRequest(RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            IDictionary<string, object> uriParams,
            IDictionary<string, object> bodyParams,
            Dictionary<string, string> headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat bodyFormat)
        {
            if (!auth && !_signPublicRequests)
                return;

            // These are always in the uri
            uriParams.Add("AccessKeyId", _credentials.Key!.GetString());
            uriParams.Add("SignatureMethod", "HmacSHA256");
            uriParams.Add("SignatureVersion", 2);
            uriParams.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var absolutePath = uri.AbsolutePath;
            if (absolutePath.StartsWith("/api"))
                // Russian api has /api prefix which shouldn't be part of the signature
                absolutePath = absolutePath.Substring(4);

            var sortedParameters = uriParams.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, arraySerialization).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant());
            var signData = $"{method}\n{uri.Host}\n{absolutePath}\n{paramString}";
            uriParams.Add("Signature", SignHMACSHA256(signData, SignOutputType.Base64));
        }

        public HuobiAuthParams GetWebsocketAuthentication(Uri uri)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("accessKey", _credentials.Key!.GetString());
            parameters.Add("signatureMethod", "HmacSHA256");
            parameters.Add("signatureVersion", 2.1);
            parameters.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
            var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
            var signature = SignHMACSHA256(signData, SignOutputType.Base64);

            return new HuobiAuthParams { AccessKey = _credentials.Key!.GetString(), Timestamp = (string)parameters["timestamp"], Signature = signature };
        }

        public HuobiAuthenticationRequest2 GetWebsocketAuthentication2(Uri uri)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("AccessKeyId", _credentials.Key!.GetString());
            parameters.Add("SignatureMethod", "HmacSHA256");
            parameters.Add("SignatureVersion", 2);
            parameters.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
            var paramString = uri.SetParameters(sortedParameters, ArrayParametersSerialization.Array).Query.Replace("?", "");
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
            var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
            var signature = SignHMACSHA256(signData, SignOutputType.Base64);

            return new HuobiAuthenticationRequest2(_credentials.Key!.GetString(), (string)parameters["Timestamp"], signature);
        }
    }
}
