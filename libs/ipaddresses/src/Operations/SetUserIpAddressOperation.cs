namespace Roblox.IpAddresses;

using System;
using System.Linq;

using EventLog;
using Operations;
using Platform.Membership;

using Enums;
using Entities;
using Extensions;

/// <summary>
/// Operation to update <see cref="UserMACAddress"/> entity
/// </summary>
public class SetUserIpAddressOperation : IResultOperation<V1.SetUserIpAddressRequest, V1.SetUserIpAddressResponse>
{
    private readonly ILogger _logger;
    private readonly MembershipDomainFactories _membershipDomainFactories;
    private readonly IIpAddressHelper _ipAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserMacAddressesByUserIdOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="membershipDomainFactories">The <see cref="MembershipDomainFactories"/> to use.</param>
    /// <param name="ipAddressHelper">The <see cref="IIpAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="membershipDomainFactories"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// </exception>
    public SetUserIpAddressOperation(
        ILogger logger, 
        MembershipDomainFactories membershipDomainFactories,
        IIpAddressHelper ipAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _membershipDomainFactories = membershipDomainFactories ?? throw new ArgumentNullException(nameof(membershipDomainFactories));
        _ipAddressHelper = ipAddressHelper ?? throw new ArgumentNullException(nameof(ipAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (V1.SetUserIpAddressResponse Output, OperationError Error) Execute(V1.SetUserIpAddressRequest request)
    {
        var user = _membershipDomainFactories.UserFactory.GetUser(request.UserId);
        if (user == null) return (null, new(IpAddressError.InvalidUserId));

        if (string.IsNullOrEmpty(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));
        if (!_ipAddressHelper.IsValidIpAddress(request.IpAddress)) return (null, new(IpAddressError.InvalidIpAddress));

        var ipAddresses = UserIPAddressV2.GetUserIPAddressesV2ByUserPaged(request.UserId, 0, 100);
        if (ipAddresses.Any(x => IPAddress.Get(x.IPAddressID).Address == request.IpAddress))
        {
            return (new V1.SetUserIpAddressResponse
            {
                Result = SetUserIpAddressResult.AlreadyAssociated.ToGrpc()
            }, null);
        }

        var ipAddress = IPAddress.GetOrCreate(request.IpAddress);
        var userIpAddress = new UserIPAddressV2();
        userIpAddress.IPAddressID = ipAddress.ID;
        userIpAddress.UserID = request.UserId;
        userIpAddress.Save();

        return (new V1.SetUserIpAddressResponse 
        { 
            Result = SetUserIpAddressResult.Associated.ToGrpc() 
        }, null);
    }
}
