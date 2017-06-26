using System.Collections.Generic;
using IdentityModels;

namespace IdentityRepository
{
public interface IRepository<T> where T : BaseEntityPG
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();
    }
}
    