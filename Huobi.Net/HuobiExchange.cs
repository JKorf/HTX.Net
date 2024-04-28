namespace Huobi.Net
{
    /// <summary>
    /// Huobi exchange information and configuration
    /// </summary>
    public static class HuobiExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "Huobi";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.huobi.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://huobiapi.github.io/docs/spot/v1/en/#change-log"
            };
    }
}
