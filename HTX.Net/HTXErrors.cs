using CryptoExchange.Net.Objects.Errors;

namespace HTX.Net
{
    internal static class HTXErrors
    {
        internal static ErrorMapping SpotMapping { get; } = new ErrorMapping([

                new ErrorInfo(ErrorType.SystemError, false, "System error", "500"),
                new ErrorInfo(ErrorType.SystemError, true, "System internal error", "base-system-error"),
                new ErrorInfo(ErrorType.SystemError, true, "Failed to retrieve data", "base-record-invalid"),

                new ErrorInfo(ErrorType.Unauthorized, false, "Forbidden", "1002"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP address not allowed", "2000"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Invalid signature", "1003"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter invalid", "validation-constraints-error"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter not supported", "base-argument-unsupported"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Timestamp exceeds limit", "order-date-limit-error"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid startTime", "invalid-start-date", "invalid-start-time"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid endTime", "invalid-end-date", "invalid-end-time"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Trigger price decimal precision invalid", "3003"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameter value", "parameter-required", "validation-constraints-required"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity more than max quantity", "order-amount-over-limit", "order-limitorder-amount-max-error", "3005", "3010"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity less than min quantity", "order-limitorder-amount-min-error", "order-marketorder-amount-min-error", "3004", "3009"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity decimal precision invalid", "order-orderamount-precision-error", "3002"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order value too low", "order-value-min-error", "3008"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Order value too high", "order-marketorder-amount-buy-max-error", "order-marketorder-amount-sell-max-error"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Price deviates too much from the last price", "order-invalid-price"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price too low", "order-limitorder-price-min-error", "order-price-greater-than-limit", "3007"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price too high", "order-limitorder-price-max-error", "order-price-less-than-limit", "3006"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price decimal precision invalid", "order-orderprice-precision-error"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "The price exceeds the protective price during limit-price trading", "price-exceeds-the-protective-price-during-limit-price-trading"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "invalid-client-order-id"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "order-accountbalance-error"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "40403", "510", "1006"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order not found", "not-found"),

                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Order not allowed during protection phase", "forbidden-trade-for-open-protect"),
                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol not trading", "base-symbol-trade-disabled", "2003"),

                new ErrorInfo(ErrorType.IncorrectState, false, "Order state invalid", "order-orderstate-error"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "The symbol is pending and not allowed to place order", "order-disabled"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "The symbol is pending and not allowed to cancel order", "cancel-disabled"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Stop order would trigger immediately", "order-stop-order-hit-trigger"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Market orders not supported during limit price trading", "market-orders-not-support-during-limit-price-trading", "3100"),

            ],
            [
                new ErrorEvaluator("api-signature-not-valid", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.Unauthorized, false, "API signature error", code);

                    if (msg.Contains("Incorrect Access key"))
                        return new ErrorInfo(ErrorType.Unauthorized, false, "Invalid API key", code);

                    if (msg.Equals("APIkey has expired"))
                        return new ErrorInfo(ErrorType.Unauthorized, false, "API key expired", code);

                    return new ErrorInfo(ErrorType.Unauthorized, false, msg, code);
                }),

                new ErrorEvaluator("invalid-parameter", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", code);

                    if (msg.Equals("invalid symbol"))
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", code);

                    if (msg.Equals("invalid size"))
                        return new ErrorInfo(ErrorType.InvalidQuantity, false, "Invalid quantity", code);

                    if (msg.Equals("request timeout"))
                        return new ErrorInfo(ErrorType.Timeout, true, "Request timeout", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, msg, code);
                }),

                new ErrorEvaluator("bad-request", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", code);

                    if (msg.Equals("invalid symbol"))
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", code);

                    if (msg.Equals("symbol trade not open now"))
                        return new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol not currently trading", code);

                    if (msg.Equals("429 too many request"))
                        return new ErrorInfo(ErrorType.RateLimitRequest, true, "Too many requests", code);

                    if (msg.Equals("request timeout"))
                        return new ErrorInfo(ErrorType.Timeout, true, "Request timeout", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, msg, code);
                }),

