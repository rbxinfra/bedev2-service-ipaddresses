namespace Roblox.IpAddresses.Entities;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Roblox.Data;
using Roblox.Entities.Mssql;

/// <summary>
/// Represents the Data Access Layer for the <see cref="MACAddress"/> Entity.
/// </summary>
[Serializable]
public class MACAddressDAL
{
    private const Roblox.MssqlDatabases.RobloxDatabase _Database = global::Roblox.MssqlDatabases.RobloxDatabase.RobloxMacAddresses;

    internal long ID { get; set; }
    internal string MACAddress { get; set; }
    internal byte State { get; set; }
    internal DateTime? Expiration { get; set; }
    internal DateTime Created { get; set; }
    internal DateTime Updated { get; set; }

    private static MACAddressDAL BuildDAL(IDictionary<string, object> record)
    {
        var dal = new MACAddressDAL();
        dal.ID = (long)record["ID"];
        dal.MACAddress = (string)record["MACAddress"];
        dal.State = (byte)record["State"];
        dal.Expiration = (DateTime?)record["Expiration"];
        dal.Created = (DateTime)record["Created"];
        dal.Updated = (DateTime)record["Updated"];

        return dal;
    }

    internal void Delete()
    {
        _Database.Delete("MACAddresses_DeleteMACAddressByID", ID);
    }

    internal void Insert()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", SqlDbType.Int)  { Direction = ParameterDirection.Output },
            new SqlParameter("@MACAddress", MACAddress),
            new SqlParameter("@State", State),
            new SqlParameter("@Expiration", Expiration != null ? Expiration : DBNull.Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated)
        };

        ID = _Database.Insert<int>("MACAddresses_InsertMACAddress", queryParameters);
    }

    internal void Update()
    {
        var queryParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", ID),
            new SqlParameter("@MACAddress", MACAddress),
            new SqlParameter("@State", State),
            new SqlParameter("@Expiration", Expiration != null ? Expiration : DBNull.Value),
            new SqlParameter("@Created", Created),
            new SqlParameter("@Updated", Updated)
        };

        _Database.Update("MACAddresses_UpdateMACAddressByID", queryParameters);
    }

    internal static MACAddressDAL Get(long id)
    {
        return _Database.Get(
            "MACAddresses_GetMACAddressByID", 
            id, 
            BuildDAL
        );
    }

    internal static MACAddressDAL GetByAddress(string macAddress)
    {
        if (string.IsNullOrEmpty(macAddress))
            return null;

        var sqlParameters = new SqlParameter[]
        {
            new SqlParameter("@MACAddress", macAddress)
        };
        return _Database.Lookup(
            "MACAddresses_GetMACAddressByMACAddress", 
            BuildDAL, 
            sqlParameters
        );
    }

    internal static EntityHelper.GetOrCreateDALWrapper<MACAddressDAL> GetOrCreate(string macAddress)
    {
        if (string.IsNullOrWhiteSpace(macAddress))
            return null;

        var sqlParameters = new List<SqlParameter>
        {
            new SqlParameter("@CreatedNewEntity", SqlDbType.Bit) { Direction = ParameterDirection.Output },
            new SqlParameter("@MACAddress", macAddress),
        };
        return _Database.GetOrCreate(
            "MACAddresses_GetOrCreateMACAddress", 
            BuildDAL,
            sqlParameters.ToArray()
        );
    }
}
