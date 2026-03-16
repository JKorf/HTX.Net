using CryptoExchange.Net.Authentication;

namespace HTX.Net
{
    /// <summary>
    /// HTX API credentials
    /// </summary>
    public class HTXCredentials : ApiCredentials
    {
        /// <summary>
        /// Credential type provided
        /// </summary>
        public ApiCredentialsType CredentialType => CredentialPairs.First().CredentialType;

        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public HTXCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key, and secret
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public HTXCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }

        /// <summary>
        /// Create HTX credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public HTXCredentials(HMACCredential credential) : base(credential) { }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Create HTX credentials using Ed25519 credentials
        /// </summary>
        /// <param name="ed25519Credential">The Ed25519 credential</param>
        public HTXCredentials(Ed25519Credential ed25519Credential)
            : base(ed25519Credential)
        {
        }
#endif

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new HTXCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
