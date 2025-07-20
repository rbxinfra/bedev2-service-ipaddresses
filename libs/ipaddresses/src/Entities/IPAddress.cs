namespace Roblox.IpAddresses.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

using Enums;

/// <summary>
/// Represents the Business Logic Layer for the <see cref="IPAddress"/> entity.
/// </summary>
public class IPAddress : IRobloxEntity<long, IPAddressDAL>, IRemoteCacheableObject
{
    private IPAddressDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    /// <summary>
    /// Gets the <see cref="Address"/> of the <see cref="IPAddress"/>
    /// </summary>
    public string Address
    {
        get { return _EntityDAL.Address; }
    }

    /// <summary>
    /// Gets or sets the <see cref="State"/> of the <see cref="IPAddress"/>
    /// </summary>
    public IpAddressState State
    {
        get
        {
            PerformStateExpirationCheck();

            return (IpAddressState)_EntityDAL.State;
        }
        set { _EntityDAL.State = (byte)value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Expiration"/> of the <see cref="IPAddress"/>
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
    /// Gets the <see cref="Created"/> of the <see cref="IPAddress"/>
    /// </summary>
    public DateTime Created
    {
        get { return _EntityDAL.Created; }
    }

    /// <summary>
    /// Gets the <see cref="Updated"/> of the <see cref="IPAddress"/>
    /// </summary>
    public DateTime? Updated
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

            return _EntityDAL.State == (byte)IpAddressState.Banned;
        }
    }

    private void PerformStateExpirationCheck()
    {
        if (_EntityDAL.State == (byte)IpAddressState.Banned &&
            _EntityDAL.Expiration != null &&
            DateTime.Now >= _EntityDAL.Expiration.Value)
            return;

        State = IpAddressState.Allowed;
        Expiration = null;
        Save();
    }

    /// <summary>
    /// Contructs a new <see cref="IPAddress"/> entity.
    /// </summary>
    public IPAddress()
    { 
        _EntityDAL = new IPAddressDAL();
    }

    internal IPAddress(IPAddressDAL dal)
    {
        _EntityDAL = dal;
    }


    internal void Delete()
    {
        EntityHelper.DeleteEntityWithRemoteCache(
            this,
            _EntityDAL.Delete
        );
    }

    internal void Save()
    {
        EntityHelper.SaveEntityWithRemoteCache(
            this, 
            () =>
            {
                _EntityDAL.Created = DateTime.Now;
                _EntityDAL.Updated = _EntityDAL.Created;
                _EntityDAL.Insert();
            }, 
            () =>
            {
                _EntityDAL.Updated = DateTime.Now;
                _EntityDAL.Update();
            }
        );
    }

    internal static IPAddress Get(long id)
    {
        return EntityHelper.GetEntity<long, IPAddressDAL, IPAddress>(
            EntityCacheInfo, 
            id, 
            () => IPAddressDAL.Get(id)
        );
    }

    /// <summary>
    /// Gets a <see cref="IPAddress"/> entity by Address.
    /// </summary>
    /// <param name="address">The Address of the <see cref="IPAddress"/> entity.</param>
    /// <returns>The <see cref="IPAddress"/> entity or null.</returns>
    public static IPAddress Get(string address)
    {
        return EntityHelper.GetEntityByLookupWithRemoteCache<long, IPAddressDAL, IPAddress>(
            EntityCacheInfo,
            string.Format("Address:{0}", address),
            () => IPAddressDAL.GetByAddress(address),
            Get
        );
    }

    /// <summary>
    /// Gets or creates a <see cref="IPAddress"/> entity by Address.
    /// </summary>
    /// <param name="address">The Address of the <see cref="IPAddress"/> entity.</param>
    /// <returns>The <see cref="IPAddress"/> entity or null.</returns>
    public static IPAddress GetOrCreate(string address)
    {
        return EntityHelper.GetOrCreateEntityWithRemoteCache<long, IPAddress>(
            EntityCacheInfo,
            string.Format("Address:{0}", address),
            () => DoGetOrCreate(address),
            Get
        );
    }

    private static IPAddress DoGetOrCreate(string address)
    {
        return EntityHelper.DoGetOrCreate<long, IPAddressDAL, IPAddress>(
            () => IPAddressDAL.GetOrCreate(address)
        );
    }

    /// <summary>
    /// Multi get <see cref="IPAddress"/> entities by IDs.
    /// </summary>
    /// <param name="ids">The IDs of the <see cref="IPAddress"/> entity.</param>
    /// <returns>The <see cref="IPAddress"/> entities.</returns>
    public static ICollection<IPAddress> MultiGet(IEnumerable<long> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (!ids.Any()) return Array.Empty<IPAddress>();

        return EntityHelper.GetEntitiesByIds<IPAddress, IPAddressDAL, long>(
            EntityCacheInfo,
            ids.Distinct().ToList(),
            IPAddressDAL.MultiGet
        ).ToList();
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(IPAddressDAL dal)
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
        new CacheabilitySettings(collectionsAreCacheable: false, countsAreCacheable: false, entityIsCacheable: true, idLookupsAreCacheable: true, hasUnqualifiedCollections: false, idLookupsAreCaseSensitive: false),
        typeof(IPAddress).ToString(),
        true
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        yield return string.Format("Address:{0}", 0, Address);
        yield break;
    }

    /// <inheritdoc cref="ICacheableObject.BuildStateTokenCollection"/>
    public IEnumerable<StateToken> BuildStateTokenCollection()
    {
        yield break;
    }

    #endregion ICacheableObject Members

    #region IRemoteCacheableObject Members

    /// <inheritdoc cref="IRemoteCacheableObject.GetSerializable"/>
    public object GetSerializable()
    {
        return _EntityDAL;
    }

    #endregion IRemoteCacheableObject Members
}
