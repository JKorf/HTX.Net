using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Interfaces;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Objects.Options;
using HTX.Net.SymbolOrderBooks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IHTXRestClient and IHTXSocketClient. Configures the services based on the provided configuration.
        /// See <see href="https://github.com/JKorf/HTX.Net/blob/master/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddHTX(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = HTXOptions.CreateFromConfiguration(configuration);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddHTXCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IHTXRestClient and IHTXSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the HTX services</param>
        /// <returns></returns>
        public static IServiceCollection AddHTX(
            this IServiceCollection services,
            Action<HTXOptions>? optionsDelegate = null)
        {
            var options = HTXOptions.Create(optionsDelegate);
            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddHTXCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddHTXCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IHTXRestClient, HTXRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<HTXRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new HTXRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<HTXRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<HTXRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IHTXSocketClient), x => { return new HTXSocketClient(x.GetRequiredService<IOptions<HTXSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IHTXOrderBookFactory, HTXOrderBookFactory>();
            services.AddTransient<IHTXTrackerFactory, HTXTrackerFactory>();
            services.AddTransient<ITrackerFactory, HTXTrackerFactory>();
            services.AddSingleton<IHTXUserClientProvider, HTXUserClientProvider>(x =>
            new HTXUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IHTXRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<HTXRestOptions>>(),
                x.GetRequiredService<IOptions<HTXSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IHTXRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IHTXSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IHTXRestClient>().UsdtFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IHTXSocketClient>().UsdtFuturesApi.SharedClient);

            services.RegisterSharedApiClient<
                IHTXSharedApiClient,
                HTXSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.UsdtFuturesRest)
                    .Add(client => client.UsdtFuturesSocket)
                    );

            return services;
        }
    }
}
