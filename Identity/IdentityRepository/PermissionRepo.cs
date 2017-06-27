using IdentityModels;
using IdentityModels.Permissions;
using IdentityModels.RolePermissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IdentityRepository
{
    public class PermissionRepo : BaseRepo<Permission>
    {

        private StatusRepo statusRepo;
        private RolePermissionRepo rolePermissionRepo;

        public PermissionRepo(IConfiguration configuration) : base(configuration)
        {
            rolePermissionRepo = new RolePermissionRepo(configuration);
            statusRepo = new StatusRepo(configuration);
        }

        public bool Insert(Permission permission)
        {
            // Permission _role = this.GetByName(permission.User_Id, permission.Name);
            // if (_role == null)
            // {
            //     mongoContext.Insert(permission);
            // }
            mongoContext.Insert(permission);
            return true;
        }

        public bool Update(Permission permission)
        {
            
            return mongoContext.Update(permission);
        }

        public bool Delete(string UserId,String Id)
        {
            Permission _permission = this.GetById(UserId, Id);
            if (_permission == null)
            {
                return true;
            }
            return mongoContext.Delete(_permission);
        }

        public Permission GetByName(string UserId, String Name)
        {
            Permission permission = mongoContext.SearchFor(p => p.User_Id == UserId && p.Name == Name).FirstOrDefault();
            return permission;
        }

        public Permission GetById(string UserId, String Id)
        {
            Permission permission = mongoContext.SearchFor(p => p.User_Id == UserId && p.Id == Id).FirstOrDefault();
            return permission;
        }

        public List<Permission> GetByUserId(string UserId)
        {
            List<Permission> permission = mongoContext.SearchFor(p => p.User_Id == UserId).ToList();
            return permission;
        }

      
        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("Name");

            bool result = mongoContext.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
