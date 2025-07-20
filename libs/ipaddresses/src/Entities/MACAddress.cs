namespace Roblox.IpAddresses.Entities;

using System;
using System.Diagnostics;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

using Enums;

/// <summary>
/// Represents the Business Logic Layer for the <see cref="MACAddress"/> entity.
/// </summary>
[DebuggerDisplay("{Address,nq}")]
public class MACAddress : IRobloxEntity<long, MACAddressDAL>, ICacheableObject<long>
{
    private MACAddressDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    /// <summary>
    /// Gets the <see cref="Address"/> of the <see cref="MACAddress"/>
    /// </summary>
    public string Address
    {
        get { return _EntityDAL.MACAddress; }
    }

    /// <summary>
    /// Gets the <see cref="State"/> of the <see cref="MACAddress"/>
    /// </summary>
    public MacAddressState State
    {
        get
        {
            PerformStateExpirationCheck();

            return (MacAddressState)_EntityDAL.State;
        }
        set { _EntityDAL.State = (byte)value; }
    }

    /// <summary>
    /// Gets the <see cref="Expiration"/> of the <see cref="MACAddress"/>
    /// </summary>
    public DateTime? Expiration
    {
        get
        {
            PerformStateExpirationCheck();

            return _EntityDAL.Expiration;
        }
        set { _EntityDAL.Expiration = value; }
    }

    /// <summary>
    /// Gets the <see cref="Created"/> of the <see cref="MACAddress"/>
    /// </summary>
    public DateTime Created
    {
        get { return _EntityDAL.Created; }
    }

    /// <summary>
    /// Gets the <see cref="Updated"/> of the <see cref="MACAddress"/>
    /// </summary>
    public DateTime Updated
    {
        get { return _EntityDAL.Updated; }
    }

    /// <summary>
    /// Gets the <see cref="IsBanned"/> of the <see cref="MACAddress"/>
    /// </summary>
    public bool IsBanned
    {
        get
        {
            PerformStateExpirationCheck();

            return _EntityDAL.State == (byte)MacAddressState.Banned;
        }
    }

    private void PerformStateExpirationCheck()
    {
        if (_EntityDAL.State == (byte)MacAddressState.Banned &&
            _EntityDAL.Expiration != null &&
            DateTime.Now >= _EntityDAL.Expiration.Value)
            return;

        State = MacAddressState.Allowed;
        Expiration = null;
        Save();
    }

    /// <summary>
    /// Contructs a new <see cref="MACAddress"/> entity.
    /// </summary>
    public MACAddress()
    {
        _EntityDAL = new MACAddressDAL();
    }

    internal MACAddress(MACAddressDAL entityDAL)
    {
        _EntityDAL = entityDAL;
    }

    internal void Delete()
    {
        EntityHelper.DeleteEntity(
            this,
            _EntityDAL.Delete
        );
    }

    internal void Save()
    {
        EntityHelper.SaveEntity(
            this,
            () =>
            {
                _EntityDAL.Created = DateTime.Now;
                _EntityDAL.Updated = Created;
                _EntityDAL.Insert();
            },
            () =>
            {
                _EntityDAL.Updated = DateTime.Now;
                _EntityDAL.Update();
            }
        );
    }

    internal static MACAddress Get(long id)
    {
        var entity = EntityHelper.GetEntity<long, MACAddressDAL, MACAddress>(
            EntityCacheInfo,
            id,
            () => MACAddressDAL.Get(id)
        );
        entity.PerformStateExpirationCheck();

        return entity;
    }

    /// <summary>
    /// Gets a <see cref="MACAddress"/> entity by ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="MACAddress"/> entity.</param>
    /// <returns>The <see cref="MACAddress"/> entity or null.</returns>
    public static MACAddress Get(long? id)
    {
        if (id == null) return null;

        return Get(id.Value);
    }

    /// <summary>
    /// Gets a <see cref="MACAddress"/> entity by Address.
    /// </summary>
    /// <param name="address">The Address of the <see cref="MACAddress"/> entity.</param>
    /// <returns>The <see cref="MACAddress"/> entity or null.</returns>
    public static MACAddress GetByAddress(string address)
    {
        return EntityHelper.GetEntityByLookup<long, MACAddressDAL, MACAddress>(
            EntityCacheInfo,
            string.Format("MACAddress:{0}", address),
            () => MACAddressDAL.GetByAddress(address)
        );
    }

    /// <summary>
    /// Get or create a <see cref="MACAddress"/> entity by Address.
    /// </summary>
    /// <param name="address">The Address of the <see cref="MACAddress"/> entity.</param>
    /// <returns>The <see cref="MACAddress"/> entity or null.</returns>
    public static MACAddress GetOrCreate(string address)
    {
        return EntityHelper.GetOrCreateEntity<long, MACAddress>(
            EntityCacheInfo,
            string.Format("MACAddress:{0}", address),
            () => DoGetOrCreate(address)
        );
    }

    private static MACAddress DoGetOrCreate(string address)
    {
        return EntityHelper.DoGetOrCreate<long, MACAddressDAL, MACAddress>(
            () => MACAddressDAL.GetOrCreate(address)
        );
    }

    /// <summary>
    /// Must get a <see cref="MACAddress"/> entity by ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="MACAddress"/> entity.</param>
    /// <returns>The <see cref="MACAddress"/> entity or null.</returns>
    public static MACAddress MustGet(long id)
    {
        return EntityHelper.MustGet<long, MACAddress>(
            id,
            Get
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(MACAddressDAL dal)
    {
        _EntityDAL = dal;
    }

    #endregion IRobloxEntity Members

    #region ICacheableObject Members

    /// <inheritdoc cref="ICacheableObject.CacheInfo"/>
    public CacheInfo CacheInfo
    {
        get { return EntityCacheInfo; }
    }

    /// <inheritdoc cref="ICacheableObject.CacheInfo"/>
    public static CacheInfo EntityCacheInfo = new CacheInfo(
        new CacheabilitySettings(false, true, true, true),
        typeof(MACAddress).ToString(),
        true
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        if (_EntityDAL != null)
        {
            yield return string.Format("MACAddress:{0}", Address);
        }
        yield break;
    }

    /// <inheritdoc cref="ICacheableObject.BuildStateTokenCollection"/>
    public IEnumerable<StateToken> BuildStateTokenCollection()
    {
        yield break;
    }

    #endregion ICacheableObject Members
}
