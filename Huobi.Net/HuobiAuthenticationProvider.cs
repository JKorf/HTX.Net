using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Huobi.Net
{
    public class HuobiAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly object locker;

        public HuobiAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            locker = new object();
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }

        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return parameters;

            var uriObj = new Uri(uri);
            Dictionary<string, object> signParameters = new Dictionary<string, object>
            {
                { "AccessKeyId", Credentials.Key.GetString() },
                { "SignatureMethod", "HmacSHA256" },
                { "SignatureVersion", 2 },
                { "Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") }
            };

            if (method == Constants.GetMethod)
                foreach (var kvp in parameters)
                    signParameters.Add(kvp.Key, kvp.Value);

            signParameters = signParameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)), new ByteOrderComparer()).ToDictionary(k => k.Key, k => k.Value);
            
            var paramString = signParameters.CreateParamString(true);
            signParameters = signParameters.OrderBy(kv => kv.Key).ToDictionary(k => k.Key, k => k.Value);

            string signData = method + "\n";
            signData += uriObj.Host + "\n";
            signData += uriObj.AbsolutePath + "\n";
            signData += paramString;
            var signBytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData));
            signParameters.Add("Signature", Convert.ToBase64String(signBytes));
            
            if (method != Constants.GetMethod)
                foreach (var kvp in parameters)
                    signParameters.Add(kvp.Key, kvp.Value);

            return signParameters;
        }
    }
}
