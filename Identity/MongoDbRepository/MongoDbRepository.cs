using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using System.Reflection;
using IdentityModels;


//https://github.com/rsingh85/MongoDbRepository


/// <summary>
/// A MongoDB repository. Maps to a collection with the same name
/// as type TEntity.
/// </summary>
/// <typeparam name="T">Entity type for this repository</typeparam>
public class MongoDbRepository<TEntity> :
    IRepository<TEntity> where
        TEntity : EntityBase
{
    private string MongoDbDatabaseName;
    private string MongoDbConnectionString;
    private MongoDatabase database;
    private MongoCollection<TEntity> collection;
    private IdentityConfig.DatabaseSettings config = new IdentityConfig.DatabaseSettings();
    public MongoDbRepository()
    {
        
        string ConnectionString = config.ConnectionString;
        MongoDbDatabaseName = config.MongoDbDatabaseName;
        MongoDbConnectionString = config.MongoDbConnectionString;

        GetDatabase();
        GetCollection();
    }
    public bool AddUniqIndex(string ColumnName)
    {

        var users = database.GetCollection<TEntity>(typeof(TEntity).Name);
        users.EnsureIndex(new IndexKeysBuilder().Ascending(ColumnName), IndexOptions.SetUnique(true));

        return true;
    }

    public bool AddUniqIndex(string[] ColumnName)
    {

        var users = database.GetCollection<TEntity>(typeof(TEntity).Name);
        users.EnsureIndex(new IndexKeysBuilder().Ascending(ColumnName), IndexOptions.SetUnique(true));

        return true;
    }

    public bool Insert(TEntity entity)
    {
        //entity.Id = Guid.NewGuid();
        entity.Create_Date = DateTime.Now.AddHours(3);
        entity.Update_Date = entity.Create_Date;
        collection.Insert(entity);
        return true;//Burası tekrar kontrol edilecek
    }


    public bool Update(TEntity entity)
    {
        //burada mobgo db'ye 3 saat eksik olarak kyıt yapıyordu bu şekilde çözdüm :-(
        entity.Update_Date = DateTime.Now.AddHours(3);
        if (entity.Id == null)
            return Insert(entity);


        return collection
            .Save(entity)
                .DocumentsAffected > 0;
    }

    public bool Delete(TEntity entity)
    {
        return collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
    }

    public IList<TEntity>
        SearchFor(Expression<Func<TEntity, bool>> predicate)
    {
        return collection
            .AsQueryable<TEntity>()
                .Where(predicate.Compile())
                    .ToList();
    }

    public IList<TEntity> GetAll()
    {
        return collection.FindAllAs<TEntity>().ToList();
    }

    public IList<TEntity> NativeQuery()
    {
        BsonDocument query = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>("{ 'Email':'altunbas.huseyin@gmail.com'}");
        QueryDocument queryDoc = new QueryDocument(query);
        return collection.FindAs<TEntity>(new QueryDocument(queryDoc)).ToList();
    }

    public TEntity GetById(Guid id)
    {
        return collection.FindOneByIdAs<TEntity>(id);
    }

    #region Private Helper Methods
    private void GetDatabase()
    {
        var client = new MongoClient(GetConnectionString());
        var server = client.GetServer();

        database = server.GetDatabase(GetDatabaseName());
    }

    private string GetConnectionString()
    {
        //return ConfigurationManager.AppSettings.Get("MongoDbConnectionString").Replace("{DB_NAME}", GetDatabaseName());

        return MongoDbConnectionString.Replace("{DB_NAME}", GetDatabaseName()); ;
    }

    private string GetDatabaseName()
    {
        //return ConfigurationManager.AppSettings.Get("MongoDbDatabaseName");
        return MongoDbDatabaseName;
    }

    private void GetCollection()
    {
        collection = database
            .GetCollection<TEntity>(typeof(TEntity).Name);
    }
    #endregion
}