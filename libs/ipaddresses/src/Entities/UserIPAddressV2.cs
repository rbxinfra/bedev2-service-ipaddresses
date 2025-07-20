namespace Roblox.IpAddresses.Entities;

using System;
using System.Linq;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

/// <summary>
/// Represents the Business Logic Layer for the <see cref="UserIPAddressV2"/> entity.
/// </summary>
public class UserIPAddressV2 : IRobloxEntity<long, UserIPAddressV2DAL>, IRemoteCacheableObject
{
    private UserIPAddressV2DAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    /// <summary>
    /// Gets the <see cref="UserID"/> of the <see cref="UserIPAddressV2"/>
    /// </summary>
    public long UserID
    {
        get { return _EntityDAL.UserID; }
        set { _EntityDAL.UserID = value; }
    }

    /// <summary>
    /// Gets the <see cref="IPAddressID"/> of the <see cref="UserIPAddressV2"/>
    /// </summary>
    public long IPAddressID
    {
        get { return _EntityDAL.IPAddressID; }
        set { _EntityDAL.IPAddressID = value; }
    }

    /// <summary>
    /// Gets the <see cref="State"/> of the <see cref="UserIPAddressV2"/>
    /// </summary>
    public byte State
    {
        get { return _EntityDAL.State; }
        set { _EntityDAL.State = value; }
    }

    /// <summary>
    /// Gets the <see cref="LastSeen"/> of the <see cref="UserIPAddressV2"/>
    /// </summary>
    public DateTime? LastSeen
    {
        get { return _EntityDAL.LastSeen; }
        set { _EntityDAL.LastSeen = value; }
    }

    /// <summary>
    /// Gets the <see cref="Created"/> of the <see cref="UserIPAddressV2"/>
    /// </summary>
    public DateTime Created
    {
        get { return _EntityDAL.Created; }
    }

    /// <summary>
    /// Contructs a new <see cref="UserIPAddressV2"/> entity.
    /// </summary>
    public UserIPAddressV2()
    { 
        _EntityDAL = new UserIPAddressV2DAL();
    }

