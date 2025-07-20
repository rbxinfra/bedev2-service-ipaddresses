namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;
using Platform.Membership;

using Enums;
using Entities;

/// <summary>
/// Operation to get MAC Address count by User ID
/// </summary>
public class GetMacAddressCountByUserIdOperation : IResultOperation<V1.GetMacAddressCountByUserIdRequest, V1.GetMacAddressCountByUserIdResponse>
{
    private readonly ILogger _logger;
    private readonly MembershipDomainFactories _membershipDomainFactories;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMacAddressCountByUserIdOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IpAddressHelper"/> to use.</param>
    /// <param name="membershipDomainFactories">The <see cref="MembershipDomainFactories"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// - <paramref name="membershipDomainFactories"/> is null.
    /// </exception>
    public GetMacAddressCountByUserIdOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper,
        MembershipDomainFactories membershipDomainFactories
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _membershipDomainFactories = membershipDomainFactories ?? throw new ArgumentNullException(nameof(membershipDomainFactories));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (V1.GetMacAddressCountByUserIdResponse Output, OperationError Error) Execute(V1.GetMacAddressCountByUserIdRequest request)
    {
        var user = _membershipDomainFactories.UserFactory.GetUser(request.UserId);
        if (user == null) return (null, new(IpAddressError.InvalidUserId));

        return (new V1.GetMacAddressCountByUserIdResponse
        {
            Count = UserMACAddress.GetTotalNumberOfUserMACAddressesByUser(user.Id)
        }, null);
    }
}
