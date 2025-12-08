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
        /// Market
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("lightning_fok")]
        LightningFok,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("lightning_ioc")]
        LightningIoc
    }
}
