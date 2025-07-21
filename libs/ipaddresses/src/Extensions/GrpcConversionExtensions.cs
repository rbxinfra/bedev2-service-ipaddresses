namespace Roblox.IpAddresses.Extensions;

using System;

using Google.Protobuf.WellKnownTypes;

using GrpcIpAddress = Ipaddresses.Ipaddresses.V1.IpAddress;
using GrpcMacAddress = Ipaddresses.Ipaddresses.V1.MacAddress;
using GrpcIpAddressType = Ipaddresses.Ipaddresses.V1.IpAddressType;
using GrpcUserMacAddress = Ipaddresses.Ipaddresses.V1.UserMacAddress;
using GrpcAddressState = Ipaddresses.Ipaddresses.V1.AddressState;
using GrpcUserIpAddressV2 = Ipaddresses.Ipaddresses.V1.UserIpAddressV2;


using IpAddress = Entities.IPAddress;
using MacAddress = Entities.MACAddress;
using IpAddressType = Enums.IpAddressType;
using AddressState = Enums.AddressState;
using UserMacAddress = Entities.UserMACAddress;
using UserIpAddressV2 = Entities.UserIPAddressV2;

/// <summary>
/// Extensions for converting between gRPC and local types, and vice versa.
/// </summary>
/// <seealso cref="IpAddress"/>
/// <seealso cref="GrpcIpAddress"/>
/// <seealso cref="MacAddress"/>
/// <seealso cref="GrpcMacAddress"/>
/// <seealso cref="IpAddressType"/>
/// <seealso cref="GrpcIpAddressType"/>
/// <seealso cref="UserMacAddress"/>
/// <seealso cref="GrpcUserMacAddress"/>
/// <seealso cref="AddressState"/>
/// <seealso cref="GrpcAddressState"/>
/// <seealso cref="UserIpAddressV2"/>
/// <seealso cref="GrpcUserIpAddressV2"/>
public static class GrpcConversionExtensions
{
    #region IpAddressType

    /// <summary>
    /// Convert a <see cref="IpAddressType"/> to a <see cref="GrpcIpAddressType"/>.
    /// </summary>
    /// <param name="type">The <see cref="IpAddressType"/> to convert.</param>
    /// <returns>The converted <see cref="IpAddressType"/>.</returns>
    public static GrpcIpAddressType ToGrpc(this IpAddressType type)
    {
        return type switch
        {
            IpAddressType.Unknown => GrpcIpAddressType.Unknown,
            IpAddressType.IPv4 => GrpcIpAddressType.Ipv4,
            IpAddressType.IPv6 => GrpcIpAddressType.Ipv6,
            _ => GrpcIpAddressType.Unknown
        };
    }

    /// <summary>
    /// Convert a <see cref="GrpcIpAddressType"/> to a <see cref="IpAddressType"/>.
    /// </summary>
    /// <param name="type">The <see cref="GrpcIpAddressType"/> to convert.</param>
    /// <returns>The converted <see cref="IpAddressType"/>.</returns>
    public static IpAddressType FromGrpc(this GrpcIpAddressType type)
    {
        return type switch
        {
            GrpcIpAddressType.Unknown => IpAddressType.Unknown,
            GrpcIpAddressType.Ipv4 => IpAddressType.IPv4,
            GrpcIpAddressType.Ipv6 => IpAddressType.IPv6,
            _ => IpAddressType.Unknown
        };
    }

    #endregion

    #region AddressState

    /// <summary>
    /// Convert a <see cref="AddressState"/> to a <see cref="GrpcAddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="AddressState"/> to convert.</param>
    /// <returns>The converted <see cref="AddressState"/>.</returns>
    public static GrpcAddressState ToGrpc(this AddressState state)
    {
        return state switch
        {
            AddressState.Unknown => GrpcAddressState.Unknown,
            AddressState.Allowed => GrpcAddressState.Allowed,
            AddressState.Banned => GrpcAddressState.Banned,
            _ => GrpcAddressState.Unknown
        };
    }

    /// <summary>
    /// Convert a <see cref="GrpcAddressState"/> to a <see cref="AddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="AddressState"/> to convert.</param>
    /// <returns>The converted <see cref="AddressState"/>.</returns>
    public static AddressState FromGrpc(this GrpcAddressState state)
    {
        return state switch
        {
            GrpcAddressState.Unknown => AddressState.Unknown,
            GrpcAddressState.Allowed => AddressState.Allowed,
            GrpcAddressState.Banned => AddressState.Banned,
            _ => AddressState.Unknown
        };
    }

    #endregion

    #region IpAddress

    /// <summary>
    /// Convert a <see cref="IpAddress"/> to a <see cref="GrpcIpAddress"/>.
    /// </summary>
    /// <param name="ipAddress">The <see cref="IpAddress"/> to convert.</param>
    /// <returns>The converted <see cref="GrpcIpAddress"/>.</returns>
    public static GrpcIpAddress ToGrpc(this IpAddress ipAddress)
    {
        if (ipAddress == null) return null;

        return new GrpcIpAddress
        {
            Id = ipAddress.ID,
            Address = ipAddress.Address,
            State = ipAddress.State.ToGrpc(),
            ExpirationTime = ipAddress.Expiration.HasValue
                ? DateTime.SpecifyKind(ipAddress.Expiration.Value, DateTimeKind.Utc).ToTimestamp()
                : null,
            CreatedTime = DateTime.SpecifyKind(ipAddress.Created, DateTimeKind.Utc).ToTimestamp(),
            UpdatedTime = ipAddress.Updated.HasValue
                ? DateTime.SpecifyKind(ipAddress.Updated.Value, DateTimeKind.Utc).ToTimestamp()
                : null
        };
    }

    #endregion

    #region MacAddress

    /// <summary>
    /// Convert a <see cref="MacAddress"/> to a <see cref="GrpcMacAddress"/>.
    /// </summary>
    /// <param name="macAddress">The <see cref="MacAddress"/> to convert.</param>
    /// <returns>The converted <see cref="GrpcMacAddress"/>.</returns>
    public static GrpcMacAddress ToGrpc(this MacAddress macAddress)
    {
        if (macAddress == null) return null;

        return new GrpcMacAddress
        {
            Id = macAddress.ID,
            MacAddress_ = macAddress.Address,
            State = macAddress.State.ToGrpc(),
            ExpirationTime = macAddress.Expiration.HasValue
                ? DateTime.SpecifyKind(macAddress.Expiration.Value, DateTimeKind.Utc).ToTimestamp()
                : null,
            CreatedTime = DateTime.SpecifyKind(macAddress.Created, DateTimeKind.Utc).ToTimestamp(),
            UpdatedTime = DateTime.SpecifyKind(macAddress.Updated, DateTimeKind.Utc).ToTimestamp()
        };
    }

    #endregion
}
