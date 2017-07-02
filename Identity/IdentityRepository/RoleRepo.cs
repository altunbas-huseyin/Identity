using IdentityModels;
using IdentityModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class RoleRepo : BaseRepo<Role>
    {
       
        public bool Update(Role role)
        {
            return mongoContext.Update(role);
        }

        public bool Delete(string UserId, String Id)
        {
            Role role = this.GetById(UserId, Id);
            return mongoContext.Delete(role);
        }

        public bool Insert(Role role)
        {
            Role _role = this.GetByName(role.UserId, role.Name);
            if (_role == null)
            {
                mongoContext.Insert(role);
            }
            return true;
        }

        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("Name");

            bool result = mongoContext.AddUniqIndex(list.ToArray());
            return true;
        }

        public Role GetByName(string UserId, string Name)
        {
            Role role = mongoContext.SearchFor(p => p.UserId == UserId && p.Name == Name).FirstOrDefault();
            return role;
        }

        public List<Role> GetByUserId(string UserId)
        {
            List<Role> roleList = mongoContext.SearchFor(p => p.UserId == UserId).ToList();
            return roleList;
        }

        public Role GetById(string UserId, string Id)
        {
            Role role = mongoContext.SearchFor(p => p._id == Id && p.UserId == UserId).First();
            return role;
        }

    }
}
