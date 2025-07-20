namespace Roblox.IpAddresses.Extensions;

using System;

using Google.Protobuf.WellKnownTypes;

using GrpcIpAddress = IpAddresses.V1.IpAddress;
using GrpcMacAddress = IpAddresses.V1.MacAddress;
using GrpcIpAddressType = IpAddresses.V1.IpAddressType;
using GrpcUserMacAddress = IpAddresses.V1.UserMacAddress;
using GrpcIpAddressState = IpAddresses.V1.IpAddressState;
using GrpcMacAddressState = IpAddresses.V1.MacAddressState;
using GrpcUserIpAddressV2 = IpAddresses.V1.UserIpAddressV2;
using GrpcSetIpAddressStateResult = IpAddresses.V1.SetIpAddressStateResult;

using IpAddress = Entities.IPAddress;
using MacAddress = Entities.MACAddress;
using IpAddressType = Enums.IpAddressType;
using IpAddressState = Enums.IpAddressState;
using MacAddressState = Enums.MacAddressState;
using UserMacAddress = Entities.UserMACAddress;
using UserIpAddressV2 = Entities.UserIPAddressV2;
using SetIpAddressStateResult = Enums.SetIpAddressStateResult;

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
/// <seealso cref="IpAddressState"/>
/// <seealso cref="GrpcIpAddressState"/>
/// <seealso cref="MacAddressState"/>
/// <seealso cref="GrpcMacAddressState"/>
/// <seealso cref="UserIpAddressV2"/>
/// <seealso cref="GrpcUserIpAddressV2"/>
/// <seealso cref="GrpcSetIpAddressStateResult"/>
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

    #region IpAddressState

    /// <summary>
    /// Convert a <see cref="IpAddressState"/> to a <see cref="GrpcIpAddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="IpAddressState"/> to convert.</param>
    /// <returns>The converted <see cref="IpAddressState"/>.</returns>
    public static GrpcIpAddressState ToGrpc(this IpAddressState state)
    {
        return state switch
        {
            IpAddressState.Unknown => GrpcIpAddressState.Unknown,
            IpAddressState.Allowed => GrpcIpAddressState.Allowed,
            IpAddressState.Banned => GrpcIpAddressState.Banned,
            _ => GrpcIpAddressState.Unknown
        };
    }

    /// <summary>
    /// Convert a <see cref="GrpcIpAddressState"/> to a <see cref="IpAddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="GrpcIpAddressState"/> to convert.</param>
    /// <returns>The converted <see cref="IpAddressState"/>.</returns>
    public static IpAddressState FromGrpc(this GrpcIpAddressState state)
    {
        return state switch
        {
            GrpcIpAddressState.Unknown => IpAddressState.Unknown,
            GrpcIpAddressState.Allowed => IpAddressState.Allowed,
            GrpcIpAddressState.Banned => IpAddressState.Banned,
            _ => IpAddressState.Unknown
        };
    }

    #endregion

    #region MacAddressState

    /// <summary>
    /// Convert a <see cref="MacAddressState"/> to a <see cref="GrpcMacAddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="MacAddressState"/> to convert.</param>
    /// <returns>The converted <see cref="MacAddressState"/>.</returns>
    public static GrpcMacAddressState ToGrpc(this MacAddressState state)
    {
        return state switch
        {
            MacAddressState.Unknown => GrpcMacAddressState.Unknown,
            MacAddressState.Allowed => GrpcMacAddressState.Allowed,
            MacAddressState.Banned => GrpcMacAddressState.Banned,
            _ => GrpcMacAddressState.Unknown
        };
    }

    /// <summary>
    /// Convert a <see cref="GrpcMacAddressState"/> to a <see cref="IpAddressState"/>.
    /// </summary>
    /// <param name="state">The <see cref="GrpcIpAddressState"/> to convert.</param>
    /// <returns>The converted <see cref="MacAddressState"/>.</returns>
    public static MacAddressState FromGrpc(this GrpcMacAddressState state)
    {
        return state switch
        {
            GrpcMacAddressState.Unknown => MacAddressState.Unknown,
            GrpcMacAddressState.Allowed => MacAddressState.Allowed,
            GrpcMacAddressState.Banned => MacAddressState.Banned,
            _ => MacAddressState.Unknown
        };
    }

    #endregion

    #region SetIpAddressStateResult

    /// <summary>
    /// Convert a <see cref="SetIpAddressStateResult"/> to a <see cref="V1.SetUserMacAddressResult"/>.
    /// </summary>
    /// <param name="type">The <see cref="SetIpAddressStateResult"/> to convert.</param>
    /// <returns>The converted <see cref="SetIpAddressStateResult"/>.</returns>
    public static GrpcSetIpAddressStateResult ToGrpc(this SetIpAddressStateResult type)
    {
        return type switch
        {
            SetIpAddressStateResult.Unknown => GrpcSetIpAddressStateResult.Unknown,
            SetIpAddressStateResult.Changed => GrpcSetIpAddressStateResult.Changed,
            SetIpAddressStateResult.Unchanged => GrpcSetIpAddressStateResult.Unchanged,
            SetIpAddressStateResult.BanExtended => GrpcSetIpAddressStateResult.BanExtended,
            _ => GrpcSetIpAddressStateResult.Unknown
        };
    }

    /// <summary>
    /// Convert a <see cref="GrpcSetIpAddressStateResult"/> to a <see cref="SetIpAddressStateResult"/>.
    /// </summary>
    /// <param name="type">The <see cref="GrpcSetIpAddressStateResult"/> to convert.</param>
    /// <returns>The converted <see cref="GrpcSetIpAddressStateResult"/>.</returns>
    public static SetIpAddressStateResult FromGrpc(this GrpcSetIpAddressStateResult type)
    {
        return type switch
        {
            GrpcSetIpAddressStateResult.Unknown => SetIpAddressStateResult.Unknown,
            GrpcSetIpAddressStateResult.Changed => SetIpAddressStateResult.Changed,
            GrpcSetIpAddressStateResult.Unchanged => SetIpAddressStateResult.Unchanged,
            GrpcSetIpAddressStateResult.BanExtended => SetIpAddressStateResult.BanExtended,
            _ => SetIpAddressStateResult.Unknown
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
