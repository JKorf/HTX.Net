﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using Huobi.Net.Enums;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
            var subTask = client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHBTC", 1, test => { });
            var id = JToken.Parse(socket.LastSendMessage)["id"];
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
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"status\": \"error\", \"id\": \"{id}\", \"err-code\": \"Fail\", \"err-msg\": \"failed\"}}");
            var subResult = subTask.Result;

            // assert
            ClassicAssert.IsFalse(subResult.Success);
        }

        [Test]
        public void SubscribeToDepthUpdates_Should_TriggerWithDepthUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiOrderBook result = null;
            var subTask = client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHBTC", 1, test => result = test.Data);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"subbed\": \"ethbtc\", \"status\": \"ok\", \"id\": \"{id}\"}}");
            var subResult = subTask.Result;

            var expected =  new HuobiOrderBook()
            {
                Asks = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry() {Quantity = 0.1m, Price = 0.2m}
                },
                Bids = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry() {Quantity = 0.3m, Price = 0.4m}
                }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.ethbtc.depth.step1", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected.Asks.ToList()[0], result.Asks.ToList()[0]));
            Assert.That(TestHelpers.AreEqual(expected.Bids.ToList()[0], result.Bids.ToList()[0]));
        }

        [Test]
        public void SubscribeToDetailUpdates_Should_TriggerWithDetailUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiSymbolDetails result = null;
            var subTask = client.SpotApi.SubscribeToSymbolDetailUpdatesAsync("ETHBTC", test => result = test.Data);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"subbed\": \"ethbtc\", \"id\": \"{id}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiSymbolData()
            {
                QuoteVolume = 0.1m,
                ClosePrice = 0.2m,
                LowPrice = 0.3m,
                HighPrice = 0.4m,
                Volume = 0.5m,
                OpenPrice = 0.6m,
                TradeCount = 12
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.ethbtc.detail", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected, result));
        }

        [Test]
        public void SubscribeToKlineUpdates_Should_TriggerWithKlineUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiSymbolData result = null;
            var subTask = client.SpotApi.SubscribeToKlineUpdatesAsync("ETHBTC", KlineInterval.FiveMinutes, test => result = test.Data);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"subbed\": \"ethbtc\", \"id\": \"{id}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiSymbolData()
            {
                QuoteVolume = 0.1m,
                ClosePrice = 0.2m,
                LowPrice = 0.3m,
                HighPrice = 0.4m,
                Volume = 0.5m,
                OpenPrice = 0.6m,
                TradeCount = 12
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.ethbtc.kline.5min", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected, result));
        }

        [Test]
        public void SubscribeToTickerUpdates_Should_TriggerWithTickerUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            IEnumerable<HuobiSymbolTicker> result = null;
            var subTask = client.SpotApi.SubscribeToTickerUpdatesAsync((test => result = test.Data));
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{id}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = new List<HuobiSymbolTicker>
            {
                new HuobiSymbolTicker()
                {
                    QuoteVolume = 0.1m,
                    ClosePrice = 0.2m,
                    LowPrice = 0.3m,
                    HighPrice = 0.4m,
                    Volume = 0.5m,
                    OpenPrice = 0.6m,
                    TradeCount = 12
                }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.tickers", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected[0], result.First()));
        }

        [Test]
        public void SubscribeToTradeUpdates_Should_TriggerWithTradeUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateSocketClient(socket);

            HuobiSymbolTrade result = null;
            var subTask = client.SpotApi.SubscribeToTradeUpdatesAsync("ethusdt", test => result = test.Data);
            var id = JToken.Parse(socket.LastSendMessage)["id"];
            socket.InvokeMessage($"{{\"subbed\": \"test\", \"id\": \"{id}\", \"status\": \"ok\"}}");
            var subResult = subTask.Result;

            var expected = 
                new HuobiSymbolTrade()
                {
                    Id = 123,
                    Timestamp = new DateTime(2018, 1, 1),
                    Details = new List<HuobiSymbolTradeDetails>()
                    {
                        new HuobiSymbolTradeDetails()
                        {
                            Id = "123",
                            Quantity = 0.1m,
                            Price = 0.2m,
                            Timestamp = new DateTime(2018,2,1),
                            Side = OrderSide.Buy
                        }
                    }
            };

            // act
            socket.InvokeMessage(SerializeExpected("market.ethusdt.trade.detail", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected, result, "Details"));
            Assert.That(TestHelpers.AreEqual(expected.Details.ToList()[0], result.Details.ToList()[0]));
        }

        [Test]
        public void SubscribeToAccountUpdates_Should_TriggerWithAccountUpdate()
        {
            // arrange
            var socket = new TestSocket();
            socket.CanConnect = true;
            var client = TestHelpers.CreateAuthenticatedSocketClient(socket);

            HuobiAccountUpdate result = null;
            var subTask = client.SpotApi.SubscribeToAccountUpdatesAsync(test => result = test.Data);
            socket.InvokeMessage("{\"ch\": \"auth\", \"code\": 200, \"action\": \"req\"}");
            Thread.Sleep(100);
            socket.InvokeMessage($"{{\"action\": \"sub\", \"code\": 200, \"ch\": \"accounts.update#1\"}}");
            var subResult = subTask.Result;

            var expected = new HuobiAccountUpdate()
            {
                AccountId = 123,
                AccountType = BalanceType.Frozen,
                Available = 456,
                Balance = 789,
                ChangeTime = new DateTime(2020, 11, 25),
                ChangeType = AccountEventType.Deposit,
                Asset = "usdt"
            };

            // act
            socket.InvokeMessage(SerializeExpectedAuth("accounts.update#1", expected));

            // assert
            Assert.That(subResult.Success);
            Assert.That(TestHelpers.AreEqual(expected, result));
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

        //[Test]
        //public async Task SubscribeV2_Should_FailIfErrorResponse()
        //{
        //    // arrange
        //    var socket = new TestSocket();
        //    socket.CanConnect = true;
        //    var client = TestHelpers.CreateSocketClient(socket);

        //    // act
        //    var subTask = client.SpotApi.SubscribeToAccountUpdatesAsync(test => { });
        //    socket.InvokeMessage("{\"op\": \"auth\"}");
        //    Thread.Sleep(10);
        //    var id = JToken.Parse(socket.LastSendMessage)["id"];
        //    socket.InvokeMessage($"{{\"op\": \"sub\", \"cid\": \"{id}\", \"status\": \"error\", \"err-code\": 1, \"err-msg\": \"failed\"}}");
        //    var subResult = subTask.Result;

        //    // assert
        //    ClassicAssert.IsFalse(subResult.Success);
        //}

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

        public string SerializeExpected<T>(string channel, T data)
        {
            return $"{{\"ch\": \"{channel}\", \"data\": {JsonConvert.SerializeObject(data)}}}";
        }

        public string SerializeExpectedAuth<T>(string channel, T data)
        {
            return $"{{\"action\": \"push\", \"ch\": \"{channel}\", \"code\": 200, \"data\": {JsonConvert.SerializeObject(data)}}}";
        }
    }
}
