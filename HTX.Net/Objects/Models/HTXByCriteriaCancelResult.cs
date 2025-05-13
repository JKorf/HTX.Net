using CryptoExchange.Net.Converters.SystemTextJson;


namespace HTX.Net.Objects.Models
{
	/// <summary>
	/// Result of Cancel Orders by Criteria
	/// </summary>
    [SerializationModel]
	public record HTXByCriteriaCancelResult
	{
		/// <summary>
		/// The number of cancel request sent successfully
		/// </summary>
		[JsonPropertyName("success-count")]
		public long Successful { get; set; }
		/// <summary>
		/// The number of cancel request failed
		/// </summary>
		[JsonPropertyName("failed-count")]
		public long Failed { get; set; }
		/// <summary>
		/// the next order id that can be canceled
		/// </summary>
		[JsonPropertyName("next-id")]
		public long NextId { get; set; }
	}
}
