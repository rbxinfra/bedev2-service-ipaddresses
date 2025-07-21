namespace Roblox.IpAddresses;

using System;

using EventLog;
using Operations;
using Platform.Membership;
using Ipaddresses.Ipaddresses.V1;

using Enums;
using Entities;

/// <summary>
/// Operation to get IP Address count by User ID
/// </summary>
public class GetUserIpAddressCountByUserIdOperation : IResultOperation<GetUserIpAddressCountByUserIdRequest, GetUserIpAddressCountByUserIdResponse>
{
    private readonly ILogger _logger;
    private readonly MembershipDomainFactories _membershipDomainFactories;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserIpAddressCountByUserIdOperation"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    /// <param name="ipAddressHelper">The <see cref="IpAddressHelper"/> to use.</param>
    /// <param name="membershipDomainFactories">The <see cref="MembershipDomainFactories"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// - <paramref name="membershipDomainFactories"/> is null.
    /// </exception>
    public GetUserIpAddressCountByUserIdOperation(
        ILogger logger,
        IIpAddressHelper ipAddressHelper,
        MembershipDomainFactories membershipDomainFactories
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _membershipDomainFactories = membershipDomainFactories ?? throw new ArgumentNullException(nameof(membershipDomainFactories));
    }

    /// <inheritdoc cref="IResultOperation{TRequest, TResponse}.Execute(TRequest)"/>
    public (GetUserIpAddressCountByUserIdResponse Output, OperationError Error) Execute(GetUserIpAddressCountByUserIdRequest request)
    {
        var user = _membershipDomainFactories.UserFactory.GetUser(request.UserId);
        if (user == null) return (null, new(IpAddressError.InvalidUserId));

        return (new GetUserIpAddressCountByUserIdResponse {
            Count = UserIPAddressV2.GetTotalNumberOfUserIPAddressesV2ByUser(user.Id)
        }, null);
    }
}
