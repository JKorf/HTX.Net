using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Deduction switch type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DeductionSwitchType>))]
    public enum DeductionSwitchType
    {
        /// <summary>
        /// ["<c>0</c>"] Point card deduction
        /// </summary>
        [Map("0")]
        PointCardDeduction,
        /// <summary>
        /// ["<c>1</c>"] Asset deduction
        /// </summary>
        [Map("1")]
        AssetDeduction,
        /// <summary>
        /// ["<c>2</c>"] Close deduction
        /// </summary>
        [Map("2")]
        CloseDeduction
    }
}
