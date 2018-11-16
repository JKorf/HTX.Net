using CryptoExchange.Net.Objects;

namespace Huobi.Net
{
    public class HuobiClientOptions: ExchangeOptions
    {
        public HuobiClientOptions()
        {
            BaseAddress = "https://api.huobi.pro";
        }
    }
}
