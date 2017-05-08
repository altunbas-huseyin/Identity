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
using Models;


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
    private string MongoDbDatabaseName = "Identity";
    public string MongoDbConnectionString = "mongodb://84.51.52.183/{DB_NAME}?safe=true";
    //public string MongoDbConnectionString = "mongodb://192.168.1.80/{DB_NAME}?safe=true";
    private MongoDatabase database;
    private MongoCollection<TEntity> collection;

    public MongoDbRepository()
    {
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
        entity.CreateDate = DateTime.Now;
        entity.UpdateDate = entity.CreateDate;
        collection.Insert(entity);

        return true;//Burası tekrar kontrol edilecek
    }


    public bool Update(TEntity entity)
    {
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