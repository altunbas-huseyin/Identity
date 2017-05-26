using IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityRepository
{
    public class RolePermissionRepo
    {
        private MongoDbRepository<RolePermission> context = new MongoDbRepository<RolePermission>();
        private StatusRepo statusRepo = new StatusRepo();

        public bool Insert(RolePermission rolePermission)
        {
            RolePermission _role = this.GetByPermissionId(rolePermission.UserId, rolePermission.PermissionId);
            if (_role == null)
            {
                context.Insert(rolePermission);
            }
            return true;
        }

        public bool Update(RolePermission rolePermission)
        {
            return context.Update(rolePermission);
        }

        public bool Delete(string UserId, String Id)
        {
            RolePermission _permission = this.GetById(UserId, Id);
            if (_permission == null)
            {
                return true;
            }
            return context.Delete(_permission);
        }

        public RolePermission GetById(string UserId, String Id)
        {
            RolePermission rolePermission = context.SearchFor(p => p.UserId == UserId && p._id == Id).FirstOrDefault();
            return rolePermission;
        }

        public RolePermission GetByPermissionId(string UserId, String PermissionId)
        {
            RolePermission rolePermission = context.SearchFor(p => p.UserId == UserId && p.PermissionId == PermissionId).FirstOrDefault();
            return rolePermission;
        }

        public bool AddUniqIndex()
        {
            List<string> list = new List<string>();
            list.Add("UserId");
            list.Add("PermissionId");
            list.Add("RoleId");
            bool result = context.AddUniqIndex(list.ToArray());
            return true;
        }
    }
}
