using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;

namespace Huobi.Net.Objects.Sockets
{
    internal class HuobiSocketConverter : SocketConverter
    {
        public override MessageInterpreterPipeline InterpreterPipeline { get; } = new MessageInterpreterPipeline
        {
            PreProcessCallback = DecompressData,
            //PostInspectCallbacks = new List<object>
            //{
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField>
            //        {
            //            new TypeField("id")
            //        },
            //        Callback = GetDeserializationTypeQueryResponse
            //    },
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField>
            //        {
            //            new TypeField("action"),
            //            new TypeField("ch")
            //        },
            //        Callback = GetDeserializationAuthTypeUpdate
            //    },
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField>
            //        {
            //            new TypeField("ch")
            //        },
            //        Callback = GetDeserializationTypeUpdate
            //    },
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField>
            //        {
            //            new TypeField("ping")
            //        },
            //        Callback = GetDeserializationPingUpdate
            //    },
            //    new PostInspectCallback
            //    {
            //        TypeFields = new List<TypeField>
            //        {
            //            new TypeField("action"),
            //            new TypeField("data")
            //        },
            //        Callback = GetDeserializationPingV2Update
            //    }

            GetIdentity = GetIdentity
        };

        private static string GetIdentity(IMessageAccessor accessor)
        {
            var id = accessor.GetStringValue("id");
            if (id != null)
                return id;

            var action = accessor.GetStringValue("action");
            var channel = accessor.GetStringValue("ch");
            if (channel == null)
                return action == null ? "ping" : "pingv2";

            if (action != null && action != "push")
                return action + channel;

            return channel;

        }

        private static PostInspectResult GetDeserializationTypeQueryResponse(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            var id = accessor.GetStringValue("id");
            if (!processors.TryGetValue(id, out var type))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = type, Identifier = id };
        }

        private static Stream DecompressData(WebSocketMessageType type, Stream stream)
        {
            if (type != WebSocketMessageType.Binary)
                return stream;

            var decompressedStream = new MemoryStream();
            using var deflateStream = new GZipStream(stream, CompressionMode.Decompress);
            deflateStream.CopyTo(decompressedStream);
            decompressedStream.Position = 0;
            return decompressedStream;
        }

        private static PostInspectResult GetDeserializationTypeUpdate(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            if (!processors.TryGetValue(accessor.GetStringValue("ch"), out var type))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = type, Identifier = accessor.GetStringValue("ch") };
        }

        private static PostInspectResult GetDeserializationAuthTypeUpdate(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            var action = accessor.GetStringValue("action");
            if (action != "push")
                return new PostInspectResult { Type = typeof(HuobiSocketAuthResponse), Identifier = action + accessor.GetStringValue("ch") };

            if (!processors.TryGetValue(accessor.GetStringValue("ch"), out var type))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = type, Identifier = accessor.GetStringValue("ch") };
        }

        private static PostInspectResult GetDeserializationPingUpdate(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            if (!processors.TryGetValue("ping", out var type))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = type, Identifier = "ping" };
        }

        private static PostInspectResult GetDeserializationPingV2Update(IMessageAccessor accessor, Dictionary<string, Type> processors)
        {
            if (!processors.TryGetValue("pingv2", out var type))
            {
                // Probably shouldn't be exception
                throw new Exception("Unknown update type");
            }

            return new PostInspectResult { Type = type, Identifier = "pingv2" };
        }
    }
}
