using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
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

            parameters.Add("AccessKeyId", Credentials.Key.GetString());
            parameters.Add("SignatureMethod", "HmacSHA256");
            parameters.Add("SignatureVersion", 2);
            parameters.Add("Timestamp", WebUtility.UrlEncode(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss")));

            parameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)), new NaturalOrderByteArrayComparer()).ToDictionary(k => k.Key, k => k.Value);
            var paramString = parameters.CreateParamString().Substring(1);
            parameters = parameters.OrderBy(kv => kv.Key).ToDictionary(k => k.Key, k => k.Value);

            var uriObj = new Uri(uri);
            string signData = method + "\n";
            signData += uriObj.Host + "\n";
            signData += uriObj.AbsolutePath + "\n";
            signData += paramString;
            var signBytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes(signData));
            parameters.Add("Signature", WebUtility.UrlEncode(Convert.ToBase64String(signBytes)));

            return parameters;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            return base.AddAuthenticationToHeaders(uri, method, parameters, signed);
        }
    }

    // I could be wrong in that this is called natural order.
    class NaturalOrderByteArrayComparer : IComparer<byte[]>
    {
        public int Compare(byte[] x, byte[] y)
        {
            // Shortcuts: If both are null, they are the same.
            if (x == null && y == null) return 0;

            // If one is null and the other isn't, then the
            // one that is null is "lesser".
            if (x == null && y != null) return -1;
            if (x != null && y == null) return 1;

            // Both arrays are non-null.  Find the shorter
            // of the two lengths.
            int bytesToCompare = Math.Min(x.Length, y.Length);

            // Compare the bytes.
            for (int index = 0; index < bytesToCompare; ++index)
            {
                // The x and y bytes.
                byte xByte = x[index];
                byte yByte = y[index];

                // Compare result.
                int compareResult = Comparer<byte>.Default.Compare(xByte, yByte);

                // If not the same, then return the result of the
                // comparison of the bytes, as they were the same
                // up until now.
                if (compareResult != 0) return compareResult;

                // They are the same, continue.
            }

            // The first n bytes are the same.  Compare lengths.
            // If the lengths are the same, the arrays
            // are the same.
            if (x.Length == y.Length) return 0;

            // Compare lengths.
            return x.Length < y.Length ? -1 : 1;
        }
    }
}
