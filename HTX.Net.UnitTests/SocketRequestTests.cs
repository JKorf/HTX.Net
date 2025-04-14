using HTX.Net.Clients;
using HTX.Net.Enums;
using HTX.Net.Objects.Models;
using HTX.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.UnitTests
{
    [TestFixture]
    public class SocketRequestTests
    {
        private HTXSocketClient CreateClient()
        {
            var fact = new LoggerFactory();
            fact.AddProvider(new TraceLoggerProvider());
            var client = new HTXSocketClient(Options.Create(new HTXSocketOptions
            {
                OutputOriginalData = true,
                RequestTimeout = TimeSpan.FromSeconds(5),
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), fact);
            return client;
        }

        [Test]
        public async Task ValidateExchangeApiCalls()
        {
            var tester = new SocketRequestValidator<HTXSocketClient>("Socket/SpotApi");

            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.PlaceOrderAsync(123, "ETHUSDT", OrderSide.Buy, OrderType.Limit, 1), "PlaceOrder", nestedJsonProperty: "data", ignoreProperties: [ ]);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.PlaceMultipleOrdersAsync(new[] { new HTXOrderRequest() { AccountId = "123" } }), "PlaceMultipleOrders", nestedJsonProperty: "data", skipResponseValidation: true);
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.PlaceMarginOrderAsync(123, "ETHUSDT", OrderSide.Buy, OrderType.Limit, MarginPurpose.AutomaticLoan, SourceType.Spot), "PlaceMarginOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.CancelAllOrdersAsync(123), "CancelAllOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(CreateClient(), client => client.SpotApi.CancelOrdersAsync(["123"]), "CancelOrders", nestedJsonProperty: "data");
        }
    }
}
