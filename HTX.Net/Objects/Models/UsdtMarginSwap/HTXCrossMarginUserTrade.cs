using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginUserTrade : HTXIsolatedMarginUserTrade
    {
    }
}
