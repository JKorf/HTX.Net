using Huobi.Net.Objects;
using Huobi.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CryptoExchange.Net.Authentication;
using System.Threading.Tasks;
using Huobi.Net.Enums;
using System.Reflection;
using Huobi.Net.Client.Rest.Spot;
using System.Diagnostics;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class HuobiClientTests
    {
        [Test]
        public void SigningString_Should_GiveCorrectSignResult()
        {
            // arrange
            var authProvider = new HuobiAuthenticationProvider(new ApiCredentials("TestKey", "TestSecret"), false);

            // act
            var parameters = authProvider.AddAuthenticationToParameters("http://api.test.com/somepath/test", HttpMethod.Get, new Dictionary<string, object>()
            {
                { "Timestamp", new DateTime(2018, 1, 1).ToString("yyyy-MM-ddTHH:mm:ss") }
            }, true, CryptoExchange.Net.Objects.HttpMethodParameterPosition.InBody, CryptoExchange.Net.Objects.ArrayParametersSerialization.MultipleValues);

            // assert
            Assert.AreEqual(parameters["Signature"], "5/vfYFw3cHwp20QWtv6DzTHDxBpHzNSU6Rv3p7Up/TI=");
        }

        [TestCase]
        public async Task ReceivingErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("{{\"status\": \"error\", \"err-code\": \"Error!\", \"err-msg\": \"ErrorMessage\"}}");

            // act
            var result = await client.ExchangeData.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error.ToString().Contains("Error!"));
            Assert.IsTrue(result.Error.ToString().Contains("ErrorMessage"));
        }

        [TestCase]
        public async Task ReceivingHttpErrorResponse_Should_FailCall()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient("Error message", System.Net.HttpStatusCode.BadRequest);

            // act
            var result = await client.ExchangeData.GetAssetsAsync();

            // assert
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Error.ToString().Contains("Error message"));
        }


        public string SerializeExpected<T>(T data, bool tick)
        {
            return $"{{\"status\": \"ok\", {(tick ? "tick" : "data")}: {JsonConvert.SerializeObject(data)}}}";
        }

        [TestCase("BTCUSDT", true)]
        [TestCase("NANOUSDT", true)]
        [TestCase("NANOBTC", true)]
        [TestCase("ETHBTC", true)]
        [TestCase("BEETC", true)]
        [TestCase("BEEC", true)]
        [TestCase("BEC", false)]
        [TestCase("NANOUSDTD", true)]
        [TestCase("BTC-USDT", false)]
        [TestCase("BTC-USD", false)]
        public void CheckValidHuobiSymbol(string symbol, bool isValid)
        {
            if (isValid)
                Assert.DoesNotThrow(() => symbol.ValidateHuobiSymbol());
            else
                Assert.Throws(typeof(ArgumentException), () => symbol.ValidateHuobiSymbol());
        }

        [Test]
        public void CheckRestInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(HuobiClientSpot));
            var ignore = new string[] { "IHuobiClientSpot" };
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IHuobiClientSpot") && !ignore.Contains(t.Name));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod);
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }

        [Test]
        public void CheckSocketInterfaces()
        {
            var assembly = Assembly.GetAssembly(typeof(HuobiSocketClientSpot));
            var clientInterfaces = assembly.GetTypes().Where(t => t.Name.StartsWith("IHuobiSocketClientSpot"));

            foreach (var clientInterface in clientInterfaces)
            {
                var implementation = assembly.GetTypes().Single(t => t.IsAssignableTo(clientInterface) && t != clientInterface);
                int methods = 0;
                foreach (var method in implementation.GetMethods().Where(m => m.ReturnType.IsAssignableTo(typeof(Task<CallResult<UpdateSubscription>>))))
                {
                    var interfaceMethod = clientInterface.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
                    Assert.NotNull(interfaceMethod);
                    methods++;
                }
                Debug.WriteLine($"{clientInterface.Name} {methods} methods validated");
            }
        }
    }
}
