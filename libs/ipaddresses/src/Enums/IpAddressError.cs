namespace Roblox.IpAddresses.Enums;

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
    UnsupportedIpAddressType,

    /// <summary>
    /// IP Adddress state is unsupported
    /// </summary>
    UnsupportedIpAddressState,

    /// <summary>
    /// Invalid MAC Address
    /// </summary>
    InvalidMacAddress,

    /// <summary>
    /// Unsupported MAC Address State
    /// </summary>
    UnsupportedMacAddressState,

    /// <summary>
    /// The User ID specified was not valid.
    /// </summary>
    InvalidUserId,

    /// <summary>
    /// Invalid IP Address
    /// </summary>
    InvalidIpAddress
}
