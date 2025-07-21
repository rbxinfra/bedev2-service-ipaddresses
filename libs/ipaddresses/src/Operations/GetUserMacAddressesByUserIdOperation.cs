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
/// Operation to get <see cref="UserMACAddress"/> entities by User ID
/// </summary>
public class GetUserMacAddressesByUserIdOperation : IResultOperation<GetUserMacAddressesByUserIdRequest, GetUserMacAddressesByUserIdResponse>
{
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserMacAddressesByUserIdOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// </exception>
    public GetUserMacAddressesByUserIdOperation(ILogger logger) 
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserMacAddressesByUserIdResponse Output, OperationError Error) Execute(GetUserMacAddressesByUserIdRequest request)
    {
        if (request.UserId == default(long)) return (null, new(IpAddressError.InvalidUserId));

        var macAddresses = UserMACAddress.GetUserMACAddressesByUserPaged(
            request.UserId,
            request.ExclusiveStartId,
            request.Count
        );

        return (new GetUserMacAddressesByUserIdResponse {
            UserMacAddresses = {
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
