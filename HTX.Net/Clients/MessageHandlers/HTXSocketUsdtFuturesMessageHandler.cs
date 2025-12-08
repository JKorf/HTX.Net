using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Text.Json;

namespace HTX.Net.Clients.MessageHandlers
{
    internal class HTXSocketUsdtFuturesMessageHandler : JsonSocketMessageHandler
    {
        private static readonly HashSet<string?> _opStandalone = [
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

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("cid"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("cid")!
            },

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("ping"),
                ],
                StaticIdentifier = "pingV3"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("op").WithFilterContstraint(_opStandalone),
                ],
                TypeIdentifierCallback = x => x.FieldValue("op")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("ch"),
                    new PropertyFieldReference("action").WithNotEqualContstraint("push"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("action") + x.FieldValue("ch")
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic")
                        .WithCustomContstraint(x =>
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
                    )
                ],
                TypeIdentifierCallback = x =>
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

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("ch"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("ch")!
            },
        ];
    }
}
