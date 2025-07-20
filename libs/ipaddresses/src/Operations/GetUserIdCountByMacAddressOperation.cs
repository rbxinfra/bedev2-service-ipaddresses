namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;

/// <summary>
/// Operation to get User ID count by MAC Address
/// </summary>
public class GetUserIdCountByMacAddressOperation : IResultOperation<V1.GetUserIdCountByMacAddressRequest, V1.GetUserIdCountByMacAddressResponse>
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
    public (V1.GetUserIdCountByMacAddressResponse Output, OperationError Error) Execute(V1.GetUserIdCountByMacAddressRequest request)
    {
        if (string.IsNullOrEmpty(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));
        if (!_macAddressHelper.IsValidMacAddress(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));

        var macAddress = MACAddress.GetOrCreate(request.MacAddress);

        return (new V1.GetUserIdCountByMacAddressResponse
        {
            Count = UserMACAddress.GetTotalNumberOfUserMACAddressesByAddress(macAddress.ID)
        }, null);
    }
}
