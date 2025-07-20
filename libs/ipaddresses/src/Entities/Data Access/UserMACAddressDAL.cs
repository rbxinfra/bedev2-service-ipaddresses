namespace Roblox.IpAddresses.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Entities.Mssql;

/// <summary>
/// Represents the Data Access Layer for the <see cref="UserMACAddress"/> Entity.
/// </summary>
[Serializable]
public class UserMACAddressDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxMacAddresses;

    internal long ID { get; set; }
    internal long UserID { get; set; }
    internal long MACAddressID { get; set; }
    internal DateTime Created { get; set; } = DateTime.MinValue;

    private static UserMACAddressDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new UserMACAddressDAL();
        dal.ID = (int)record["ID"];
        dal.UserID = (long)record["UserID"];
        dal.MACAddressID = (long)record["MACAddressID"];
        dal.Created = (DateTime)record["Created"];

        return dal;
    }

    internal void Insert()
    {
        if (UserID == 0)
            throw new ApplicationException("Required value not specified: UserID.");
        if (MACAddressID == 0)
            throw new ApplicationException("Required value not specified: MACAddressID.");
        if (Created.Equals(DateTime.MinValue))
            throw new ApplicationException("Required value not specified: Created.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", SqlDbType.Int)  { Direction = ParameterDirection.Output },
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@MACAddressID", MACAddressID),
            new SqlParameter("@Created", Created),
        };

        ID = _Database.Insert<int>("UserMACAddresses_InsertUserMACAddress", queryParameters);
    }

    internal void Update()
    {
        if (ID == 0)
            throw new ApplicationException("Required value not specified: ID.");
        if (UserID == 0)
            throw new ApplicationException("Required value not specified: UserID.");
        if (MACAddressID == 0)
            throw new ApplicationException("Required value not specified: MACAddressID.");
        if (Created.Equals(DateTime.MinValue))
            throw new ApplicationException("Required value not specified: Created.");

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@IPAddressID", MACAddressID),
            new SqlParameter("@Created", Created),
        };

        _Database.Update("UserMACAddresses_UpdateUserMACAddressByID", queryParameters);
    }

    internal static UserMACAddressDAL Get(long id)
    {
        return _Database.Get("UserMACAddresses_GetUserMACAddressByID", id, BuildDAL);
    }

    internal static UserMACAddressDAL GetUserMACAddressByUserIDAndMACAddressID(long userId, long macAddressId)
    {
        var sqlParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@MACAddressID", macAddressId)
        };
        return _Database.Lookup("UserMACAddresses_GetUserMACAddressByUserIDAndMACAddressID", BuildDAL, sqlParameters);
    }

    internal static ICollection<long> GetUserMACAddressIDsByUserPaged(long userId, int startRowIndex, long maximumRows)
    {
        if (userId == 0)
            return new List<long>();

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };
        return _Database.GetIDCollection<long>("UserMACAddresses_GetUserMACAddressIDsByUserID_Paged", queryParameters);
    }

    internal static ICollection<long> GetUserMACAddressIDsByAddressPaged(long macAddressId, int startRowIndex, long maximumRows)
    {
        if (macAddressId == 0)
            return new List<long>();

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@MACAddressID", macAddressId),
            new SqlParameter("@StartRowIndex", startRowIndex),
            new SqlParameter("@MaximumRows", maximumRows)
        };
        return _Database.GetIDCollection<long>("UserMACAddresses_GetUserMACAddressIDsByMACAddressID_Paged", queryParameters);
    }

    internal static long GetTotalNumberOfUserMACAddressesByUser(long userId)
    {
        if (userId == 0)
            return 0;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", userId)
        };
        return _Database.GetCount<long>("UserMACAddresses_GetTotalNumberOfUserMACAddressesByUserID", queryParameters);
    }

    internal static long GetTotalNumberOfUserMACAddressesByAddress(long macAddressId)
    {
        if (macAddressId == 0)
            return 0;

        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@MACAddressID", macAddressId)
        };
        return _Database.GetCount<long>("UserMACAddresses_GetTotalNumberOfUserMACAddressesByMACAddressID", queryParameters);
    }
}
