namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;

using Enums;
using Entities;

/// <summary>
/// Operation to get User ID count by IP Address
/// </summary>
public class GetUserIdCountByIpAddressOperation : IResultOperation<V1.GetUserIdCountByIpAddressRequest, V1.GetUserIdCountByIpAddressResponse>
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
    public GetUserIdCountByIpAddressOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (V1.GetUserIdCountByIpAddressResponse Output, OperationError Error) Execute(V1.GetUserIdCountByIpAddressRequest request)
    {
        if (string.IsNullOrEmpty(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));
        if (!_ipAddressHelper.IsValidIpAddress(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddress = IPAddress.GetOrCreate(request.IpAddress);

        return (new V1.GetUserIdCountByIpAddressResponse
        {
            Count = UserIPAddressV2.GetTotalNumberOfUserIPAddressesV2ByAddress(ipAddress.ID)
        }, null);
    }
}
