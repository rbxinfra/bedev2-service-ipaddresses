namespace Roblox.IpAddresses.Service;

using EventLog;
using Configuration;

using Web.Framework.Services;
using Web.Framework.Services.Grpc;

using static SettingsProvidersDefaults;

internal class Settings : BaseSettingsProvider<Settings>, IGrpcServiceSettings
{
    /// <inheritdoc cref="IVaultProvider.Path"/>
    protected override string ChildPath => IpAddressesSettingsPath;

    /// <inheritdoc cref="IServiceSettings.ApiKey"/>
    public string ApiKey => GetOrDefault(nameof(ApiKey), string.Empty);

    /// <inheritdoc cref="IServiceSettings.LogLevel"/>
    public LogLevel LogLevel => GetOrDefault(nameof(LogLevel), LogLevel.Information);

    /// <inheritdoc cref="IServiceSettings.VerboseErrorsEnabled"/>
    public bool VerboseErrorsEnabled => GetOrDefault(nameof(VerboseErrorsEnabled), false);

    /// <inheritdoc cref="IGrpcServiceSettings.MetricsPort"/>
    public int MetricsPort => GetOrDefault(nameof(MetricsPort), 8888);
}
