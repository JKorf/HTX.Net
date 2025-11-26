using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net;
using System;
using System.Linq;
using System.Text.Json;

namespace HTX.Net.Clients.MessageHandlers
{
    internal class HTXSocketUsdtFuturesMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string> _opStandalone = [
            "ping",
            "close",
            "auth"
            ];

        private static readonly Dictionary<string, string> _topicEndsWithReplacements = new()
        {
            { ".liquidation_orders", "public.*.liquidation_orders" },
            { ".funding_rate", "public.*.funding_rate" },
        };

        private static readonly Dictionary<string, string> _topicStartsWithReplacements = new()
        {
            { "accounts.", "accounts" },
            { "orders.", "orders" },
            { "positions.", "positions" },
            { "accounts_cross.", "accounts_cross" },
            { "orders_cross.", "orders_cross" },
            { "positions_cross.", "positions_cross" },
        };

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(HTXExchange._serializerContext);

        protected override MessageEvaluator[] MessageEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

            new MessageEvaluator {
                Priority = 2,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("cid"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("cid")!
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("ping"),
                ],
                StaticIdentifier = "pingV3"
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("op") { Constraint = x => _opStandalone.Contains(x!) },
                ],
                IdentifyMessageCallback = x => x.FieldValue("op")!
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("ch"),
                    new PropertyFieldReference("action") { Constraint = x => x != null && !x.Equals("push", StringComparison.Ordinal) },
                ],
                IdentifyMessageCallback = x => x.FieldValue("action") + x.FieldValue("ch")
            },

            new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("topic") 
                    { 
                        Constraint = x =>
                        {
                            foreach(var item in _topicEndsWithReplacements){
                                if (x!.EndsWith(item.Key))
                                    return true;
                            }

                            foreach(var item in _topicStartsWithReplacements){
                                if (x!.StartsWith(item.Key))
                                    return true;
                            }

                            return false;
                        }
                    },
                ],
                IdentifyMessageCallback = x =>
                {
                    var value = x.FieldValue("topic");
                    foreach(var item in _topicEndsWithReplacements){
                        if (value!.EndsWith(item.Key))
                            return item.Value;
                    }

                    foreach(var item in _topicStartsWithReplacements){
                        if (value!.StartsWith(item.Key))
                            return item.Value;
                    }

                    return value!;
                }
            },

            new MessageEvaluator {
                Priority = 7,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!
            },

            new MessageEvaluator {
                Priority = 8,
                Fields = [
                    new PropertyFieldReference("ch"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("ch")!
            },
        ];
    }
}
