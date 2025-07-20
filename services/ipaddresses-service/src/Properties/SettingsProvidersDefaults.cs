namespace Roblox.IpAddresses.Service;

/// <summary>
/// Default details for the settings providers.
/// </summary>
internal static class SettingsProvidersDefaults
{
    /// <summary>
    /// The path prefix for the web platform.
    /// </summary>
    public const string ProviderPathPrefix = "services";

    /// <summary>
    /// The path to the game server validation settings.
    /// </summary>
    public const string IpAddressesSettingsPath = $"{ProviderPathPrefix}/ip-addresses-service";
}
