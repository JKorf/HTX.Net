using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;

namespace HTX.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IHTXTrackerFactory : ITrackerFactory
    {
        /// <summary>
        /// Create a new Spot user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, HTXEnvironment? environment = null);
        /// <summary>
        /// Create a new spot user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null);

        /// <summary>
        /// Create a new futures user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="environment">Environment</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, SharedMarginMode marginMode, FuturesUserDataTrackerConfig? config = null, HTXEnvironment? environment = null);
        /// <summary>
        /// Create a new futures user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        /// <param name="marginMode">Margin mode</param>
        IUserFuturesDataTracker CreateUserFuturesDataTracker(SharedMarginMode marginMode, FuturesUserDataTrackerConfig? config = null);
    }
}
