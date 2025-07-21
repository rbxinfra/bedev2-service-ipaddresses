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
/// Operation to update <see cref="MACAddress"/> entities state
/// </summary>
public class SetMacAddressStateOperation : IResultOperation<SetMacAddressStateRequest, SetMacAddressStateResponse>
{
    private readonly ILogger _logger;
    private readonly IMacAddressHelper _macAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetMacAddressStateOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="macAddressHelper">The <see cref="IMacAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="macAddressHelper"/> is null.
    /// </exception>
    public SetMacAddressStateOperation(
        ILogger logger,
        IMacAddressHelper macAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _macAddressHelper = macAddressHelper ?? throw new ArgumentNullException(nameof(macAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (SetMacAddressStateResponse Output, OperationError Error) Execute(SetMacAddressStateRequest request)
    {
        if (string.IsNullOrEmpty(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));
        if (!_macAddressHelper.IsValidMacAddress(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));

        var macAddressState = request.MacAddressState.FromGrpc();

        if (macAddressState < AddressState.Allowed ||
            macAddressState > AddressState.Banned)
            return (new SetMacAddressStateResponse {
                Result = SetAddressStateResult.Unknown
            }, new(IpAddressError.UnsupportedMacAddressState));

        var macAddress = MACAddress.GetOrCreate(request.MacAddress);
        if (macAddress.State != macAddressState)
        {
            macAddress.State = macAddressState;
            if (macAddressState == AddressState.Banned)
            {
                macAddress.Expiration = DateTime.Now.AddDays(100);
                return (new SetMacAddressStateResponse {
                    Result = SetAddressStateResult.BanExtended
                }, null);
            }

            return (new SetMacAddressStateResponse {
                Result = SetAddressStateResult.Changed
            }, null);
        }

        return (new SetMacAddressStateResponse {
            Result = SetAddressStateResult.Unchanged
        }, null);
    }
}
