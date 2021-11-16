using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
	/// <summary>
	/// Result of Cancel Orders by Criteria
	/// </summary>
	public class HuobiByCriteriaCancelResult
	{
		/// <summary>
		/// The number of cancel request sent successfully
		/// </summary>
		[JsonProperty("success-count")]
		public long Successful { get; set; }
		/// <summary>
		/// The number of cancel request failed
		/// </summary>
		[JsonProperty("failed-count")]
		public long Failed { get; set; }
		/// <summary>
		/// the next order id that can be canceled
		/// </summary>
		[JsonProperty("next-id")]
		public long NextId { get; set; }
	}
}