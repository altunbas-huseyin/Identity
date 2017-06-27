using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRepository
{
    public interface IRepository<T> where T : EntityBase
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
