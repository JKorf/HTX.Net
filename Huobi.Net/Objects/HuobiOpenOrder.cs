using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Globalization;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Open order
    /// </summary>
    public class HuobiOpenOrder: ICommonOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The order id as specified by the client
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;

        /// <summary>
        /// The symbol of the order
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The id of the account that placed the order
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// The amount of the order
        /// </summary>
        [JsonProperty("amount")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// The price of the order
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// The time the order was created
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The time the order was canceled
        /// </summary>
        [JsonProperty("canceled-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CanceledAt { get; set; }
        /// <summary>
        /// The time the order was finished
        /// </summary>
        [JsonProperty("finished-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime FinishedAt { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType Type { get; set; }

        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonProperty("source"), JsonOptionalProperty]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter))]
        public HuobiOrderState State { get; set; }

        /// <summary>
        /// The amount of the order that is filled
        /// </summary>
        [JsonProperty("filled-amount")]
        public decimal FilledQuantity { get; set; }

        /// <summary>
        /// Filled cash amount
        /// </summary>
        [JsonProperty("filled-cash-amount")]
        public decimal FilledCashQuantity { get; set; }

        /// <summary>
        /// The amount of fees paid for the filled amount
        /// </summary>
        [JsonProperty("filled-fees")]
        public decimal FilledFees { get; set; }

        string ICommonOrderId.CommonId => Id.ToString(CultureInfo.InvariantCulture);
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price;
        decimal ICommonOrder.CommonQuantity => Quantity;
        DateTime ICommonOrder.CommonOrderTime => CreatedAt;
        IExchangeClient.OrderStatus ICommonOrder.CommonStatus =>
            State == HuobiOrderState.Created || State == HuobiOrderState.PartiallyFilled || State == HuobiOrderState.PreSubmitted || State == HuobiOrderState.Submitted ? IExchangeClient.OrderStatus.Active :
            State == HuobiOrderState.Filled ? IExchangeClient.OrderStatus.Filled :
            IExchangeClient.OrderStatus.Canceled;

        bool ICommonOrder.IsActive =>
            State == HuobiOrderState.Created ||
            State == HuobiOrderState.PreSubmitted ||
            State == HuobiOrderState.Submitted ||
            State == HuobiOrderState.PartiallyFilled;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Type.ToString().ToLowerInvariant().Contains("buy")
            ? IExchangeClient.OrderSide.Buy
            : IExchangeClient.OrderSide.Sell;

        IExchangeClient.OrderType ICommonOrder.CommonType
        {
            get
            {
                if (Type == HuobiOrderType.LimitBuy
                    || Type == HuobiOrderType.LimitSell)
                    return IExchangeClient.OrderType.Limit;
                if (Type == HuobiOrderType.MarketBuy
                    || Type == HuobiOrderType.MarketSell)
                    return IExchangeClient.OrderType.Market;
                return IExchangeClient.OrderType.Other;
            }
        }
    }
}
