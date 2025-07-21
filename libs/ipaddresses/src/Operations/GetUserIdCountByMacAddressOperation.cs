namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;
using Ipaddresses.Ipaddresses.V1;

/// <summary>
/// Operation to get User ID count by MAC Address
/// </summary>
public class GetUserIdCountByMacAddressOperation : IResultOperation<GetUserIdCountByMacAddressRequest, GetUserIdCountByMacAddressResponse>
{
    private readonly ILogger _logger;
    private readonly IMacAddressHelper _macAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserIdCountByIpAddressOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="macAddressHelper">The <see cref="MacAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="macAddressHelper"/> is null.
    /// </exception>
    public GetUserIdCountByMacAddressOperation(
        ILogger logger,
        IMacAddressHelper macAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _macAddressHelper = macAddressHelper ?? throw new ArgumentNullException(nameof(macAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserIdCountByMacAddressResponse Output, OperationError Error) Execute(GetUserIdCountByMacAddressRequest request)
    {
        if (string.IsNullOrEmpty(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));
        if (!_macAddressHelper.IsValidMacAddress(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));

        var macAddress = MACAddress.GetOrCreate(request.MacAddress);

        return (new GetUserIdCountByMacAddressResponse {
            Count = UserMACAddress.GetTotalNumberOfUserMACAddressesByAddress(macAddress.ID)
        }, null);
    }
}
