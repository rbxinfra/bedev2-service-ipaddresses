namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;
using Extensions;

/// <summary>
/// Operation to get the status of an Ip Address
/// </summary>
public class GetMacAddressStatusOperation : IResultOperation<V1.GetMacAddressStatusRequest, V1.GetMacAddressStatusResponse>
{
    private readonly ILogger _logger;
    private readonly IMacAddressHelper _macAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMacAddressStatusOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="macAddressHelper">The <see cref="MacAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="macAddressHelper"/> is null.
    /// </exception>
    public GetMacAddressStatusOperation(
        ILogger logger,
        IMacAddressHelper macAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _macAddressHelper = macAddressHelper ?? throw new ArgumentNullException(nameof(macAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (V1.GetMacAddressStatusResponse Output, OperationError Error) Execute(V1.GetMacAddressStatusRequest request)
    {
        if (string.IsNullOrEmpty(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));
        if (!_macAddressHelper.IsValidMacAddress(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));

        var macAddress = MACAddress.GetOrCreate(request.MacAddress);

        return (new V1.GetMacAddressStatusResponse
        {
            MacAddressState = macAddress.State.ToGrpc(),
        }, null);
    }
}
