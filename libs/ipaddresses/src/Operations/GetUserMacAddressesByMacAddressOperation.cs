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
/// Operation to get <see cref="UserMACAddress"/> entities by MAC Address
/// </summary>
public class GetUserMacAddressesByMacAddressOperation : IResultOperation<GetUserMacAddressesByMacAddressRequest, GetUserMacAddressesByMacAddressResponse>
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserMacAddressesByMacAddressOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// </exception>
    public GetUserMacAddressesByMacAddressOperation(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserMacAddressesByMacAddressResponse Output, OperationError Error) Execute(GetUserMacAddressesByMacAddressRequest request)
    {
        if (request.MacAddressId == default(long)) return (null, new(IpAddressError.InvalidUserId));

        var macAddresses = UserMACAddress.GetUserMACAddressesByAddressPaged(
            request.MacAddressId,
            request.ExclusiveStartId,
            request.Count
        );

        return (new GetUserMacAddressesByMacAddressResponse
        {
            UserMacAddresses =
            {
                macAddresses.Select(mac => new UserMacAddress
                {
                    UserId = mac.UserID,
                    MacAddress = MACAddress.Get(mac.MACAddressID).ToGrpc(),
                    CreatedTime = DateTime.SpecifyKind(mac.Created, DateTimeKind.Utc).ToTimestamp(),
                })
            }
        }, null);
    }
}
