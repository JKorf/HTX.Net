﻿using CryptoExchange.Net.Clients;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets;
using System;
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

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated && !_signPublicRequests)
                return;

            request.QueryParameters.Add("AccessKeyId", _credentials.Key);
            request.QueryParameters.Add("SignatureMethod", "HmacSHA256");
            request.QueryParameters.Add("SignatureVersion", 2);
            request.QueryParameters.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

            // Russian api has /api prefix which shouldn't be part of the signature
            var path = request.Path.StartsWith("/api") ? request.Path.Substring(4) : request.Path;
            
            var sortedParameters = request.QueryParameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer()).ToDictionary(x => x.Key, x => x.Value);
            var paramString = sortedParameters.CreateParamString(true, request.ArraySerialization);
            paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant());

            var host = request.BaseAddress.Substring(request.BaseAddress.IndexOf("/") + 2);
            var signData = $"{request.Method}\n{host}\n{path}\n{paramString}";

            var signature = SignHMACSHA256(signData, SignOutputType.Base64);
            request.QueryParameters.Add("Signature", signature);
            request.SetQueryString($"{paramString}&Signature={WebUtility.UrlEncode(signature)}");
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
