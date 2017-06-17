using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRepository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : EntityBase
    {
        public MongoDbRepository<T> mongoContext = new MongoDbRepository<T>();

        public Result Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public T FindById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll(string UserId)
        {
            throw new NotImplementedException();
        }

        public T GetByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public Result Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
