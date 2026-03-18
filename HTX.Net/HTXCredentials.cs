using CryptoExchange.Net.Authentication;

namespace HTX.Net
{
    /// <summary>
    /// HTX API credentials
    /// </summary>
    public class HTXCredentials : ApiCredentials
    {
        internal CredentialPair Credential { get; set; }

        public HMACCredential? HMAC
        {
            get => Credential as HMACCredential;
            set { if (value != null) Credential = value; }
        }

#if NET8_0_OR_GREATER
        public Ed25519Credential? Ed25519
        {
            get => Credential as Ed25519Credential;
            set { if (value != null) Credential = value; }
        }
#endif

        public HTXCredentials WithHMAC(string key, string secret)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new HMACCredential(key, secret);
            return this;
        }

#if NET8_0_OR_GREATER
        public HTXCredentials WithEd25519(string key, string secret)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new Ed25519Credential(key, secret);
            return this;
        }
#endif

        /// <inheritdoc />
        public override ApiCredentials Copy() => new HTXCredentials { Credential = Credential };
    }
}
