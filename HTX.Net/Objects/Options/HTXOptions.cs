using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Options
{
    /// <summary>
    /// HTX options
    /// </summary>
    public class HTXOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public HTXRestOptions Rest { get; set; } = new HTXRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public HTXSocketOptions Socket { get; set; } = new HTXSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `HTXEnvironment` to swap environment, for example `Environment = HTXEnvironment.Live`
        /// </summary>
        public HTXEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IHTXSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
