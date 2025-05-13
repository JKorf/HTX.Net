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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HTX.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;

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

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.huobi.pro")]
        [TestCase("", "https://api.huobi.pro")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "HTX:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddHTX(configuration.GetSection("HTX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IHTXRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "HTX", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddHTX(configuration.GetSection("HTX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IHTXRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.huobi.pro"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "HTX:Environment:Name", "test" },
                    { "HTX:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddHTX(configuration.GetSection("HTX"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IHTXRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.huobi.pro"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddHTX(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IHTXRestClient>();
            var socketClient = provider.GetRequiredService<IHTXSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
