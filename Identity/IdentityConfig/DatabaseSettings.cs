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
            this.MongoDbConnectionString = "mongodb://u1:Huso7474@138.68.80.239:27017/{DB_NAME}?safe=true";
        }
    }
}
