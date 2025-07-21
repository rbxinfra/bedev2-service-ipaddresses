namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;
using Extensions;

/// <summary>
/// Operation to update <see cref="IPAddress"/> entities state
/// </summary>
public class SetIpAddressStateOperation : IResultOperation<V1.SetIpAddressStateRequest, V1.SetIpAddressStateResponse>
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
    public (V1.SetIpAddressStateResponse Output, OperationError Error) Execute(V1.SetIpAddressStateRequest request)
    {
        if (string.IsNullOrEmpty(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));
        if (!_ipAddressHelper.IsValidIpAddress(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddressState = request.IpAddressState.FromGrpc();

        if (ipAddressState < AddressState.Allowed ||
            ipAddressState > AddressState.Banned)
            return (new V1.SetIpAddressStateResponse
            {
                Result = SetAddressStateResult.Unknown.ToGrpc()
            }, new(IpAddressError.UnsupportedIpAddressState));

        var ipAddress = IPAddress.GetOrCreate(request.IpAddress);
        if (ipAddress.State == ipAddressState)
            return (new V1.SetIpAddressStateResponse {
                Result = SetAddressStateResult.Unchanged.ToGrpc()
            }, null);

        if (ipAddressState == AddressState.Banned)
        {
            ipAddress.State = AddressState.Banned;
            ipAddress.Expiration = DateTime.Now.AddDays(100);
            ipAddress.Save();

            return (new V1.SetIpAddressStateResponse { 
                Result = SetAddressStateResult.BanExtended.ToGrpc() 
            }, null);
        }

        ipAddress.State = ipAddressState;
        ipAddress.Save();

        return (new V1.SetIpAddressStateResponse { 
            Result = SetAddressStateResult.Changed.ToGrpc() 
        }, null);
    }
}
