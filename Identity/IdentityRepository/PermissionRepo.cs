using IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class PermissionRepo
    {
        private MongoDbRepository<Permission> context = new MongoDbRepository<Permission>();
        private StatusRepo statusRepo = new StatusRepo();

        public bool Insert(Permission permission)
        {
            Permission _role = this.GetByName(permission.UserId, permission.Name);
            if (_role == null)
            {
                context.Insert(permission);
            }
            return true;
        }

        public bool Update(Permission permission)
        {
            return context.Update(permission);
        }

        public bool Delete(string UserId,String Id)
        {
            Permission _permission = this.GetById(UserId, Id);
            if (_permission == null)
            {
                return true;
            }
            return context.Delete(_permission);
        }

        public Permission GetByName(string UserId, String Name)
        {
            Permission permission = context.SearchFor(p => p.UserId == UserId && p.Name == Name).FirstOrDefault();
            return permission;
        }

        public Permission GetById(string UserId, String Id)
        {
            Permission permission = context.SearchFor(p => p.UserId == UserId && p._id == Id).FirstOrDefault();
            return permission;
        }

        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("Name");

            bool result = context.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
