using IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IdentityRepository
{
    public interface IBaseRepo<T> where T : EntityBase
    {
        Result Add(T entity);
        Result Delete(string Id);
        Result Update(T entity);
        List<T> GetAll(string UserId);
        T FindById(string Id);
        T GetByUserId(string UserId);
    }
}
