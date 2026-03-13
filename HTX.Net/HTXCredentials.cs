using CryptoExchange.Net.Authentication;

namespace HTX.Net
{
    /// <summary>
    /// HTX API credentials
    /// </summary>
    public class HTXCredentials : ApiCredentials
    {
        public ApiCredentialsType CredentialType => CredentialPairs.First().CredentialType;
        
        public HTXCredentials() { }

        public HTXCredentials(string apiKey, string secretKey)
            : this(new HMACCredential(apiKey, secretKey)) { }

        public HTXCredentials(HMACCredential hmacCredential)
            : base(hmacCredential) 
        {
        }

#if NET8_0_OR_GREATER
        public HTXCredentials(ED25519Credential ed25519Credential)
            : base(ed25519Credential)
        {
        }
#endif

        /// <inheritdoc />
        public override ApiCredentials Copy() =>
            CredentialType switch
            {
                ApiCredentialsType.Hmac => new HTXCredentials(GetCredential<HMACCredential>()!),
#if NET8_0_OR_GREATER
                ApiCredentialsType.Ed25519 => new HTXCredentials(GetCredential<ED25519Credential>()!),
#endif
            };
    }
}
