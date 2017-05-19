using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityConfig
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string MongoDbDatabaseName { get; set; }
        public string MongoDbConnectionString { get; set; }

        public DatabaseSettings()
        {
            this.ConnectionString = "";
            this.MongoDbDatabaseName = "Identity";
            //this.MongoDbConnectionString = "mongodb://u1:Huso7474@138.68.80.239:27017/{DB_NAME}?safe=true";
            this.MongoDbConnectionString = "mongodb://identity:1111@cluster0-shard-00-00-4vogi.mongodb.net:27017,cluster0-shard-00-01-4vogi.mongodb.net:27017,cluster0-shard-00-02-4vogi.mongodb.net:27017/{DB_NAME}?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin";
        }
    }
}
