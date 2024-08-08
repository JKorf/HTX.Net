using System;
using System.Threading;
using System.Threading.Tasks;
using HTX.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text.Json;

namespace HTX.Net.UnitTests
{
    [TestFixture]
    public class HTXSocketClientTests
    {
        [Test]
        public void SubscribeV1_Should_SucceedIfSubbedResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHBTC", 1, test => { });
            var id = JsonDocument.Parse(socket.LastSendMessage).RootElement.GetProperty("id").GetString();
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\":\"{id}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            // assert
            Assert.That(subResult.Success);
        }

        [Test]
        public async Task SubscribeV1_Should_FailIfNoResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, x =>
            {
                x.RequestTimeout = TimeSpan.FromMilliseconds(10);
            });

            // act
            var subResult = await client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHBTC", 1, test => { });

            // assert
            ClassicAssert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV1_Should_FailIfErrorResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHBTC", 1, test => { });
            var id = JsonDocument.Parse(socket.LastSendMessage).RootElement.GetProperty("id").GetString();
            socket.InvokeMessage($"{{\"status\": \"error\", \"id\": \"{id}\", \"err-code\": \"Fail\", \"err-msg\": \"failed\"}}");
            var subResult = subTask.Result;

            // assert
            ClassicAssert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_SucceedIfSubbedResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            // act
            var subTask = client.SpotApi.SubscribeToAccountUpdatesAsync(test => { });
             socket.InvokeMessage("{\"action\": \"req\", \"code\": 200, \"ch\": \"auth\"}");
            Thread.Sleep(10);
             socket.InvokeMessage("{\"action\": \"sub\", \"code\": 200, \"ch\": \"accounts.update#1\"}");
            var subResult = subTask.Result;

            // assert
            Assert.That(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_FailIfAuthErrorResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SpotApi.SubscribeToAccountUpdatesAsync(test => { });
            socket.InvokeMessage("{ \"action\": \"req\", \"ch\": \"auth\", \"code\": 400}");
            var subResult = subTask.Result;

            // assert
            ClassicAssert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeV2_Should_FailIfNoResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, x =>
            {
                x.RequestTimeout = TimeSpan.FromMilliseconds(10);
            });

            // act
            var subTask = client.SpotApi.SubscribeToAccountUpdatesAsync(test => { });
            var subResult = subTask.Result;

            // assert
            ClassicAssert.IsFalse(subResult.Success);
        }
    }
}
