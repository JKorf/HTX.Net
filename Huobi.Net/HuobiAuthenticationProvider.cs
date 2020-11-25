using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Huobi.Net
{
    internal class HuobiAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly object encryptLock = new object();
        private readonly bool signPublicRequests;

        public HuobiAuthenticationProvider(ApiCredentials credentials, bool signPublicRequests) : base(credentials)
        {
            this.signPublicRequests = signPublicRequests;

            if(credentials.Secret == null)
                throw new ArgumentException("ApiKey/secret not provided");

            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (!signed && !signPublicRequests)
                return parameters;

            return SignRequest(uri, method, parameters, 
                "AccessKeyId", "SignatureMethod", "SignatureVersion", "Timestamp", "Signature", 2);
        }

        internal Dictionary<string, object> SignRequest(
            string uri, 
            HttpMethod method, 
            Dictionary<string, object> parameters,
            string accessKeyName, 
            string methodName,
            string versionName,
            string timestampName,
            string signatureName,
            double signatureVersion)
        {
            if (Credentials.Key == null)
                throw new ArgumentException("ApiKey/secret not provided");

            var uriObj = new Uri(uri);
            var signParameters = new Dictionary<string, object>
            {
                { accessKeyName, Credentials.Key.GetString() },
                { methodName, "HmacSHA256" },
                { versionName, signatureVersion }
            };

            if (!parameters.ContainsKey(timestampName) || method != HttpMethod.Get)
                signParameters.Add(timestampName, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"));

            if (method == HttpMethod.Get)
            {
                foreach (var kvp in parameters)
                    signParameters.Add(kvp.Key, kvp.Value);
            }

            signParameters = signParameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)), new ByteOrderComparer()).ToDictionary(k => k.Key, k => k.Value);

            var paramString = signParameters.CreateParamString(true, ArrayParametersSerialization.MultipleValues);
            paramString = paramString.Replace("%2C", ".");
            
            signParameters = signParameters.OrderBy(kv => kv.Key).ToDictionary(k => k.Key, k => k.Value);

            var absolutePath = uriObj.AbsolutePath;
            if (absolutePath.StartsWith("/api"))
                // Russian api has /api prefix which shouldn't be part of the signature
                absolutePath = absolutePath.Substring(4);

            var signData = method + "\n";
            signData += uriObj.Host + "\n";
            signData += absolutePath + "\n";
            signData += paramString;
            byte[] signBytes;
            lock (encryptLock)
                signBytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData));
            signParameters.Add(signatureName, Convert.ToBase64String(signBytes));

            if (method != HttpMethod.Get)
            {
                foreach (var kvp in parameters)
                    signParameters.Add(kvp.Key, kvp.Value);
            }

            return signParameters;
        }
    }
}
