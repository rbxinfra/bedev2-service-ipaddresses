namespace Roblox.IpAddresses.Service;

using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using EventLog;
using Configuration;
using Instrumentation;
using Platform.Membership;

using Web.Framework.Services;
using Web.Framework.Services.Grpc;

using IpAddressesSettings = Roblox.IpAddresses.Service.Settings;

/// <summary>
/// Startup class for ip-addresses.
/// </summary>
public class Startup : GrpcStartupBase
{
    /// <inheritdoc cref="GrpcStartupBase.Settings"/>
    protected override IGrpcServiceSettings Settings => IpAddressesSettings.Singleton;

    /// <inheritdoc cref="GrpcStartupBase.ConfigureEndpoints(IEndpointRouteBuilder)"/>
    protected override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        base.ConfigureEndpoints(endpoints);

        endpoints.MapGrpcService<IpAddressesAPI>();
    }

    /// <inheritdoc cref="StartupBase.ConfigureLogger(IServiceProvider)"/>
    protected override ILogger ConfigureLogger(IServiceProvider provider)
    {
        var logger = base.ConfigureLogger(provider);

        logger.CustomLogPrefixes.Add(() => RobloxEnvironment.Name);

        return logger;
    }

    /// <inheritdoc cref="StartupBase.ConfigureServices(IServiceCollection)"/>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSingleton<IOperationExecutor, OperationExecutor>();
        services.AddSingleton<IIpAddressesOperations, IpAddressesOperations>();

        services.AddSingleton<IIpAddressHelper, IpAddressHelper>();
        services.AddSingleton<IMacAddressHelper, MacAddressHelper>();
        services.AddSingleton<ICounterRegistry, CounterRegistry>();
        services.AddSingleton<MembershipDomainFactories>();
    }
}
