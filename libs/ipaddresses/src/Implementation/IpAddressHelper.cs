namespace Roblox.IpAddresses;

using System.Net;
using System.Net.Sockets;

using Enums;

/// <inheritdoc cref="IIpAddressHelper"/>
public class IpAddressHelper : IIpAddressHelper
{
    /// <inheritdoc cref="IIpAddressHelper.IsValidIpAddress(string)"/>
    public bool IsValidIpAddress(string ipAddress)
        => IPAddress.TryParse(ipAddress, out _);

    /// <inheritdoc cref="IIpAddressHelper.GetAddressType(string)"/>
    public IpAddressType GetAddressType(string ip)
    {
        if (!IPAddress.TryParse(ip, out var newip))
            return IpAddressType.Unknown;

        if (newip.AddressFamily == AddressFamily.InterNetwork)
            return IpAddressType.IPv4;

        return IpAddressType.IPv6;
    }
}
