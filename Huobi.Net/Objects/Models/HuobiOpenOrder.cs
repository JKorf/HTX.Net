using System;
using System.Globalization;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
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
        [JsonProperty("client-order-id")]
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
        /// The quantity of the order
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
        [JsonProperty("created-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// The time the order was canceled
        /// </summary>
        [JsonProperty("canceled-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CancelTime { get; set; }
        /// <summary>
        /// The time the order was completed
        /// </summary>
        [JsonProperty("finished-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CompleteTime { get; set; }

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public OrderType Type { get; set; }

        /// <summary>
        /// The source of the order
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(OrderStateConverter))]
        public OrderState State { get; set; }

        /// <summary>
        /// The quantity of the order that is filled
        /// </summary>
        [JsonProperty("filled-amount")]
        public decimal QuantityFilled { get; set; }

        /// <summary>
        /// Filled cash quantity
        /// </summary>
        [JsonProperty("filled-cash-amount")]
        public decimal QuoteQuantityFilled { get; set; }

        /// <summary>
        /// The quantity of fees paid for the filled quantity
        /// </summary>
        [JsonProperty("filled-fees")]
        public decimal Fee { get; set; }

        string ICommonOrderId.CommonId => Id.ToString(CultureInfo.InvariantCulture);
        string ICommonOrder.CommonSymbol => Symbol;
        decimal ICommonOrder.CommonPrice => Price;
        decimal ICommonOrder.CommonQuantity => Quantity;
        DateTime ICommonOrder.CommonOrderTime => CreateTime;
        IExchangeClient.OrderStatus ICommonOrder.CommonStatus =>
            State == OrderState.Created || State == OrderState.PartiallyFilled || State == OrderState.PreSubmitted || State == OrderState.Submitted ? IExchangeClient.OrderStatus.Active :
            State == OrderState.Filled ? IExchangeClient.OrderStatus.Filled :
            IExchangeClient.OrderStatus.Canceled;

        bool ICommonOrder.IsActive =>
            State == OrderState.Created ||
            State == OrderState.PreSubmitted ||
            State == OrderState.Submitted ||
            State == OrderState.PartiallyFilled;

        IExchangeClient.OrderSide ICommonOrder.CommonSide => Type.ToString().ToLowerInvariant().Contains("buy")
            ? IExchangeClient.OrderSide.Buy
            : IExchangeClient.OrderSide.Sell;

        IExchangeClient.OrderType ICommonOrder.CommonType
        {
            get
            {
                if (Type == OrderType.LimitBuy
                    || Type == OrderType.LimitSell)
                    return IExchangeClient.OrderType.Limit;
                if (Type == OrderType.MarketBuy
                    || Type == OrderType.MarketSell)
                    return IExchangeClient.OrderType.Market;
                return IExchangeClient.OrderType.Other;
            }
        }
    }
}
