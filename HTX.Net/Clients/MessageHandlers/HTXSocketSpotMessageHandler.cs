using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net;
using System;
using System.Linq;
using System.Text.Json;

namespace HTX.Net.Clients.MessageHandlers
{
    internal class HTXSocketSpotMessageHandler : JsonSocketMessageHandler
    {
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
                    new PropertyFieldReference("action") { Constraint = x => x!.Equals("ping", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "pingV2"
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("ch"),
                    new PropertyFieldReference("action") { Constraint = x => x != null && !x.Equals("push", StringComparison.Ordinal) }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("action")}{x.FieldValue("ch")}"
            },

            new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("ch") { Constraint = x => x!.StartsWith("trade.clearing") || x.StartsWith("orders#") },
                    new PropertyFieldReference("eventType") { Depth = 2 } 
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("ch")}{x.FieldValue("eventType")}"
            },

            new MessageEvaluator {
                Priority = 7,
                Fields = [
                    new PropertyFieldReference("ch"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("ch")!
            },
        ];
    }
}
