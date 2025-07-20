namespace Roblox.IpAddresses.Enums;

/// <summary>
/// Result for SetMacAddressStateOperation
/// </summary>
public enum SetMacAddressStateResult
{
    /// <summary>
    /// Unknown Result
    /// </summary>
    Unknown,

    /// <summary>
    /// MAC Address Changed
    /// </summary>
    Changed,

    /// <summary>
    /// MAC Address Unchanged
    /// </summary>
    Unchanged,

    /// <summary>
    /// MAC Address ban extended
    /// </summary>
    BanExtended
}
