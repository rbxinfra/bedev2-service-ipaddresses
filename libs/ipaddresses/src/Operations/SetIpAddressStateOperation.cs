namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;
using Extensions;
using Ipaddresses.Ipaddresses.V1;
using AddressState = IpAddresses.Enums.AddressState;

/// <summary>
/// Operation to update <see cref="IPAddress"/> entities state
/// </summary>
public class SetIpAddressStateOperation : IResultOperation<SetIpAddressStateRequest, SetIpAddressStateResponse>
{
    private readonly ILogger _logger;
    private readonly IIpAddressHelper _ipAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetIpAddressStateOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IIpAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// </exception>
    public SetIpAddressStateOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (SetIpAddressStateResponse Output, OperationError Error) Execute(SetIpAddressStateRequest request)
    {
        if (string.IsNullOrEmpty(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));
        if (!_ipAddressHelper.IsValidIpAddress(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddressState = request.IpAddressState.FromGrpc();

        if (ipAddressState < AddressState.Allowed ||
            ipAddressState > AddressState.Banned)
            return (new SetIpAddressStateResponse
            {
                Result = SetAddressStateResult.Unknown
            }, new(IpAddressError.UnsupportedIpAddressState));

        var ipAddress = IPAddress.GetOrCreate(request.IpAddress);
        if (ipAddress.State == ipAddressState)
            return (new SetIpAddressStateResponse {
                Result = SetAddressStateResult.Unchanged
            }, null);

        if (ipAddressState == AddressState.Banned)
        {
            ipAddress.State = AddressState.Banned;
            ipAddress.Expiration = DateTime.Now.AddDays(100);
            ipAddress.Save();

            return (new SetIpAddressStateResponse { 
                Result = SetAddressStateResult.BanExtended 
            }, null);
        }

        ipAddress.State = ipAddressState;
        ipAddress.Save();

        return (new SetIpAddressStateResponse { 
            Result = SetAddressStateResult.Changed 
        }, null);
    }
}
