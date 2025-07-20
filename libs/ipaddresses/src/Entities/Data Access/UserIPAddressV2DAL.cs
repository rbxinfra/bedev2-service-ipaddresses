namespace Roblox.IpAddresses.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

/// <summary>
/// Represents the Data Access Layer for the <see cref="UserIPAddressV2"/> Entity.
/// </summary>
public class UserIPAddressV2DAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxIpAddresses;

    internal long ID { get; set; }
    internal long UserID { get; set; }
    internal long IPAddressID { get; set; }
    internal byte State { get; set; }
    internal DateTime? LastSeen { get; set; }
    internal DateTime Created { get; set; }

    private static UserIPAddressV2DAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new UserIPAddressV2DAL();
        dal.ID = (long)record["ID"];
        dal.UserID = (long)record["UserID"];
        dal.IPAddressID = (long)record["IPAddressID"];
        dal.State = (byte)record["State"];
        dal.LastSeen = record["LastSeen"] != null ? (DateTime)record["LastSeen"] : default(DateTime);
        dal.Created = (DateTime)record["Created"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("UserIPAddressesV3_DeleteUserIPAddressV2ByID", ID);
    }

    internal void Insert()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID) { Direction = ParameterDirection.Output },
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@IPAddressID", IPAddressID),
            new SqlParameter("@State", State),
            new SqlParameter("@LastSeen", LastSeen == null ? DBNull.Value : (object)LastSeen),
            new SqlParameter("@Created", Created),
        };

        ID = _Database.Insert<long>("UserIPAddressesV3_InsertUserIPAddressV2", queryParameters);
    }

    internal void Update()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@IPAddressID", IPAddressID),
            new SqlParameter("@State", State),
            new SqlParameter("@LastSeen", LastSeen == null ? DBNull.Value : (object)LastSeen),
            new SqlParameter("@Created", Created),
        };

        _Database.Update("UserIPAddressesV3_UpdateUserIPAddressV2ByID", queryParameters);
    }

    internal static UserIPAddressV2DAL Get(long id)
    {
        return _Database.Get(
            "UserIPAddressesV3_GetUserIPAddressV2ByID",
            id,
            BuildDAL
        );
    }

    internal static ICollection<long> GetUserIPAddressesV2ByUserPaged(long userID, long startRowIndex, long maximumRows)
    {
        if (userID == default(long)) 
            throw new ArgumentException("Parameter 'userID' cannot be null, empty or the default value.");
        if (startRowIndex < 1)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userID),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "UserIPAddressesV3_GetUserIPAddressV2IDsByUserID_Paged",
            queryParameters
        );
    }

    internal static ICollection<long> GetUserIPAddressesV2ByAddressPaged(long IPAddressID, long startRowIndex, long maximumRows)
    {
        if (IPAddressID == default(long)) 
            throw new ArgumentException("Parameter 'IPAddressID' cannot be null, empty or the default value.");
        if (startRowIndex < 1)
            throw new ApplicationException("Required value not specified: StartRowIndex.");
        if (maximumRows < 1)
            throw new ApplicationException("Required value not specified: MaximumRows.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@IPAddressID", IPAddressID),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };

        return _Database.GetIDCollection<long>(
            "UserIPAddressesV3_GetUserIPAddressV2IDsByIPAddressID_Paged",
            queryParameters
        );
    }

    internal static int GetTotalNumberOfUserIPAddressesV2ByUser(long userID)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userID),
        };

        return _Database.GetCount<int>(
            "UserIPAddressesV3_GetTotalNumberOfUserIPAddressV2sByUserID",
            queryParameters: queryParameters
        );
    }

    internal static int GetTotalNumberOfUserIPAddressesV2ByAddress(long IPAddressID)
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@IPAddressID", IPAddressID),
        };

        return _Database.GetCount<int>(
            "UserIPAddressesV3_GetTotalNumberOfUserIPAddressV2sByIPAddressID",
            queryParameters: queryParameters
        );
    }

    internal static EntityHelper.GetOrCreateDALWrapper<UserIPAddressV2DAL> GetOrCreate(long userID, long IPAddressID)
    {
        if (userID == default(long)) 
            return null;
        if (IPAddressID == default(long)) 
            return null;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@CreatedNewEntity", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@UserID", userID),
            new SqlParameter("@IPAddressID", IPAddressID),
        };

        return _Database.GetOrCreate(
            "UserIPAddressesV3_GetOrCreateUserIPAddressV2",
            BuildDAL,
            queryParameters
        );
    }

    internal static ICollection<long> GetUserIPAddressesV2ByUserID(long userID, int count, DateTime? exclusiveStartLastSeen, long? exclusiveStartId)
    {
        if (userID == default(long)) 
            throw new ArgumentException("Parameter 'userID' cannot be null, empty or the default value.");
        if (count < 1)
            throw new ApplicationException("Required value not specified: Count.");
        if (exclusiveStartId != null && exclusiveStartId < 0)
            throw new ApplicationException("Parameter 'ExclusiveStartID' cannot be negative.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userID),
            new SqlParameter("@Count", count),
            new SqlParameter("@ExclusiveStartLastSeen", exclusiveStartLastSeen != null ? exclusiveStartLastSeen.Value.ToUniversalTime() : DBNull.Value),
            new SqlParameter("@ExclusiveStartID", exclusiveStartId != null ? exclusiveStartId.Value : DBNull.Value),
        };

        return _Database.GetIDCollection<long>(
            "UserIPAddressesV3_GetUserIPAddressV2IDsByUserID",
            queryParameters
        );
    }

    internal static ICollection<UserIPAddressV2DAL> MultiGet(IEnumerable<long> ids)
    {
        return _Database.MultiGet(
            "UserIPAddressesV3_GetUserIPAddressV2sByIDs",
            ids,
            BuildDAL
        );
    }
}