    internal UserIPAddressV2(UserIPAddressV2DAL dal)
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
                _EntityDAL.Insert();
            }, 
            _EntityDAL.Update
        );
    }

    internal static UserIPAddressV2 Get(long id)
    {
        return EntityHelper.GetEntity<long, UserIPAddressV2DAL, UserIPAddressV2>(
            EntityCacheInfo, 
            id, 
            () => UserIPAddressV2DAL.Get(id)
        );
    }

    /// <summary>
    /// Get <see cref="UserIPAddressV2"/> entities by User ID.
    /// </summary>
    /// <param name="userID">The ID of the User.</param>
    /// <param name="startRowIndex">The StartRowIndex.</param>
    /// <param name="maximumRows">The MaximumRows.</param>
    /// <returns>The <see cref="UserIPAddressV2"/> entities.</returns>
    public static ICollection<UserIPAddressV2> GetUserIPAddressesV2ByUserPaged(long userID, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetUserIPAddressesV2ByUserPaged_UserID:{0}_StartRowIndex:{1}_MaximumRows:{2}", userID, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<UserIPAddressV2, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("UserID:{0}", userID)
            ),
            collectionId,
            () =>
            {
                return UserIPAddressV2DAL.GetUserIPAddressesV2ByUserPaged(
                    userID,
                    startRowIndex + 1,
                    maximumRows
                );
            },
            Get
        );
    }

    /// <summary>
    /// Get <see cref="UserIPAddressV2"/> entities by IPAddress ID.
    /// </summary>
    /// <param name="IPAddressID">The ID of the IPAddress.</param>
    /// <param name="startRowIndex">The StartRowIndex.</param>
    /// <param name="maximumRows">The MaximumRows.</param>
    /// <returns>The <see cref="UserIPAddressV2"/> entities.</returns>
    public static ICollection<UserIPAddressV2> GetUserIPAddressesV2ByAddressPaged(long IPAddressID, long startRowIndex, long maximumRows)
    {
        var collectionId = string.Format("GetUserIPAddressesV2ByAddressPaged_IPAddressID:{0}_StartRowIndex:{1}_MaximumRows:{2}", IPAddressID, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<UserIPAddressV2, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("IPAddressID:{0}", IPAddressID)
            ),
            collectionId,
            () =>
            {
                return UserIPAddressV2DAL.GetUserIPAddressesV2ByAddressPaged(
                    IPAddressID,
                    startRowIndex + 1,
                    maximumRows
                );
            },
            Get
        );
    }

    /// <summary>
    /// Gets the total number of <see cref="UserIPAddressV2"/> by User ID.
    /// </summary>
    /// <param name="userID">The ID of the User.</param>
    /// <returns>The total number of <see cref="UserIPAddressV2"/> associated with this User ID.</returns>
    public static int GetTotalNumberOfUserIPAddressesV2ByUser(long userID)
    {
        var countId = string.Format("GetTotalNumberOfUserIPAddressesV2ByUser_UserID:{0}", userID);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("UserID:{0}", userID)
            ),
            countId,
            () => UserIPAddressV2DAL.GetTotalNumberOfUserIPAddressesV2ByUser(userID)
        );
    }

    /// <summary>
    /// Gets the total number of <see cref="UserIPAddressV2"/> by IPAddress ID.
    /// </summary>
    /// <param name="IPAddressID">The ID of the <see cref="IPAddress"/> entity.</param>
    /// <returns>The total number of <see cref="UserIPAddressV2"/> associated with this IPAddress ID.</returns>
    public static int GetTotalNumberOfUserIPAddressesV2ByAddress(long IPAddressID)
    {
        var countId = string.Format("GetTotalNumberOfUserIPAddressesV2ByAddress_IPAddressID:{0}", IPAddressID);

        return EntityHelper.GetEntityCount<int>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("IPAddressID:{0}", IPAddressID)
            ),
            countId,
            () => UserIPAddressV2DAL.GetTotalNumberOfUserIPAddressesV2ByAddress(IPAddressID)
        );
    }

    /// <summary>
    /// Gets or creates a <see cref="UserIPAddressV2"/> entity by User ID and IPAddress ID.
    /// </summary>
    /// <param name="userID">The ID oftthe User.</param>
    /// <param name="IPAddressID">The ID of the IPAddress.</param>
    /// <returns>The <see cref="UserIPAddressV2"/> entity or null.</returns>
    public static UserIPAddressV2 GetOrCreate(long userID, long IPAddressID)
    {
        return EntityHelper.GetOrCreateEntityWithRemoteCache<long, UserIPAddressV2>(
            EntityCacheInfo,
            string.Format("UserID:{0}_IPAddressID:{1}", userID, IPAddressID),
            () => DoGetOrCreate(userID, IPAddressID),
            Get
        );
    }

    private static UserIPAddressV2 DoGetOrCreate(long userID, long IPAddressID)
    {
        return EntityHelper.DoGetOrCreate<long, UserIPAddressV2DAL, UserIPAddressV2>(
            () => UserIPAddressV2DAL.GetOrCreate(userID, IPAddressID)
        );
    }

    /// <summary>
    /// Get <see cref="UserIPAddressV2"/> entities by User ID.
    /// </summary>
    /// <param name="userID">The ID of the User.</param>
    /// <param name="count">The Count.</param>
    /// <param name="exclusiveStartLastSeen">The ExclusiveStartLastSeen.</param>
    /// <param name="exclusiveStartId">The ExclusiveStartId.</param>
    /// <returns>The <see cref="UserIPAddressV2"/> entities.</returns>
    public static ICollection<UserIPAddressV2> GetUserIPAddressesV2ByUserID(long userID, int count, DateTime? exclusiveStartLastSeen, long? exclusiveStartId)
    {
        var collectionId = string.Format("GetUserIPAddressesV2ByUserID_UserID:{0}_Count:{1}_ExclusiveStartLastSeen:{2}_ExclusiveStartID:{3}", userID, count, exclusiveStartLastSeen, exclusiveStartId);

        return EntityHelper.GetEntityCollection<UserIPAddressV2, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("UserID:{0}", userID)
            ),
            collectionId,
            () =>
            {
                return UserIPAddressV2DAL.GetUserIPAddressesV2ByUserID(
                    userID,
                    count,
                    exclusiveStartLastSeen,
                    exclusiveStartId
                );
            },
            Get
        );
    }

    /// <summary>
    /// Multi get <see cref="UserIPAddressV2"/> entities by IDs.
    /// </summary>
    /// <param name="ids">The IDs of the <see cref="UserIPAddressV2"/> entity.</param>
    /// <returns>The <see cref="UserIPAddressV2"/> entities.</returns>
    public static ICollection<UserIPAddressV2> MultiGet(IEnumerable<long> ids)
    {
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (!ids.Any()) return Array.Empty<UserIPAddressV2>();

        return EntityHelper.GetEntitiesByIds<UserIPAddressV2, UserIPAddressV2DAL, long>(
            EntityCacheInfo,
            ids.Distinct().ToList(),
            UserIPAddressV2DAL.MultiGet
        ).ToList();
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(UserIPAddressV2DAL dal)
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
        typeof(UserIPAddressV2).ToString(),
        true
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        yield return string.Format("UserID:{0}_IPAddressID:{1}", UserID, IPAddressID);
        yield break;
    }

    /// <inheritdoc cref="ICacheableObject.BuildStateTokenCollection"/>
    public IEnumerable<StateToken> BuildStateTokenCollection()
    {
        yield return new StateToken(string.Format("UserID:{0}", UserID));
        yield return new StateToken(string.Format("IPAddressID:{0}", IPAddressID));
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
