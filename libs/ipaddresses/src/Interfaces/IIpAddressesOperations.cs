namespace Roblox.IpAddresses;

/// <summary>
/// Operations for ip-addresses.
/// </summary>
public interface IIpAddressesOperations
{
    /// <summary>
    /// Gets the <see cref="GetUserIdCountByIpAddressOperation"/>.
    /// </summary>
    GetUserIdCountByIpAddressOperation GetUserIdCountByIpAddress { get; }

    /// <summary>
    /// Gets the <see cref="GetUserIdCountByMacAddressOperation"/>.
    /// </summary>
    GetUserIdCountByMacAddressOperation GetUserIdCountByMacAddress { get; }

    /// <summary>
    /// Gets the <see cref="GetUserIpAddressesByUserIdOperation"/>.
    /// </summary>
    GetUserIpAddressesByUserIdOperation GetUserIpAddressesByUserId { get; }

    /// <summary>
    /// Gets the <see cref="GetUserMacAddressesByUserIdOperation"/>.
    /// </summary>
    GetUserMacAddressesByUserIdOperation GetUserMacAddressesByUserId { get; }

    /// <summary>
    /// Gets the <see cref="GetUserMacAddressesByMacAddressOperation"/>.
    /// </summary>
    GetUserMacAddressesByMacAddressOperation GetUserMacAddressesByMacAddress { get; }

    /// <summary>
    /// Gets the <see cref="GetUserIpAddressCountByUserIdOperation"/>.
    /// </summary>
    GetUserIpAddressCountByUserIdOperation GetUserIpAddressCountByUserId { get; }

    /// <summary>
    /// Gets the <see cref="GetIpAddressStatusOperation"/>.
    /// </summary>
    GetIpAddressStatusOperation GetIpAddressStatus { get; }

    /// <summary>
    /// Gets the <see cref="GetMacAddressCountByUserIdOperation"/>.
    /// </summary>
    GetMacAddressCountByUserIdOperation GetMacAddressCountByUserId { get; }

    /// <summary>
    /// Gets the <see cref="GetMacAddressStatusOperation"/>.
    /// </summary>
    GetMacAddressStatusOperation GetMacAddressStatus { get; }

    /// <summary>
    /// Gets the <see cref="SetUserIpAddressOperation"/>.
    /// </summary>
    SetUserIpAddressOperation SetUserIpAddress { get; }

    /// <summary>
    /// Gets the <see cref="SetUserMacAddressOperation"/>.
    /// </summary>
    SetUserMacAddressOperation SetUserMacAddress { get; }

    /// <summary>
    /// Gets the <see cref="SetMacAddressStateOperation"/>.
    /// </summary>
    SetMacAddressStateOperation SetMacAddressState { get; }

    /// <summary>
    /// Gets the <see cref="SetIpAddressStateOperation"/>.
    /// </summary>
    SetIpAddressStateOperation SetIpAddressState { get; }
}
