using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Contract status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractStatus>))]
    public enum ContractStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Delisting
        /// </summary>
        [Map("0")]
        Delisting,
        /// <summary>
        /// ["<c>1</c>"] Listing
        /// </summary>
        [Map("1")]
        Listing,
        /// <summary>
        /// ["<c>2</c>"] Pending listing
        /// </summary>
        [Map("2")]
        PendingListing,
        /// <summary>
        /// ["<c>3</c>"] Suspension
        /// </summary>
        [Map("3")]
        Suspension,
        /// <summary>
        /// ["<c>4</c>"] Suspending of listing
        /// </summary>
        [Map("4")]
        SuspendingOfListing,
        /// <summary>
        /// ["<c>5</c>"] In settlement
        /// </summary>
        [Map("5")]
        InSettlement,
        /// <summary>
        /// ["<c>6</c>"] Delivering
        /// </summary>
        [Map("6")]
        Delivering,
        /// <summary>
        /// ["<c>7</c>"] Settlement completed
        /// </summary>
        [Map("7")]
        SettlementCompleted,
        /// <summary>
        /// ["<c>8</c>"] Delivered
        /// </summary>
        [Map("8")]
        Delivered
    }
}
