using HTX.Net.Interfaces.Clients;
using HTX.Net.Objects.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace HTX.Net.Clients
{
    /// <inheritdoc />
    public class HTXUserClientProvider : IHTXUserClientProvider
    {
        private static ConcurrentDictionary<string, IHTXRestClient> _restClients = new ConcurrentDictionary<string, IHTXRestClient>();
        private static ConcurrentDictionary<string, IHTXSocketClient> _socketClients = new ConcurrentDictionary<string, IHTXSocketClient>();

        private readonly IOptions<HTXRestOptions> _restOptions;
        private readonly IOptions<HTXSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => HTXExchange.ExchangeName;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, HTXEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IHTXRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, HTXEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IHTXSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, HTXEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client) || client.Disposed)
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IHTXRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, HTXEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new HTXRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IHTXSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, HTXEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new HTXSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<HTXRestOptions> SetRestEnvironment(HTXEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new HTXRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<HTXSocketOptions> SetSocketEnvironment(HTXEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new HTXSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
