namespace Roblox.IpAddresses;

using System;
using System.Linq;

using Google.Protobuf.WellKnownTypes;

using EventLog;
using Operations;
using Ipaddresses.Ipaddresses.V1;

using Enums;
using Entities;
using Extensions;

/// <summary>
/// Operation to get <see cref="UserIPAddressV2"/> entities by <see cref="IPAddress.Address"/>
/// </summary>
public class GetUserIpAddressesByIpAddressOperation : IResultOperation<GetUserIpAddressesByIpAddressRequest, GetUserIpAddressesByIpAddressResponse>
{
    private readonly ILogger _logger;
    private readonly IIpAddressHelper _ipAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserIpAddressesByIpAddressOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IpAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// </exception>
    public GetUserIpAddressesByIpAddressOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserIpAddressesByIpAddressResponse Output, OperationError Error) Execute(GetUserIpAddressesByIpAddressRequest request)
    {
        if (request.IpAddressId == default(long)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddresses = UserIPAddressV2.GetUserIPAddressesV2ByAddressPaged(
            request.IpAddressId,
            request.ExclusiveStartId,
            request.Count
        );

        return (new GetUserIpAddressesByIpAddressResponse {
            UserIpAddresses =
            {
                ipAddresses.Select(ip => new UserIpAddressV2 {
                    UserId = ip.UserID,
                    IpAddress = IPAddress.Get(ip.IPAddressID).ToGrpc(),
                    LastSeenTime = ip.LastSeen.HasValue
                        ? DateTime.SpecifyKind(ip.LastSeen.Value, DateTimeKind.Utc).ToTimestamp()
                        : null,
                    CreatedTime = DateTime.SpecifyKind(ip.Created, DateTimeKind.Utc).ToTimestamp(),
                })
            }
        }, null);
    }
}
