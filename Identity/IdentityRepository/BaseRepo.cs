using IdentityModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IdentityRepository
{
    public class BaseRepo<T>  where T : EntityBase
    {
        public MongoDbRepository<T> mongoContext = new MongoDbRepository<T>();

        public string connectionString;
        //public BaseRepo(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        //}

        public BaseRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
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
