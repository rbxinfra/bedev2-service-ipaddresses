namespace Roblox.IpAddresses;

using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

/// <inheritdoc cref="IMacAddressHelper"/>
public class MacAddressHelper : IMacAddressHelper
{
    private static readonly Regex _MacAddressFormatRegex = new("^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");
    private static readonly Regex _MacAddressSimpleRegex = new("^[a-fA-F0-9]{12}$");

    /// <inheritdoc cref="IMacAddressHelper.IsValidMacAddress(string)"/>
    public bool IsValidMacAddress(string macAddress)
    {
        if (_MacAddressFormatRegex.IsMatch(macAddress) || _MacAddressSimpleRegex.IsMatch(macAddress))
            return true;

        try
        {
            var addr = PhysicalAddress.Parse(macAddress.ToUpper());
            if (PhysicalAddress.None == addr)
                return false;
        }
        catch (FormatException)
        {
            return false;
        }

        return true;
    }
}
