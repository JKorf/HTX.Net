using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi user info
    /// </summary>
    public class HuobiUser
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        [JsonProperty("uid")]
        public long Id { get; set; }
        /// <summary>
        /// The state of the user
        /// </summary>
        [JsonProperty("userState"), JsonConverter(typeof(UserStateConverter))]
        public UserState State { get; set; }
    }
}
