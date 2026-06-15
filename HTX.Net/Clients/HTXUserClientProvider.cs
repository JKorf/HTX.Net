using CryptoExchange.Net.Clients;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace HTX.Net.Clients
{
    /// <inheritdoc />
    public class HTXUserClientProvider : UserClientProvider<
        IHTXRestClient,
        IHTXSocketClient,
        HTXRestOptions,
        HTXSocketOptions,
        HTXCredentials,
        HTXEnvironment
        >, IHTXUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => HTXExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public HTXUserClientProvider(Action<HTXOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public HTXUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<HTXRestOptions> restOptions,
            IOptions<HTXSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IHTXRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<HTXRestOptions> options)
            => new HTXRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IHTXSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<HTXSocketOptions> options)
            => new HTXSocketClient(options, loggerFactory);
    }
}
