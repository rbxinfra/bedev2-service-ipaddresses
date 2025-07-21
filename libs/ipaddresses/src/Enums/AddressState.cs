namespace Roblox.IpAddresses.Enums;

/// <summary>
/// Shared enum for State of <see cref="Entities.IPAddress"/> and <see cref="Entities.MACAddress"/> 
/// </summary>
public enum AddressState
{
    /// <summary>
    /// Unknown Address State
    /// </summary>
    Unknown,

    /// <summary>
    /// Address is Open
    /// </summary>
    Allowed,

    /// <summary>
    /// Address is Banned
    /// </summary>
    Banned
}
