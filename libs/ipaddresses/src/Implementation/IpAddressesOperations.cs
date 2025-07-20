namespace Roblox.IpAddresses;

using System;

using EventLog;
using Platform.Membership;

/// <summary>
/// Implementation of <see cref="IIpAddressesOperations"/>.
/// </summary>
/// <seealso cref="IIpAddressesOperations"/>
public class IpAddressesOperations : IIpAddressesOperations
{
    /// <inheritdoc cref="IIpAddressesOperations.GetUserIdCountByIpAddress"/>
    public GetUserIdCountByIpAddressOperation GetUserIdCountByIpAddress { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetUserIdCountByMacAddress"/>
    public GetUserIdCountByMacAddressOperation GetUserIdCountByMacAddress { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetUserIpAddressesByUserId"/>
    public GetUserIpAddressesByUserIdOperation GetUserIpAddressesByUserId { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetUserMacAddressesByUserId"/>
    public GetUserMacAddressesByUserIdOperation GetUserMacAddressesByUserId { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetUserMacAddressesByMacAddress"/>
    public GetUserMacAddressesByMacAddressOperation GetUserMacAddressesByMacAddress { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetUserIpAddressCountByUserId"/>
    public GetUserIpAddressCountByUserIdOperation GetUserIpAddressCountByUserId { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetIpAddressStatus"/>
    public GetIpAddressStatusOperation GetIpAddressStatus { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetMacAddressCountByUserId"/>
    public GetMacAddressCountByUserIdOperation GetMacAddressCountByUserId { get; }

    /// <inheritdoc cref="IIpAddressesOperations.GetMacAddressStatus"/>
    public GetMacAddressStatusOperation GetMacAddressStatus { get; }

    /// <inheritdoc cref="IIpAddressesOperations.SetUserIpAddress"/>
    public SetUserIpAddressOperation SetUserIpAddress { get; }

    /// <inheritdoc cref="IIpAddressesOperations.SetUserMacAddress"/>
    public SetUserMacAddressOperation SetUserMacAddress { get; }

    /// <inheritdoc cref="IIpAddressesOperations.SetMacAddressState"/>
    public SetMacAddressStateOperation SetMacAddressState { get; }

    /// <inheritdoc cref="IIpAddressesOperations.SetIpAddressState"/>
    public SetIpAddressStateOperation SetIpAddressState { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="IpAddressesOperations"/>.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use.</param>
    /// <param name="ipAddressHelper">The <see cref="IIpAddressHelper"/> to use.</param>
    /// <param name="macAddressHelper">The <see cref="IMacAddressHelper"/> to use.</param>
    /// <param name="membershipDomainFactories">The <see cref="MembershipDomainFactories"/> to use.</param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> is null.
    /// - <paramref name="ipAddressHelper"/> is null.
    /// - <paramref name="macAddressHelper"/> is null.
    /// - <paramref name="membershipDomainFactories"/> is null.
    /// </exception>
    public IpAddressesOperations(
        ILogger logger, 
        IIpAddressHelper ipAddressHelper,
        IMacAddressHelper macAddressHelper,
        MembershipDomainFactories membershipDomainFactories
    ) {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(ipAddressHelper, nameof(ipAddressHelper));
        ArgumentNullException.ThrowIfNull(macAddressHelper, nameof(macAddressHelper));
        ArgumentNullException.ThrowIfNull(membershipDomainFactories, nameof(membershipDomainFactories));

        GetUserIdCountByIpAddress = new(logger, ipAddressHelper);
        GetUserIdCountByMacAddress = new(logger, macAddressHelper);
        GetUserIpAddressesByUserId = new(logger, ipAddressHelper);
        GetUserMacAddressesByUserId = new(logger);
        GetUserMacAddressesByMacAddress = new(logger);
        GetUserIpAddressCountByUserId = new(logger, ipAddressHelper, membershipDomainFactories);
        GetIpAddressStatus = new(logger, ipAddressHelper);
        GetMacAddressCountByUserId = new(logger, ipAddressHelper, membershipDomainFactories);
        GetMacAddressStatus = new(logger, macAddressHelper);
        SetUserIpAddress = new(logger, membershipDomainFactories, ipAddressHelper);
        SetUserMacAddress = new(logger, membershipDomainFactories, macAddressHelper);
        SetMacAddressState = new(logger, macAddressHelper);
        SetIpAddressState = new(logger, ipAddressHelper);
    }
}
