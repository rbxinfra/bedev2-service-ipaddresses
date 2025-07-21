namespace Roblox.IpAddresses.Enums;

/// <summary>
/// Result for operations which set the addresses state
/// </summary>
public enum SetAddressStateResult
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
