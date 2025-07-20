namespace Roblox.IpAddresses;

/// <summary>
/// Helper for parsing Mac Addresses.
/// </summary>
public interface IMacAddressHelper
{
    /// <summary>
    /// Is the input Mac address a valid Mac address?
    /// </summary>
    /// <param name="macAddress">The Mac address.</param>
    /// <returns>True if the input is a valid Mac address.</returns>
    bool IsValidMacAddress(string macAddress);
}
