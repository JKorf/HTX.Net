namespace HTX.Net.Objects.Models
{
	/// <summary>
	/// Result of Cancel Orders by Criteria
	/// </summary>
    [SerializationModel]
	public record HTXByCriteriaCancelResult
	{
		/// <summary>
		/// ["<c>success-count</c>"] The number of cancel request sent successfully
		/// </summary>
		[JsonPropertyName("success-count")]
		public long Successful { get; set; }
		/// <summary>
		/// ["<c>failed-count</c>"] The number of cancel request failed
		/// </summary>
		[JsonPropertyName("failed-count")]
		public long Failed { get; set; }
		/// <summary>
		/// ["<c>next-id</c>"] the next order id that can be canceled
		/// </summary>
		[JsonPropertyName("next-id")]
		public long NextId { get; set; }
	}
}
