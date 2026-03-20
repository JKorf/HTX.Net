using CryptoExchange.Net.Authentication;

namespace HTX.Net
{
    /// <summary>
    /// HTX API credentials
    /// </summary>
    public class HTXCredentials : ApiCredentials
    {
        internal CredentialSet Credential { get; set; }

        /// <summary>
        /// HMAC credentials
        /// </summary>
        public HMACCredential? HMAC
        {
            get => Credential as HMACCredential;
            set { if (value != null) Credential = value; }
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Ed25519 credentials
        /// </summary>
        public Ed25519Credential? Ed25519
        {
            get => Credential as Ed25519Credential;
            set { if (value != null) Credential = value; }
        }
#endif

        /// <summary>
        /// Create new credentials
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public HTXCredentials() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public HTXCredentials(string key, string secret)
        {
            Credential = new HMACCredential(key, secret);
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public HTXCredentials(HMACCredential credential)
        {
            Credential = credential;
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing Ed25519 credentials
        /// </summary>
        /// <param name="credential">Ed25519 credentials</param>
        public HTXCredentials(Ed25519Credential credential)
        {
            Credential = credential;
        }
#endif

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public HTXCredentials WithHMAC(string key, string secret)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new HMACCredential(key, secret);
            return this;
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Specify the Ed25519 credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public HTXCredentials WithEd25519(string key, string privateKey)
        {
            if (Credential != null) throw new InvalidOperationException("Credentials already set");

            Credential = new Ed25519Credential(key, privateKey);
            return this;
        }
#endif

        /// <inheritdoc />
        public override ApiCredentials Copy() => new HTXCredentials { Credential = Credential };

        /// <inheritdoc />
        public override void Validate()
        {
            if (Credential == null)
                throw new ArgumentException("Credential not set");

            Credential.Validate();
        }
    }
}
