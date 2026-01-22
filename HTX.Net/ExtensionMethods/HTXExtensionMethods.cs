using System.Web;

namespace HTX.Net.ExtensionMethods
{
    /// <summary>
    /// Extension methods specific to using the HTX API
    /// </summary>
    public static class HTXExtensionMethods
    {
        /// <summary>
        /// Create a new uri with the provided parameters as query
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="baseUri"></param>
        /// <param name="arraySerialization"></param>
        /// <returns></returns>
        public static Uri SetParameters(this Uri baseUri, IOrderedEnumerable<KeyValuePair<string, object>> parameters, ArrayParametersSerialization arraySerialization)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = baseUri.Scheme;
            uriBuilder.Host = baseUri.Host;
            uriBuilder.Port = baseUri.Port;
            uriBuilder.Path = baseUri.AbsolutePath;
            var httpValueCollection = HttpUtility.ParseQueryString(string.Empty);
            foreach (var parameter in parameters)
            {
                if (parameter.Value.GetType().IsArray)
                {
                    if (arraySerialization == ArrayParametersSerialization.JsonArray)
                    {
                        httpValueCollection.Add(parameter.Key, $"[{string.Join(",", (object[])parameter.Value)}]");
                    }
                    else
                    {
                        foreach (var item in (object[])parameter.Value)
                        {
                            if (arraySerialization == ArrayParametersSerialization.Array)
                            {
                                httpValueCollection.Add(parameter.Key + "[]", item.ToString());
                            }
                            else
                            {
                                httpValueCollection.Add(parameter.Key, item.ToString());
                            }
                        }
                    }
                }
                else
                {
                    httpValueCollection.Add(parameter.Key, parameter.Value.ToString());
                }
            }

            uriBuilder.Query = httpValueCollection.ToString();
            return uriBuilder.Uri;
        }
    }
}
