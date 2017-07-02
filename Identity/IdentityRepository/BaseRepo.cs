using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRepository
{
    public class BaseRepo<T>  where T : EntityBase
    {
        public MongoDbRepository<T> mongoContext = new MongoDbRepository<T>();

    }
}
