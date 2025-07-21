namespace Roblox.IpAddresses;

using System;
using System.Linq;

using EventLog;
using Operations;
using Platform.Membership;
using Ipaddresses.Ipaddresses.V1;

using Enums;
using Entities;

/// <summary>
/// Operation to update <see cref="UserMACAddress"/> entity
/// </summary>
public class SetUserMacAddressOperation : IResultOperation<SetUserMacAddressRequest, SetUserMacAddressResponse>
{
    private readonly ILogger _logger;
    private readonly MembershipDomainFactories _membershipDomainFactories;
    private readonly IMacAddressHelper _macAddressHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetUserMacAddressOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="membershipDomainFactories">The <see cref="MembershipDomainFactories"/> to use.</param>
    /// <param name="macAddressHelper">The <see cref="IMacAddressHelper"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="membershipDomainFactories"/> is null.
    /// - <paramref name="macAddressHelper"/> is null.
    /// </exception>
    public SetUserMacAddressOperation(
        ILogger logger, 
        MembershipDomainFactories membershipDomainFactories,
        IMacAddressHelper macAddressHelper
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _membershipDomainFactories = membershipDomainFactories ?? throw new ArgumentNullException(nameof(membershipDomainFactories));
        _macAddressHelper = macAddressHelper ?? throw new ArgumentNullException(nameof(macAddressHelper));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (SetUserMacAddressResponse Output, OperationError Error) Execute(SetUserMacAddressRequest request)
    {
        var user = _membershipDomainFactories.UserFactory.GetUser(request.UserId);
        if (user == null) return (null, new(IpAddressError.InvalidUserId));

        if (string.IsNullOrEmpty(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));
        if (!_macAddressHelper.IsValidMacAddress(request.MacAddress)) return (null, new(IpAddressError.InvalidMacAddress));

        var macAddresses = UserMACAddress.GetUserMACAddressesByUserPaged(request.UserId, 0, 100);
        if (macAddresses.Any(x => MACAddress.Get(x.MACAddressID).Address == request.MacAddress))
        {
            return (new SetUserMacAddressResponse {
                Result = SetUserMacAddressResult.AlreadyAssociated
            }, null);
        }

        var macAddress = MACAddress.GetOrCreate(request.MacAddress);
        var userMACAddress = new UserMACAddress();
        userMACAddress.MACAddressID = macAddress.ID;
        userMACAddress.UserID = request.UserId;
        userMACAddress.Save();

        return (new SetUserMacAddressResponse { 
            Result = SetUserMacAddressResult.Associated 
        }, null);
    }
}