                new ErrorEvaluator("2001", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", code);

                    if (msg.Equals("invalid.symbol"))
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, msg, code);
                }),

                new ErrorEvaluator("2002", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", code);

                    if (msg.Equals("invalid field value in currency"))
                        return new ErrorInfo(ErrorType.UnknownAsset, false, "Invalid asset", code);

                    if (msg.Equals("auth.fail"))
                        return new ErrorInfo(ErrorType.Unauthorized, false, "Authentication failed", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, msg, code);
                }),

                new ErrorEvaluator("4000", (code, msg) => {
                    if (msg == null)
                        return new ErrorInfo(ErrorType.Unknown, false, "Unknown", code);

                    if (msg.Equals("too.many.request"))
                        return new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", code);

                    if (msg.Equals("too.many.connection"))
                        return new ErrorInfo(ErrorType.RateLimitConnection, false, "Too many connections", code);

                    return new ErrorInfo(ErrorType.Unknown, false, msg, code);
                })
                ]);

        internal static ErrorMapping FuturesMapping { get; } = new ErrorMapping([

                new ErrorInfo(ErrorType.Unauthorized, true, "API key expired", "12004"),
                new ErrorInfo(ErrorType.Unauthorized, true, "IP Address not allowed", "12005"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Incorrect public key", "12007"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Verification failed", "12008"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Signature verification failed", "1253"),
                new ErrorInfo(ErrorType.Unauthorized, true, "API access disabled", "1084"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Contract trading disabled because of account balance", "1221"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Contract trading disabled because of account age", "1222"),
                new ErrorInfo(ErrorType.Unauthorized, true, "VIP level too low", "1223"),
                new ErrorInfo(ErrorType.Unauthorized, true, "Contract trading disabled because of region", "1224"),
                new ErrorInfo(ErrorType.Unauthorized, true, "KYC verification required", "1231"),

                new ErrorInfo(ErrorType.SystemError, true, "System error", "1000", "1001", "1046", "1108"),
                new ErrorInfo(ErrorType.SystemError, true, "System busy", "1004"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol doesn't exist", "1013", "1014", "1332"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order not found", "1017", "1061"),
                new ErrorInfo(ErrorType.UnknownOrder, false, "No orders to cancel", "1051"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Input error", "1030", "1035", "1036", "1347"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Illegal parameter", "1067"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Contract period invalid", "1033"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Price type invalid", "1034"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid leverage", "1037"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Exceeded batch order limit", "1052"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Take profit price error", "1405"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Stop loss price error", "1407"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Reduce only parameter error", "1491"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Parameter can't be empty", "1066"),

                new ErrorInfo(ErrorType.InvalidQuantity, false, "Invalid quantity", "1040"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity should be more than 0", "1322"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Quantity exceeds max order quantity", "1041"),
                new ErrorInfo(ErrorType.InvalidQuantity, false, "Reduce only quantity exceeds open position quantity", "1492"),

                new ErrorInfo(ErrorType.InvalidPrice, false, "Price decimal precision invalid", "1038", "1412"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price deviates too much from current price", "1039"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid price", "1055", "1069", "1075"),
                new ErrorInfo(ErrorType.InvalidPrice, false, "Price too high", "1072"),

                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient quantity available", "1301"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient margin available", "1047"),
                new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient close quantity available", "1048"),

                new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol not currently trading", "1057", "1058"),

                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Symbol in settlement, can't place or cancel orders", "1056"),
                new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Symbol in delivery, can't place or cancel orders", "1059"),

                new ErrorInfo(ErrorType.IncorrectState, false, "Order has been canceled", "1063", "1071"),
                new ErrorInfo(ErrorType.IncorrectState, false, "Position is in forced liquidation", "1083"),

                new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "1032"),
                new ErrorInfo(ErrorType.RateLimitOrder, false, "Too many TP/SL orders", "1403"),
            ]);

    }
}