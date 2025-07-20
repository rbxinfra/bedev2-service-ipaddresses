namespace Roblox.IpAddresses.Entities;

using System;
using System.Diagnostics;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Caching;
using Roblox.Data.Interfaces;
using Roblox.Caching.Interfaces;

/// <summary>
/// Represents the Business Logic Layer for the <see cref="UserMACAddress"/> entity.
/// </summary>
[DebuggerDisplay("{GetMACAddress().Address,nq}")]
public class UserMACAddress : IRobloxEntity<long, UserMACAddressDAL>, ICacheableObject<long>
{
    private UserMACAddressDAL _EntityDAL;

    /// <inheritdoc cref="ICacheableObject{TIndex}.ID"/>
    public long ID
    {
        get { return _EntityDAL.ID; }
    }

    /// <summary>
    /// Gets the <see cref="UserID"/> of the <see cref="UserMACAddress"/>
    /// </summary>
    public long UserID
    {
        get { return _EntityDAL.UserID; }
        set { _EntityDAL.UserID = value; }
    }

    /// <summary>
    /// Gets the <see cref="MACAddressID"/> of the <see cref="UserMACAddress"/>
    /// </summary>
    public long MACAddressID
    {
        get { return _EntityDAL.MACAddressID; }
        set { _EntityDAL.MACAddressID = value; }
    }

    /// <summary>
    /// Gets the <see cref="Created"/> of the <see cref="UserMACAddress"/>
    /// </summary>
    public DateTime Created
    {
        get { return _EntityDAL.Created; }
    }

    /// <summary>
    /// Contructs a new <see cref="UserMACAddress"/> entity.
    /// </summary>
    public UserMACAddress()
    {
        _EntityDAL = new UserMACAddressDAL();
    }

    internal UserMACAddress(UserMACAddressDAL entityDAL)
    {
        _EntityDAL = entityDAL;
    }

    internal MACAddress GetMACAddress()
    {
        return MACAddress.MustGet(MACAddressID);
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

    internal static UserMACAddress Get(long id)
    {
        return EntityHelper.GetEntity<long, UserMACAddressDAL, UserMACAddress>(
            EntityCacheInfo,
            id,
            () => UserMACAddressDAL.Get(id)
        );
    }

    /// <summary>
    /// Gets a <see cref="UserMACAddress"/> entity by ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="UserMACAddress"/> entity.</param>
    /// <returns>The <see cref="UserMACAddress"/> entity or null.</returns>
    public static UserMACAddress Get(long? id)
    {
        if (id == null) return null;

        return Get(id.Value);
    }

    /// <summary>
    /// Gets <see cref="UserMACAddress"/> entities by User ID.
    /// </summary>
    /// <param name="userId">The ID of the User.</param>
    /// <param name="startRowIndex">The StartRowIndex.</param>
    /// <param name="maximumRows">The MaximumRows.</param>
    /// <returns>The <see cref="UserMACAddress"/> entities or null.</returns>
    public static ICollection<UserMACAddress> GetUserMACAddressesByUserPaged(long userId, int startRowIndex, int maximumRows)
    {
        var collectionId = string.Format("GetUserMACAddressesByUserPaged_UserID:{0}_StartRowIndex:{1}_MaximumRows:{2}", userId, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<UserMACAddress, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("UserID:{0}", userId)
            ),
            collectionId,
            () =>
            {
                return UserMACAddressDAL.GetUserMACAddressIDsByUserPaged(
                    userId,
                    startRowIndex + 1,
                    maximumRows
                );
            },
            Get
        );
    }

    /// <summary>
    /// Gets <see cref="UserMACAddress"/> entities by User ID.
    /// </summary>
    /// <param name="macAddressId">The ID of the <see cref="MACAddress"/> entity.</param>
    /// <param name="startRowIndex">The StartRowIndex.</param>
    /// <param name="maximumRows">The MaximumRows.</param>
    /// <returns>The <see cref="UserMACAddress"/> entities or null.</returns>
    public static ICollection<UserMACAddress> GetUserMACAddressesByAddressPaged(long macAddressId, int startRowIndex, int maximumRows)
    {
        var collectionId = string.Format("GetUserMACAddressesByAddressPaged_MACAddressID:{0}_StartRowIndex:{1}_MaximumRows:{2}", macAddressId, startRowIndex, maximumRows);

        return EntityHelper.GetEntityCollection<UserMACAddress, long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("MACAddressID:{0}", macAddressId)
            ),
            collectionId,
            () =>
            {
                return UserMACAddressDAL.GetUserMACAddressIDsByAddressPaged(
                    macAddressId,
                    startRowIndex + 1,
                    maximumRows
                );
            },
            Get
        );
    }

    /// <summary>
    /// Gets the total number of <see cref="UserMACAddress"/> by User ID.
    /// </summary>
    /// <param name="userId">The ID of the User.</param>
    /// <returns>The total number of <see cref="UserMACAddress"/> associated with this User ID.</returns>
    public static long GetTotalNumberOfUserMACAddressesByUser(long userId)
    {
        var countId = string.Format("GetTotalNumberOfUserMACAddressesByUser_UserID:{0}", userId);

        return EntityHelper.GetEntityCount<long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("UserID:{0}", userId)
            ), countId,
            () => UserMACAddressDAL.GetTotalNumberOfUserMACAddressesByUser(userId)
        );
    }

    /// <summary>
    /// Gets the total number of <see cref="UserMACAddress"/> by MACAddress ID.
    /// </summary>
    /// <param name="macAddressId">The ID of the <see cref="MACAddress"/> entity.</param>
    /// <returns>The total number of <see cref="UserMACAddress"/> associated with this MACAddress ID.</returns>
    public static long GetTotalNumberOfUserMACAddressesByAddress(long macAddressId)
    {
        var countId = string.Format("GetTotalNumberOfUserMACAddressesByAddress_MACAddressID:{0}", macAddressId);

        return EntityHelper.GetEntityCount<long>(
            EntityCacheInfo,
            new CacheManager.CachePolicy(
                CacheManager.CacheScopeFilter.Qualified,
                string.Format("MACAddressID:{0}", macAddressId)
            ), countId,
            () => UserMACAddressDAL.GetTotalNumberOfUserMACAddressesByAddress(macAddressId)
        );
    }

    #region IRobloxEntity Members

    /// <inheritdoc cref="IRobloxEntity{TIndex, TDal}.Construct(TDal)"/>
    public void Construct(UserMACAddressDAL dal)
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
        typeof(UserMACAddress).ToString(),
        true
    );

    /// <inheritdoc cref="ICacheableObject.BuildEntityIDLookups"/>
    public IEnumerable<string> BuildEntityIDLookups()
    {
        if (_EntityDAL != null)
        {
            yield return string.Format("UserMACAddress:{0}", UserID);
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
