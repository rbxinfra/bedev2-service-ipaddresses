namespace Roblox.IpAddresses;

using System;
using System.Linq;

using Google.Protobuf.WellKnownTypes;

using EventLog;
using Operations;

using Enums;
using Entities;
using Extensions;
using Ipaddresses.Ipaddresses.V1;

/// <summary>
/// Operation to get <see cref="UserIPAddressV2"/> entities by User ID
/// </summary>
public class GetUserIpAddressesByUserIdOperation : IResultOperation<GetUserIpAddressesByUserIdRequest, GetUserIpAddressesByUserIdResponse>
{
    private readonly ILogger _logger;
    private readonly IIpAddressHelper _ipAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserIpAddressesByUserIdOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IpAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// </exception>
    public GetUserIpAddressesByUserIdOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserIpAddressesByUserIdResponse Output, OperationError Error) Execute(GetUserIpAddressesByUserIdRequest request)
    {
        if (request.UserId == default(long)) return (null, new(IpAddressError.InvalidUserId));

        var ipAddresses = UserIPAddressV2.GetUserIPAddressesV2ByUserPaged(
            request.UserId, 
            request.ExclusiveStartId, 
            request.Count
        );

        return (new GetUserIpAddressesByUserIdResponse {
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
