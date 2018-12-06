using System;
using System.Collections.Generic;
using System.Text;
using Huobi.Net.Objects;
using Huobi.Net.Objects.SocketObjects;
using Huobi.Net.UnitTests.TestImplementations;
using NUnit.Framework;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class HuobiSocketClientTests
    {
        [Test]
        public void SubscribeV1_Should_SucceedIfSubbedResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            // act
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => { });
            socket.InvokeMessage("{\"subbed\": \"test\"}");
            var subResult = subTask.Result;

            // assert
            Assert.IsTrue(subResult.Success);
        }

        [Test]
        public void SubscribeV1_Should_FailIfNoResponse()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket, new HuobiSocketClientOptions()
            {
                SocketResponseTimeout = TimeSpan.FromMilliseconds(10)
            });

            // act
            var subTask = client.SubscribeToMarketDepthUpdatesAsync("test", 1, test => { });
            var subResult = subTask.Result;

            // assert
            Assert.IsFalse(subResult.Success);
        }
    }
}
