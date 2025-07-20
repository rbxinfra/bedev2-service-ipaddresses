namespace Roblox.IpAddresses.Enums;

/// <summary>
/// Result for SetIpAddressStateOperation
/// </summary>
public enum SetIpAddressStateResult
{
    /// <summary>
    /// Unknown Result
    /// </summary>
    Unknown,

    /// <summary>
    /// IP Address Changed
    /// </summary>
    Changed,

    /// <summary>
    /// IP Address Unchanged
    /// </summary>
    Unchanged,

    /// <summary>
    /// IP Address ban extended
    /// </summary>
    BanExtended
}
