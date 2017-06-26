﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;



    public static class DapperManager
    {

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

        public static object Insert<TType>(string con, TType type)
        {

            string sql = getSqlParameterString(type);
            SqlConnection conn = new SqlConnection(con);
            object resut = conn.Query<int>(sql, type).Single();
            conn.Close();
            conn.Dispose();
            return resut;
        }


        public static object Insert<TType>(SqlConnection conn, SqlTransaction trans, TType type)
        {

            string sql = getSqlParameterString(type);
            object resut = conn.Query<int>(sql, type).Single();
            conn.Close();
            conn.Dispose();
            return resut;
        }


        public static string getSqlParameterString<TType>(TType type)
        {

            List<string> columnsList = new List<string>();
            List<string> columnsValueList = new List<string>();

            foreach (PropertyInfo propertyInfo in type.GetType().GetProperties())
            {
                object o = propertyInfo.GetValue(type, null);
                if (o == null) { continue; }

                columnsList.Add(propertyInfo.Name);
                columnsValueList.Add("@" + propertyInfo.Name);
            }

            string sql = "insert into " + type.GetType().Name + "(" + string.Join(",", columnsList.ToArray()) + ") values(" + string.Join(",", columnsValueList.ToArray()) + ") select SCOPE_IDENTITY()";

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
