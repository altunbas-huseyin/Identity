using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class RoleRepo
    {
        private MongoDbRepository<Role> roleRepository = new MongoDbRepository<Role>();
        public bool Add(Role role)
        {
            return roleRepository.Insert(role);
        }

        public bool Update(Role role)
        {
            return roleRepository.Update(role);
        }

        public bool Delete(String Id)
        {
            Role role = new Role();
            role.Id = new Guid(Id);
            return roleRepository.Delete(role);
        }


        public bool InsertRoleList()
        {
            roleRepository.Insert(new Role { Name = "Admin" });
            roleRepository.Insert(new Role { Name = "Firm" });
            roleRepository.Insert(new Role { Name = "User" });


            return true;
        }


        public bool AddUniqIndex()
        {
            bool result = roleRepository.AddUniqIndex("Name");
            return true;
        }

        public Role Get(string Name)
        {
            Role role =  roleRepository.SearchFor(p => p.Name == Name).FirstOrDefault();
            return role;
        }
    }
}
