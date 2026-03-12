using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LightningPriceType>))]
    public enum LightningPriceType
    {
        /// <summary>
        /// ["<c>market</c>"] Market
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>lightning_fok</c>"] Fill or kill
        /// </summary>
        [Map("lightning_fok")]
        LightningFok,
        /// <summary>
        /// ["<c>lightning_ioc</c>"] Immediate or cancel
        /// </summary>
        [Map("lightning_ioc")]
        LightningIoc
    }
}
