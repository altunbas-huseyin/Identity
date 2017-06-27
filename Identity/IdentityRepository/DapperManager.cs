﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using IdentityModels;
using IdentityRepository;
using Microsoft.Extensions.Configuration;
using IdentityModels.Users;

public class DapperManager : BaseRepo<EntityBase>
{
    public DapperManager(IConfiguration configuration) : base(configuration)
    {
    }

    public static List<TType> GetData<TType>(string con, string sql)
    {
        List<TType> list = new List<TType>();

        using (var connection = new SqlConnection(con))
        {
            list = connection.Query<TType>(sql, null).ToList();
            connection.Close();
            connection.Dispose();
        }

        return list;
    }
    public static List<TType> GetData<TType>(string con, string sql, List<SqlParameter> SqlParameterList)
    {
        var _DynamicParameters = new DynamicParameters();

        foreach (SqlParameter item in SqlParameterList)
        {
            _DynamicParameters.Add(item.ParameterName, item.Value);
        }
        List<TType> list = new List<TType>();
        using (var connection = new SqlConnection(con))
        {
            //list = connection.Query<TType>(sql, null).ToList();
            list = connection.Query<TType>(sql, _DynamicParameters, commandType: CommandType.Text).ToList();
            connection.Close();
            connection.Dispose();
        }

        return list;
    }
    public static List<TType> GetDataStoredProcedure<TType>(string con, string sql, List<SqlParameter> SqlParameterList)
    {
        var _DynamicParameters = new DynamicParameters();

        foreach (SqlParameter item in SqlParameterList)
        {
            _DynamicParameters.Add(item.ParameterName, item.Value);
        }
        List<TType> list = new List<TType>();
        using (var connection = new SqlConnection(con))
        {
            //list = connection.Query<TType>(sql, null).ToList();
            list = connection.Query<TType>(sql, _DynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            connection.Close();
            connection.Dispose();
        }

        return list;
    }

    public int Insert<TType>(string con, TType type)
    {
        int result = 0;
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            string sql = getTypeInsertQuery(type);
            result = dbConnection.ExecuteScalar<int>(sql, type);
        }
        return result;
    }

    public bool Update<TType>(string con, TType type)
    {
        bool result = false;
        using (IDbConnection dbConnection = Connection)
        {
            dbConnection.Open();
            string sql = getTypeUpdateQuery(type);
            dynamic d = dbConnection.Query(sql, type);
            result = true;
        }
        return result;
    }

    public static object Insert<TType>(SqlConnection conn, SqlTransaction trans, TType type)
    {
        string sql = getTypeInsertQuery(type);
        object resut = conn.Query<int>(sql, type).Single();
        conn.Close();
        conn.Dispose();
        return resut;
    }


    public static string getTypeInsertQuery<TType>(TType type)
    {
        List<string> columnsList = new List<string>();
        List<string> columnsValueList = new List<string>();

        foreach (PropertyInfo propertyInfo in type.GetType().GetProperties())
        {
            object o = propertyInfo.GetValue(type, null);
            if (o == null) { continue; }
            if (propertyInfo.Name == "Id") { continue; }

            columnsList.Add(propertyInfo.Name.ToLower().Replace("ı", "i"));
            columnsValueList.Add("@" + propertyInfo.Name);
        }

        string sql = "insert into \"" + type.GetType().Name.ToLower() + "\" (" + string.Join(",", columnsList.ToArray()) + ") values(" + string.Join(",", columnsValueList.ToArray()) + "); SELECT currval(pg_get_serial_sequence('"+ type.GetType().Name.ToLower() + "', 'id'));";

        return sql;
    }

    public static string getTypeUpdateQuery<TType>(TType type)
    {
        List<string> columnsList = new List<string>();
        
        foreach (PropertyInfo propertyInfo in type.GetType().GetProperties())
        {
            object o = propertyInfo.GetValue(type, null);
            if (o == null) { continue; }
            if (propertyInfo.Name == "Id") { continue; }
            string column = propertyInfo.Name.ToLower().Replace("ı", "i") + " = @" + propertyInfo.Name;
            columnsList.Add(column);
            
        }

        string sql = "UPDATE \"" + type.GetType().Name.ToLower() + "\" SET "+ string.Join(",", columnsList.ToArray()) + " WHERE id = @Id";
        return sql;
    }

    public static List<SqlParameter> getSqlParameter<TType>(TType type)
    {

        List<SqlParameter> list = new List<SqlParameter>();

        foreach (PropertyInfo propertyInfo in type.GetType().GetProperties())
        {
            object o = propertyInfo.GetValue(type, null);
            if (o == null) { continue; }

            SqlParameter p = new SqlParameter("@" + propertyInfo.Name, o);
            list.Add(p);
        }

        return list;
    }
}
