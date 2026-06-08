namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Account update
    /// </summary>
    [SerializationModel]
    public record HTXAccountUpdateV5 : HTXAccountBalanceV5
    {
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
    }
}
