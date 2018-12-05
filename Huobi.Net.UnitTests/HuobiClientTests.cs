using Huobi.Net.Objects;
using Huobi.Net.UnitTests.TestImplementations;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class HuobiClientTests
    {
        [TestCase]
        public void GetMarketTickers_Should_RespondWithMarketTickers()
        {
            // arrange
            var expected = new List<HuobiMarketTick>
            {
                new HuobiMarketTick
                {
                    Close = 0.1m,
                    Id = 12345,
                    Low = 0.2m,
                    Symbol = "BTCETH",
                    Amount = 0.3m,
                    Open = 0.4m,
                    High = 0.5m,
                    Volume = 0.6m,
                    TradeCount = 123
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketTickers();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.Data[0]));
        }

        [TestCase]
        public void GetMarketTickerMerged_Should_RespondWithMergedTick()
        {
            // arrange
            var expected = new HuobiMarketTickMerged
            {
                Close = 0.1m,
                Id = 12345,
                Low = 0.2m,
                Amount = 0.3m,
                Open = 0.4m,
                High = 0.5m,
                Volume = 0.6m,
                TradeCount = 123,
                BestAsk = new HuobiOrderBookEntry(){ Amount = 0.7m, Price = 0.8m},
                BestBid = new HuobiOrderBookEntry(){ Amount = 0.9m, Price = 1.0m },
                Version = 1
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketTickerMerged("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data.Data));
        }

        [TestCase]
        public void GetMarketKlines_Should_RespondWithKlines()
        {
            // arrange
            var expected = new List<HuobiMarketData>
            {
                new HuobiMarketData
                {
                    Close = 0.1m,
                    Id = 12345,
                    Low = 0.2m,
                    Amount = 0.3m,
                    Open = 0.4m,
                    High = 0.5m,
                    Volume = 0.6m,
                    TradeCount = 123
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketKlines("BTCETH", HuobiPeriod.FiveMinutes, 10);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.Data[0]));
        }

        [TestCase]
        public void GetMarketDepth_Should_RespondWithDepth()
        {
            // arrange
            var expected = new HuobiMarketDepth()
            { 
                Asks = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry(){ Amount = 0.1m, Price = 0.2m}
                },
                Bids = new List<HuobiOrderBookEntry>()
                {
                    new HuobiOrderBookEntry(){ Amount = 0.3m, Price = 0.4m}
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketDepth("BTCETH", 1);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Asks[0], result.Data.Data.Asks[0]));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Bids[0], result.Data.Data.Bids[0]));
        }

        [TestCase]
        public void GetMarketLastTrade_Should_RespondWithLastTrade()
        {
            // arrange
            var expected = new HuobiMarketTrade()
            {
                Id = 123,
                Timestamp = new DateTime(2018, 1, 1),
                Details = new List<HuobiMarketTradeDetails>()
                {
                    new HuobiMarketTradeDetails()
                    {
                        Amount = 0.1m,
                        Id = "12334232",
                        Price = 0.2m,
                        Timestamp = new DateTime(2018, 1, 1),
                        Side = HuobiOrderSide.Buy
                    }
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketLastTrade("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data.Data, "Details"));
            Assert.IsTrue(TestHelpers.AreEqual(expected.Details[0], result.Data.Data.Details[0]));
        }

        [TestCase]
        public void GetMarketTradeHistory_Should_RespondWithTradeHistory()
        {
            // arrange
            var expected = new List<HuobiMarketTrade>
            {
                new HuobiMarketTrade()
                {
                    Id = 123,
                    Timestamp = new DateTime(2018, 1, 1),
                    Details = new List<HuobiMarketTradeDetails>()
                    {
                        new HuobiMarketTradeDetails()
                        {
                            Amount = 0.1m,
                            Id = "12334232",
                            Price = 0.2m,
                            Timestamp = new DateTime(2018, 1, 1),
                            Side = HuobiOrderSide.Buy
                        }
                    }
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketTradeHistory("BTCETH", 1);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data.Data[0], "Details"));
            Assert.IsTrue(TestHelpers.AreEqual(expected[0].Details[0], result.Data.Data[0].Details[0]));
        }

        [TestCase]
        public void GetMarketDetails24H_Should_RespondWithDetails24H()
        {
            // arrange
            var expected = new HuobiMarketData
            {
                Close = 0.1m,
                Id = 12345,
                Low = 0.2m,
                Amount = 0.3m,
                Open = 0.4m,
                High = 0.5m,
                Volume = 0.6m,
                TradeCount = 123
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetMarketDetails24H("BTCETH");

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected, result.Data.Data, "Details"));
        }

        [TestCase]
        public void GetSymbols_Should_RespondWithSymbolList()
        {
            // arrange
            var expected = new List<HuobiSymbol>
            {
                new HuobiSymbol()
                {
                    Symbol = "BTCETH",
                    AmountPrecision = 8,
                    BaseCurrency = "BTC",
                    PricePrecision = 8,
                    QuoteCurrency = "ETH",
                    SymbolPartition = "INOVATION"
                }
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetSymbols();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data[0]));
        }

        [TestCase]
        public void GetCurrencies_Should_RespondWithCurrenciesList()
        {
            // arrange
            var expected = new List<string>
            {
                "BTCETH",
                "BTCUSD"
            };

            var client = TestHelpers.CreateResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetCurrencies();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(expected[0] == result.Data[0]);
            Assert.IsTrue(expected[1] == result.Data[1]);
        }

        [TestCase]
        public void GetAccounts_Should_RespondWithAccountsList()
        {
            // arrange
            var expected = new List<HuobiAccount>
            {
                new HuobiAccount()
                {
                    Id = 123,
                    Type = HuobiAccountType.Margin,
                    State = HuobiAccountState.Working
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetAccounts();

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected[0], result.Data[0]));
        }

        [TestCase]
        public void GetAccountBalances_Should_RespondWithAccountBalanceList()
        {
            // arrange
            var expected = new HuobiAccountBalances()
            {
                Data = new List<HuobiBalance>
                {
                    new HuobiBalance()
                    {
                        Type = HuobiBalanceType.Frozen,
                        Balance = 0.1m,
                        Currency = "ETH"
                    }
                }
            };

            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(expected, true));

            // act
            var result = client.GetBalances(123);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(TestHelpers.AreEqual(expected.Data[0], result.Data[0]));
        }

        [TestCase]
        public void PlaceOrder_Should_RespondWithPlacedOrderId()
        {
            // arrange
            var client = TestHelpers.CreateAuthResponseClient(SerializeExpected(123, true));

            // act
            var result = client.PlaceOrder(123, "BTCETH", HuobiOrderType.LimitBuy, 1, 2);

            // assert
            Assert.AreEqual(true, result.Success);
            Assert.IsTrue(123 == result.Data);
        }

        public string SerializeExpected<T>(T data, bool tick)
        {
            return $"{{\"status:\": \"ok\", {(tick ? "tick": "data")}: {JsonConvert.SerializeObject(data)}}}";
        }
    }
}
