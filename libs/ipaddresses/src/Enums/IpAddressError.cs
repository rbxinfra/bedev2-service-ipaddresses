namespace Roblox.IpAddresses.Enums;

using System.ComponentModel;

/// <summary>
/// Enum to indicate type of error or failures from the IpAddresses service.
/// </summary>
public enum IpAddressError
{
    /// <summary>
    /// Unknown Error
    /// </summary>
    Unknown,

    /// <summary>
    /// IP Address type is unsupported
    /// </summary>
    [Description("IP Address type is unsupported")]
    UnsupportedIpAddressType,

    /// <summary>
    /// IP Adddress state is unsupported
    /// </summary>
    [Description("IP Adddress state is unsupported")]
    UnsupportedIpAddressState,

    /// <summary>
    /// Invalid MAC Address
    /// </summary>
    [Description("Invalid MAC Address")]
    InvalidMacAddress,

    /// <summary>
    /// Unsupported MAC Address State
    /// </summary>
    [Description("Unsupported MAC Address State")]
    UnsupportedMacAddressState,

    /// <summary>
    /// The User ID specified was not valid.
    /// </summary>
    [Description("The User ID specified was not valid.")]
    InvalidUserId,

    /// <summary>
    /// Invalid IP Address
    /// </summary>
    [Description("Invalid IP Address")]
    InvalidIpAddress
}
