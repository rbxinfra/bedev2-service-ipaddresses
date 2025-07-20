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
public class GetIpAddressStatusOperation : IResultOperation<V1.GetIpAddressStatusRequest, V1.GetIpAddressStatusResponse>
{
    private readonly ILogger _logger;
    private readonly IIpAddressHelper _ipAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserIdCountByIpAddressOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IpAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// </exception>
    public GetIpAddressStatusOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (V1.GetIpAddressStatusResponse Output, OperationError Error) Execute(V1.GetIpAddressStatusRequest request)
    {
        if (string.IsNullOrEmpty(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));
        if (!_ipAddressHelper.IsValidIpAddress(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddress = IPAddress.GetOrCreate(request.IpAddress);

        return (new V1.GetIpAddressStatusResponse {
            IpAddress = ipAddress.Address,
            IpAddressType = _ipAddressHelper.GetAddressType(ipAddress.Address).ToGrpc(),
            IpAddressState = ipAddress.State.ToGrpc()
        }, null);
    }
}
