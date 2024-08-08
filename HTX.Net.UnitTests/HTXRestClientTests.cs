using HTX.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using HTX.Net.Clients;
using NUnit.Framework.Legacy;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace HTX.Net.UnitTests
{
    [TestFixture]
    public class HTXRestClientTests
    {
        [TestCase]
        public async Task ReceivingErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("{\"status\": \"error\", \"err-code\": \"Error!\", \"err-msg\": \"ErrorMessage\"}");

            // act
            var result = await client.SpotApi.ExchangeData.GetAssetsAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            Assert.That(result.Error.ToString().Contains("Error!"));
            Assert.That(result.Error.ToString().Contains("ErrorMessage"));
        }

        [TestCase]
        public async Task ReceivingHttpErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("Error message", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.SpotApi.ExchangeData.GetAssetsAsync();

            // assert
            ClassicAssert.IsFalse(result.Success);
            Assert.That(result.Error.ToString().Contains("Error message"));
        }


        public string SerializeExpected<T>(T data, bool tick)
        {
            return $"{{\"status\": \"ok\", {(tick ? "tick" : "data")}: {JsonSerializer.Serialize(data)}}}";
        }

        [Test]
        public void CheckSignatureExample()
        {
            var authProvider = new HTXAuthenticationProvider(
                new ApiCredentials("e2xxxxxx-99xxxxxx-84xxxxxx-7xxxx", "XXXXXXXXXX"),
                false
                );
            var client = (RestApiClient)new HTXRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Get,
                "/v1/order/orders",
                (uriParams, bodyParams, headers) =>
                {
                    return uriParams["Signature"].ToString();
                },
                "2ZQ7/roKBjdnAv8z5DymwzgSaOPyPgJl0BIlq9fa94w=",
                new Dictionary<string, object>
                {
                    { "order-id", "1234567890" }
                },
                time: new DateTime(2017, 5, 11, 15, 19, 30, DateTimeKind.Utc),
                host: "https://api.huobi.pro");
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<HTXRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<HTXSocketClient>();
        }
    }
}
