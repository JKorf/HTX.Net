using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using HTX.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HTX.Net.Clients.MessageHandlers
{
    internal class HTXRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(HTXExchange._serializerContext);

        public HTXRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is HTXApiResponse htxResponse)
            {
                if (htxResponse.ErrorCode != null && htxResponse.ErrorCode != "0" && htxResponse.ErrorCode != "200")
                    return new ServerError(htxResponse.ErrorCode!, _errorMapping.GetErrorInfo(htxResponse.ErrorCode!, htxResponse.ErrorMessage));
            }
            else if (result is HTXApiResponseV2 htxResponseV2)
            {
                if (htxResponseV2.Code != 0 && htxResponseV2.Code != 200)
                    return new ServerError(htxResponseV2.Code!, _errorMapping.GetErrorInfo(htxResponseV2.Code.ToString(), htxResponseV2.Message));
            }

            return null;
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, object? state, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var (parseError, document) = await GetJsonDocument(responseStream, state).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            var code = document!.RootElement.TryGetProperty("err-code", out var codeProp) ? codeProp.GetString() : null;
            var msg = document!.RootElement.TryGetProperty("err-msg", out var msgProp) ? msgProp.GetString() : null;

            if (code == null || msg == null)
                return new ServerError(ErrorInfo.Unknown);

            return new ServerError(code!, _errorMapping.GetErrorInfo(code, msg));
        }

    }
}
