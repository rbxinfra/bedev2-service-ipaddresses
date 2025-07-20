namespace Roblox.IpAddresses;

using Enums;

/// <summary>
/// Helper for parsing IP Addresses.
/// </summary>
public interface IIpAddressHelper
{
    /// <summary>
    /// Is the input IP address a valid IP address?
    /// </summary>
    /// <param name="ipAddress">The IP address.</param>
    /// <returns>True if the input is a valid IP address.</returns>
    bool IsValidIpAddress(string ipAddress);

    /// <summary>
    /// Get the type of an IP address based on the input.
    /// </summary>
    /// <param name="ip">The IP address.</param>
    /// <returns>The type of the IP address.</returns>
    IpAddressType GetAddressType(string ip);
}
