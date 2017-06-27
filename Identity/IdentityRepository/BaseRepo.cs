using IdentityModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IdentityRepository
{
    public class BaseRepo<T> where T : EntityBase
    {
        public MongoDbRepository<T> mongoContext = null;
        public string connectionString;
       
        //public BaseRepo(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        //}

        public BaseRepo(IConfiguration configuration)
        {
            if (mongoContext == null)
            {
                mongoContext = new MongoDbRepository<T>();
            }

            connectionString = "User ID=postgres;Password=Huso7474;Host=138.68.80.239;Port=5432;Database=myDataBase;Pooling=true;";//configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
    }
}
