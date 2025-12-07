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
                    new PropertyFieldReference("action").WithEqualContstraint("ping"),
                ],
                StaticIdentifier = "pingV2"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("ch"),
                    new PropertyFieldReference("action").WithNotEqualContstraint("push")
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("action")}{x.FieldValue("ch")}"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("ch").WithStartsWithContstraints("trade.clearing", "orders#"),
                    new PropertyFieldReference("eventType") { Depth = 2 } 
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("ch")}{x.FieldValue("eventType")}"
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
