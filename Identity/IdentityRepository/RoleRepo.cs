using IdentityModels;
using IdentityModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class RoleRepo
    {
        private MongoDbRepository<Role> roleRepository = new MongoDbRepository<Role>();
        public bool Update(Role role)
        {
            return roleRepository.Update(role);
        }

        public bool Delete(string UserId,String Id)
        {
            Role role = new Role();
            role._id = Id;
            return roleRepository.Delete(role);
        }


        public bool Insert(Role role)
        {
            Role _role = this.GetByName(role.Name);
            if (_role == null)
            {
                roleRepository.Insert(role);
            }
            return true;
        }


        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("Name");

            bool result = roleRepository.AddUniqIndex(list.ToArray());
            return true;
        }

        public Role GetByName(string Name)
        {
            Role role = roleRepository.SearchFor(p => p.Name == Name).FirstOrDefault();
            return role;
        }
    }
}
